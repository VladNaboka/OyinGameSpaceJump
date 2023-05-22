using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private float _playerSpeed = 2.0f;
    [SerializeField] private float _switchDelay;
    private int _lineToMove = 1;
    private float _lineDistance = 1f;
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    private float _jumpHeight = 1.0f;
    private float _gravityValue = -9.81f;
    private bool _isGrounded;

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
        _isGrounded = groundCheck.groundCheck;
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
        //_groundedPlayer = _characterController.isGrounded;
        if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }
    }

    private void OnJumped()
    {
        if (_isGrounded)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
        }
    }

    private void OnSlided()
    {
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
        _characterController.center = new Vector3(0, 0.49f, 0);

        //Anim.SetTrigger("Slide");
        yield return new WaitForSeconds(1);

        _characterController.height = 1.75f;
        _characterController.center = new Vector3(0, 0, 0);
    }
}
