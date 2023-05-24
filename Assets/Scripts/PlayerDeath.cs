using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private Animator _anim;

    public event Action OnPlayerDied = default;

    //[NonSerialized]public bool isDead;
    public SoundManager sfx;

    private void Update()
    {
        //kostyl
        if(gameObject.transform.position.y < -1f)
        {
            _cameraMovement.enabled = false;
            _anim.Play("Fall Flat");
            if(gameObject.transform.position.y < -3.5f)
                FallLose();
                sfx.PlayDeathSound();
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Obstacle"))
        {
            _anim.Play("Hit");
            HitLose();

            sfx.PlayDeathSound();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Electricity"))
        {
            //ElectricityLose()
            //Vmesto hit, animatia sgoryania
            _anim.Play("Hit");
            HitLose();

            sfx.PlayDeathSound();
        }
    }

    private void FallLose()
    {
        //_cameraMovement.enabled = false;
        OnPlayerDied?.Invoke();
        _playerInput.enabled = false;
        _gameManager.GameOverScreen();
    }

    private void HitLose()
    {
        
        //_cameraMovement.enabled = false;
        OnPlayerDied?.Invoke();
        _playerInput.enabled = false;
        _gameManager.GameOverScreen();
    }
}
