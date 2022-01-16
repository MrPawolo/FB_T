using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundPlay : MonoBehaviour
{
    [SerializeField] AudioClip[] sounds;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void PlayRandom()
    {
        if (sounds.Length == 0)
            return;
        int track = Random.Range(0, sounds.Length);
        audioSource.PlayOneShot(sounds[track]);
    }
}
