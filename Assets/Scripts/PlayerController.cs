using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    public GameObject _playerObject;
    [Header("Scripts")]
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerDeath _playerDeath;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private CharacterController _characterController;
    [Header("Animator")]
    [SerializeField] private Animator _animator;
    [Header("Variables")]
    [SerializeField] private float _switchDelay;
    [SerializeField] private float _playerSpeed = 8f;
    [SerializeField] private float _increaseAmount;
    [Header("Raycast")]
    [SerializeField] private float rayCastDistance;
    public int raycastSwiped = 0;


    private float _maxPlayerSpeed = 20f;
    private float _controllerHeight = 1.38f;
    private int _lineToMove = 1;
    private float _lineDistance = 1f;
    private bool _groundedPlayer;
    private float _jumpHeight = 1.0f;
    private float _gravityValue = -9.81f;
    private bool _isGrounded;
    private bool _isSliding;
    private Vector3 _playerVelocity;
    private Coroutine _slideCoroutine;
    private bool _leftObstacle;
    private bool _rightObstacle;

    private void Awake()
    {
        _soundManager.PlayWalkSound();
    }

    private void OnEnable()
    {
        StartCoroutine("IncreaseSpeed");
        _playerInput.Jumped += OnJumped;
        _playerInput.Slided += OnSlided;
        _playerInput.Swiped += OnSwiped;
        _playerDeath.OnPlayerDied += OnDied;
    }

    private void OnDisable()
    {
        StopCoroutine("IncreaseSpeed");
        _playerInput.Jumped -= OnJumped;
        _playerInput.Slided -= OnSlided;
        _playerInput.Swiped -= OnSwiped;
        _playerDeath.OnPlayerDied -= OnDied;
    }

    private void Update()
    {
        CheckGround();
        Move();
        CheckCollisionWithObstacles();
    }

    private void Move()
    {
        _characterController.Move(transform.forward * _playerSpeed * Time.deltaTime);
        
        _playerVelocity.y += _gravityValue * Time.deltaTime;
        _characterController.Move(_playerVelocity * Time.deltaTime);
    }

    private void CheckGround()
    {
        _groundedPlayer = _characterController.isGrounded;
        //_isGrounded = groundCheck.groundCheck;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }
    }

    private void OnDied()
    {
        _playerSpeed = 0f;
        StopCoroutine("IncreaseSpeed");
    }

    private void OnJumped()
    {
        if (_groundedPlayer)
        {
            _animator.SetTrigger("Jump");
            if(_isSliding && _slideCoroutine != null)
            {
                StopCoroutine(_slideCoroutine);
            }
            _characterController.height = _controllerHeight;
            _isSliding = false;
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
            _soundManager.PlayJumpSound();
        }
    }

    private void OnSlided()
    {
        if(!_isSliding)
        _slideCoroutine = StartCoroutine(SlideDown());
    }

    public void OnSwiped(bool isLeft)
    {
        _lineToMove = Mathf.Clamp(_lineToMove, 0, 2);
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if(!isLeft)
        {
            if (_lineToMove < 2)
            {
                _lineToMove++;
                raycastSwiped = 2;
                Debug.Log("право");
                StartCoroutine(SwipeRight());

                //_animator.Play("SwipeRight");
                //anim.SetTrigger("MoveRight");
            }
        }
        else if(isLeft)
        {
            if (_lineToMove > 0)
            {
                _lineToMove--;
                raycastSwiped = 1;
                Debug.Log("лево");
                StartCoroutine(SwipeLeft());
                //_animator.Play("SwipeLeft");
                //anim.SetTrigger("MoveLeft");
            }
        }
        else
           raycastSwiped = 0;

        if (_lineToMove == 0)
            targetPosition += Vector3.left * _lineDistance;
        else if (_lineToMove == 2)
            targetPosition += Vector3.right * _lineDistance;

        transform.DOMoveX(targetPosition.x, _switchDelay);
    }

    private IEnumerator SlideDown()
    {
        _isSliding = true;
        DOTween.To(() => _characterController.height, x => _characterController.height = x, 0, 0.10f);
        transform.DOMoveY(0.2f, 0.32f);
        
        _animator.SetTrigger("Slide");
        _soundManager.PlaySlideSound();
        yield return new WaitForSeconds(0.8f);

        DOTween.To(() => _characterController.height, x => _characterController.height = x, _controllerHeight, 0.20f);

        yield return new WaitForSeconds(0.3f);
        _isSliding = false;
    }

    public IEnumerator IncreaseSpeed()
    {
        yield return new WaitForSeconds(1);
        if (_playerSpeed < _maxPlayerSpeed)
        {
            _playerSpeed += _increaseAmount;
            Debug.Log(_playerSpeed);
            StartCoroutine("IncreaseSpeed");
        }
    }
    private void CheckCollisionWithObstacles()
    {
        _leftObstacle = Physics.Raycast(transform.position, Vector3.left, rayCastDistance, LayerMask.GetMask("Ground"));
        _rightObstacle = Physics.Raycast(transform.position, Vector3.right, rayCastDistance, LayerMask.GetMask("Ground"));
    }
    private IEnumerator SwipeLeft()
    {
        _playerObject.transform.DORotate(new Vector3(0, -30f, 0),0.2f);
        yield return new WaitForSeconds(0.2f);
        _playerObject.transform.DORotate(new Vector3(0, 0f, 0), 0.2f);
    }
    private IEnumerator SwipeRight()
    {
        _playerObject.transform.DORotate(new Vector3(0, 30f, 0), 0.2f);
        yield return new WaitForSeconds(0.2f);
        _playerObject.transform.DORotate(new Vector3(0, 0f, 0), 0.2f);
    }
}
