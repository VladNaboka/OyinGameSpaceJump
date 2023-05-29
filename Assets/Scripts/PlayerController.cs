using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerDeath _playerDeath;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _switchDelay;
    private float _playerSpeed = 5f;
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


    private void Awake()
    {
        _soundManager.PlayWalkSound();
        StartCoroutine("IncreaseSpeed");
    }

    private void OnEnable()
    {
        _playerInput.Jumped += OnJumped;
        _playerInput.Slided += OnSlided;
        _playerInput.Swiped += OnSwiped;
        _playerDeath.OnPlayerDied += OnDied;
    }

    private void OnDisable()
    {
        _playerInput.Jumped -= OnJumped;
        _playerInput.Slided -= OnSlided;
        _playerInput.Swiped -= OnSwiped;
        _playerDeath.OnPlayerDied -= OnDied;
    }

    private void Update()
    {
        CheckGround();
        Move();  
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
            _characterController.height = _controllerHeight;
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
            _soundManager.PlayJumpSound();
        }
    }

    private void OnSlided()
    {
        if(!_isSliding)
        StartCoroutine(SlideDown());
    }

    private void OnSwiped(bool isLeft)
    {
        _lineToMove = Mathf.Clamp(_lineToMove, 0, 2);
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if(!isLeft)
        {
            if (_lineToMove < 2)
            {
                _lineToMove++;
                //anim.SetTrigger("MoveRight");
            }
        }
        else if(isLeft)
        {
            if (_lineToMove > 0)
            {
                _lineToMove--;
                //anim.SetTrigger("MoveLeft");
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

    IEnumerator IncreaseSpeed()
    {
        yield return new WaitForSeconds(10);
        if (_playerSpeed < _maxPlayerSpeed)
        {
            _playerSpeed += 1f;
            Debug.Log(_playerSpeed);
            StartCoroutine("IncreaseSpeed");
        }
      
    }
}
