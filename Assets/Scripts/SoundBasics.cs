using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBasics : MonoBehaviour
{
    public AudioClip loopedClip; // Assign in Unity Editor
    public float loopedVolume = 1.0f; // Volume control for looped sound, visible in Unity Editor

    public AudioClip oneShotClip; // Assign in Unity Editor
    public float oneShotVolume = 1.0f; // Volume control for one-shot sound, visible in Unity Editor

    public List<AudioClip> randomSounds; // Assign in Unity Editor

    private AudioSource loopedSource; // Reference to the AudioSource for looped sound

    void Start()
    {
        // Play looped sound with specified volume
        PlayLoopedSound(loopedVolume);
    }

    // Plays a sound looped with volume control
    void PlayLoopedSound(float volume)
    {
        loopedSource = gameObject.AddComponent<AudioSource>();
        loopedSource.clip = loopedClip;
        loopedSource.loop = true; // Enable looping
        loopedSource.volume = volume; // Set volume
        loopedSource.Play();
    }

    // Sets the volume of the looped sound
    public void SetLoopedVolume(float volume)
    {
        if (loopedSource != null)
        {
            loopedSource.volume = volume;
        }
    }

    // Plays a one-shot sound with volume control
    public void PlayOneShotSound()
    {
        PlaySound(oneShotClip, oneShotVolume); // Use the specified volume for the one-shot sound
    }

    // Plays a random sound from the pool
    public void PlayRandomSound()
    {
        int index = Random.Range(0, randomSounds.Count);
        PlaySound(randomSounds[index], 1f); // Assuming full volume for random sounds, can be adjusted similarly
    }

    // General method to play a sound with given volume
    private void PlaySound(AudioClip clip, float volume)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.playOnAwake = false;
        source.spatialBlend = 1.0f; // Ensure the sound is played in 3D space
        source.Play();

        // Destroy the AudioSource component after the clip has finished playing
        Destroy(source, clip.length);
    }
}
