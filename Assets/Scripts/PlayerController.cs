using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerDeath _playerDeath;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _playerSpeed = 2.0f;
    [SerializeField] private float _switchDelay;
    private int _lineToMove = 1;
    private float _lineDistance = 1f;
    private bool _groundedPlayer;
    private float _jumpHeight = 1.0f;
    private float _gravityValue = -9.81f;
    private bool _isGrounded;
    private bool _isSliding;
    private Vector3 _playerVelocity;

    private void OnEnable()
    {
        _playerInput.Jumped += OnJumped;
        _playerInput.Slided += OnSlided;
        _playerInput.Swiped += OnSwiped;
    }

    private void OnDisable()
    {
        _playerInput.Jumped -= OnJumped;
        _playerInput.Slided -= OnSlided;
        _playerInput.Swiped -= OnSwiped;
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

    private void OnJumped()
    {
        if (_groundedPlayer)
        {
            _animator.SetTrigger("Jump");
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
        }
    }

    private void OnSlided()
    {
        if(_isSliding != true)
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
        _characterController.height = 0;
        _playerVelocity.y = -10f;
        _isSliding = true;
        //_characterController.center = new Vector3(0, 0.49f, 0);

        _animator.SetTrigger("Slide");
        yield return new WaitForSeconds(1);


        _playerVelocity.y = 0f;
        _characterController.height = 1.38f;
        _isSliding = false;
        //_characterController.center = new Vector3(0, 0, 0);
    }
}
