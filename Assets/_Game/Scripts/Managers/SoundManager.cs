using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource audioSource;
    public AudioClip themeClip;
    public AudioClip buttonClip;
    public AudioClip correctClip;
    public AudioClip wrongClip;
    public AudioClip shieldClip;
    void Start()
    {
        PlayTheme();
    }
    public void PlayTheme()
    {
        audioSource.clip = themeClip;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void PlayButon()
	{
        audioSource.PlayOneShot(buttonClip);
    }
    public void PlayCorrect()
    {
        audioSource.PlayOneShot(correctClip);
    }
    public void PlayWrong()
    {
        audioSource.PlayOneShot(wrongClip);
    }
    public void PlayShield()
    {
        audioSource.PlayOneShot(shieldClip);
    }
}
