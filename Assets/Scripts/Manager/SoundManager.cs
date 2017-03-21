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
    public AudioClip spawn;
    public AudioClip superPower;

    private AudioSource _audioSource;

    public enum soundToPlay
    {
        waveClear,
        explosion,
        spawn,
        superPower,

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
            case soundToPlay.spawn:
                _audioSource.PlayOneShot(spawn);
                break;
            case soundToPlay.superPower:
                _audioSource.PlayOneShot(superPower);
                break;
        }
    }

}
