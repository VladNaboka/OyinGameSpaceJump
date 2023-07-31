using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    public AudioSource walkSound;
    public AudioSource jumpSound;
    public AudioSource slideSound;
    public AudioSource coinPickupSound;
    public AudioSource deathSound;
    public AudioSource edeathSound;
    public AudioSource slamSound;
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
        
        if(!PlayerPrefs.HasKey("musicVolume") && !PlayerPrefs.HasKey("soundVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            PlayerPrefs.SetFloat("soundVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
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
    public void PlaySlamSound()
    {
        slamSound.Play();
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
    public void ChangeVolumeMusic()
    {
        musicSound.volume = musicSlider.value;
        SaveMusic();
    }
    public void ChangeVolumeSFX()
    {
        buttonSound.volume = soundSlider.value * 0.2f;
        walkSound.volume = soundSlider.value;
        jumpSound.volume = soundSlider.value;
        slideSound.volume = soundSlider.value;
        coinPickupSound.volume = soundSlider.value;
        deathSound.volume = soundSlider.value;
        edeathSound.volume = soundSlider.value;
        //landingSound.mute = !landingSound.mute;
        magnetSound.volume = soundSlider.value;
   /*     slamSound.mute = !slamSound.mute;*/
        SaveSound();
    }

    private void Load()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        soundSlider.value = PlayerPrefs.GetFloat("soundVolume");
    }

    private void SaveMusic()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }
    private void SaveSound()
    {
        PlayerPrefs.SetFloat("soundVolume", soundSlider.value);
    }
}