using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource vfxAudioSource;

    public AudioClip musicClip;
    private void Start()
    {
        audioSource.clip = musicClip;
        audioSource.Play();
    }
    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
