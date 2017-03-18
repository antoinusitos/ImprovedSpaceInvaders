using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public static SoundManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }

    public AudioClip waveClear;
    public AudioClip explosion;

    private AudioSource _audioSource;

    public enum soundToPlay
    {
        waveClear,
        explosion,

    };

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void playSound(soundToPlay toPlay)
    {
        switch(toPlay)
        {
            case soundToPlay.waveClear:
                _audioSource.PlayOneShot(waveClear);
                break;
            case soundToPlay.explosion:
                _audioSource.PlayOneShot(explosion);
                break;
        }
    }

}
