using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public Button buttonMusic;
    public Button buttonSFX;
    public Sprite musicOn;
    public Sprite musicOff;
    public Sprite SFXOn;
    public Sprite SFXOff;

    public AudioSource walkSound;
    public AudioSource jumpSound;
    public AudioSource slideSound;
    public AudioSource coinPickupSound;
    public AudioSource deathSound;
    public AudioSource edeathSound;
    //public AudioSource landingSound;
    public AudioSource magnetSound;
    public GameObject objectMusic;
    private AudioSource musicSound;
    public GameObject objectCkilckButtonSound;
    private AudioSource buttonSound;


    private bool isWalking;
    private bool isDead;

    private void Start()
    {
        isWalking = true;
        isDead = false;
        objectMusic =  GameObject.FindWithTag("GameMusic");
        musicSound = objectMusic.GetComponent<AudioSource>();
        objectCkilckButtonSound = GameObject.FindWithTag("ClickSound");
        buttonSound = objectCkilckButtonSound.GetComponent<AudioSource>();
    }

    public void PlayWalkSound()
    {
        if (!isWalking && !isDead)
        {
            walkSound.Play();
            isWalking = true;
        }
    }

    public void StopWalkSound()
    {
        if (isWalking)
        {
            walkSound.Stop();
            isWalking = false;
        }
    }

    public void PlayJumpSound()
    {
        // Stop the walk sound if it's playing
        if (isWalking)
        {
            StopWalkSound();
        }

        jumpSound.Play();

        // Turn on walk sound again after the jump sound is finished
        Invoke("PlayWalkSound", jumpSound.clip.length);
    }

    public void PlaySlideSound()
    {
        // Stop the walk sound if it's playing
        if (isWalking)
        {
            StopWalkSound();
        }

        slideSound.Play();

        // Turn on walk sound again after the slide sound is finished
        Invoke("PlayWalkSound", slideSound.clip.length);
    }

    public void PlayCoinPickupSound()
    {
        coinPickupSound.Play();
    }
    public void PlayMagnetPickupSound()
    {
        magnetSound.Play();
    }
    public void PlayDeathSound()
    {
        if (!isDead)
        {
            isDead = true;
            walkSound.Stop();
            StopWalkSound();
            jumpSound.Stop();
            slideSound.Stop();
            deathSound.Play();
            Debug.Log("DeathSound Played");
        }
    }
    public void PlayEDeathSound()
    {
        walkSound.Stop();
        StopWalkSound();
        jumpSound.Stop();
        slideSound.Stop();
        //landingSound.Stop();
        edeathSound.Play();
    }
    public void MuteSoundOff()
    {
        walkSound.Stop();
        jumpSound.Stop();
        slideSound.Stop();
        //landingSound.Stop();
        deathSound.Stop();
    }
    public void MuteSoundOn()
    {
        walkSound.Play();
    }
    public void ToggleMusic()
    {
        musicSound.mute = !musicSound.mute;
        if(musicSound.mute )
        {
            buttonMusic.image.sprite = musicOff;
        }
        else
        {
            buttonMusic.image.sprite = musicOn;
        }
    }
    public void ToggleSFX()
    {
        buttonSound.mute = !buttonSound.mute;
        walkSound.mute = !walkSound.mute;
        jumpSound.mute = !jumpSound.mute;
        slideSound.mute = !slideSound.mute;
        coinPickupSound.mute = !coinPickupSound.mute;
        deathSound.mute = !deathSound.mute;
        edeathSound.mute = !edeathSound.mute;
        //landingSound.mute = !landingSound.mute;
        magnetSound.mute = !magnetSound.mute;

        if (walkSound.mute)
        {
            buttonSFX.image.sprite = SFXOff;
        }
        else if (!walkSound.mute)
        {
            buttonSFX.image.sprite = SFXOn;
        }
    }
}