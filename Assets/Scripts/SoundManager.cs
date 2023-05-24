using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource walkSound;
    public AudioSource jumpSound;
    public AudioSource slideSound;
    public AudioSource coinPickupSound;
    public AudioSource deathSound;
    public AudioSource landingSound;

    private bool isWalking;

    private void Start()
    {
        isWalking = true;
    }

    public void PlayWalkSound()
    {
        if (!isWalking)
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
    public void PlayDeathSound()
    {
        StopWalkSound();
        jumpSound.Stop();
        slideSound.Stop();
        deathSound.Play();

    }
}