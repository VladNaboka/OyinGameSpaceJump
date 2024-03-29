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
    [SerializeField] public float _playerSpeed = 8f;
    [SerializeField] private float _increaseAmount;
    [Header("Slam")]
    [SerializeField] private float _leftSlamDist;
    [SerializeField] private float _rightSlamDist;
    [SerializeField] private float _startTimer;
    private float _slamTimer = 0;
    private bool _canSlam;
    private Vector3 _savedPosition;
    [Header("Raycast")]
    [SerializeField] private float rayCastDistance;
    [Header("Booleans")]
    public bool _leftObstacle;
    public bool _rightObstacle;

    private float _maxPlayerSpeed = 20f;
    private float _controllerHeight = 1.38f;
    private int _lineToMove = 1;
    private float _lineDistance = 1f;
    private bool _groundedPlayer;
    private float _jumpHeight = 1.0f;
    private float _gravityValue = -9.81f;
    private bool _isGrounded;
    private bool _isSliding;
    private bool _isSlaming;
    private Vector3 _playerVelocity;
    private Coroutine _slideCoroutine;
    
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
        _playerDeath.OnPlayerDied += ChangePlayerSpeed;
    }

    private void OnDisable()
    {
        StopCoroutine("IncreaseSpeed");
        _playerInput.Jumped -= OnJumped;
        _playerInput.Slided -= OnSlided;
        _playerInput.Swiped -= OnSwiped;
        _playerDeath.OnPlayerDied -= ChangePlayerSpeed;
    }

    private void Update()
    {
        CheckGround();
        HandleSlamTimer();
        Move();
        //CheckCollisionWithObstacles();
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

    private void ChangePlayerSpeed()
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
        if(!isLeft && _canSlam)
        {
            if (_lineToMove < 2 && !_rightObstacle)
            {
                _lineToMove++;
                StartCoroutine(SwipeRotation(30));

                //_animator.Play("SwipeRight");
                //anim.SetTrigger("MoveRight");
            }
            else if(_rightObstacle)
            {
                _slamTimer = _startTimer;
                Debug.Log("SLAM RIGHT");
                StartCoroutine(SlamRight());
            }
        }
        else if(isLeft && _canSlam)
        {
            if (_lineToMove > 0 && !_leftObstacle)
            {
                _lineToMove--;
                StartCoroutine(SwipeRotation(-30));
                //_animator.Play("SwipeLeft");
                //anim.SetTrigger("MoveLeft");
            }
            else if (_leftObstacle && _canSlam)
            {
                Debug.Log("SLAM LEFT");
                _slamTimer = _startTimer;

                StartCoroutine(SlamLeft());
            }
        }

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
    //private void CheckCollisionWithObstacles()
    //{
    //    _leftObstacle = Physics.Raycast(transform.position, Vector3.left, rayCastDistance, LayerMask.GetMask("Obstacle"));
    //    _rightObstacle = Physics.Raycast(transform.position, Vector3.right, rayCastDistance, LayerMask.GetMask("Obstacle"));
    //}
    private IEnumerator SwipeRotation(float rotation)
    {
        _playerObject.transform.DORotate(new Vector3(0, rotation, 0),0.2f);
        yield return new WaitForSeconds(0.2f);
        _playerObject.transform.DORotate(new Vector3(0, 0f, 0), 0.2f);
    }
    
    private IEnumerator SlamLeft()
    {
        Debug.Log("SlamLeft");
        _soundManager.PlaySlamSound();
        _savedPosition = _playerObject.transform.position;
        float targetPos = _savedPosition.x - _rightSlamDist;
        _playerObject.transform.DOMoveX(targetPos, 0.1f);
        _playerObject.transform.DORotate(new Vector3(0, 0, 5f), 0.1f);
        yield return new WaitForSeconds(0.1f);

        //Back to place
        if(_lineToMove == 1)
            _playerObject.transform.DOMoveX(0, 0.2f);
        else if(_lineToMove == 2)
            _playerObject.transform.DOMoveX(1, 0.2f);

        _playerObject.transform.DORotate(new Vector3(0, 0, 0), 0.2f);
        _savedPosition = Vector3.zero;
    }

    private IEnumerator SlamRight()
    {
        _soundManager.PlaySlamSound();
        _savedPosition = _playerObject.transform.position;
        float targetPos = _savedPosition.x + _rightSlamDist;
        _playerObject.transform.DOMoveX(targetPos, 0.1f);
        _playerObject.transform.DORotate(new Vector3(0, 0, -5f), 0.1f);
        yield return new WaitForSeconds(0.1f);

        //Back to place
        if (_lineToMove == 1)
            _playerObject.transform.DOMoveX(0, 0.2f);
        else if (_lineToMove == 0)
            _playerObject.transform.DOMoveX(-1, 0.2f);

        _playerObject.transform.DORotate(new Vector3(0, 0, 0), 0.2f);
        _savedPosition = Vector3.zero;
    }

    private void HandleSlamTimer()
    {
        if (_slamTimer <= 0)
        {
            _canSlam = true;
        }
        else
        {
            _canSlam = false;
            _slamTimer -= Time.deltaTime;
            Debug.Log(_slamTimer);
        }
    }
}
