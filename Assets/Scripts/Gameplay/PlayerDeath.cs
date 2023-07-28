using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private Animator _anim;
    [SerializeField] private GameObject _vfx;
    //public Behaviour scriptInputPlayer;
    //public Behaviour scriptPlayerControler;

    private Vector3 currentPosition;

    private bool _isDead;
    public static bool deadState;

    public event Action OnPlayerDied;

    //[NonSerialized]public bool isDead;
    public SoundManager sfx;

    private void Update()
    {
        currentPosition = transform.position;
        //kostyl
        if(gameObject.transform.position.y < -1f)
        {
            _cameraMovement.enabled = false;
            _anim.Play("Fall Flat");
            if(gameObject.transform.position.y < -3.5f && !_isDead)
            {
                Death();
                sfx.PlayDeathSound();
            }
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Obstacle"))
        {
            Debug.Log(hit.gameObject.name);
            sfx.PlayDeathSound();
            _anim.Play("Hit");
            Death();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Electricity"))
        {
            //ElectricityLose()
            //Vmesto hit, animatia sgoryania
            _anim.Play("Hit");
            ElectricityDeath();
        }
        if (other.gameObject.CompareTag("TriggerDown"))
        {
            Debug.Log("Down");
            _cameraMovement.enabled = false;
            _anim.Play("Fall Flat");
            Death();
            sfx.PlayDeathSound();
            //scriptInputPlayer.enabled = false;
            currentPosition.x = 0f;
            currentPosition.z = 0f;
            transform.position = currentPosition; 
        }
    }

    private void Death()
    {
        //_cameraMovement.enabled = false;
        _isDead = true;
        OnPlayerDied?.Invoke();
        _gameManager.GameOverScreen();
        Debug.Log("Death");
    }

    private void ElectricityDeath()
    {
        Death();
        sfx.PlayEDeathSound();
        _vfx.SetActive(true);
    }
}
