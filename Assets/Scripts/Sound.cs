using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{

    static Sound instance;
    public static Sound Instance
    {
        get
        {
            if (instance != null) return instance;
            instance = FindObjectOfType<Sound>();

            if (instance == null)
            {
                return null;
            }

            return instance;

        }
    }

    [SerializeField] AudioSource diggingSound;
    [SerializeField] AudioSource flyingSound;
    AudioSource currentAudioSource;
    private bool isFadingIn = false;
    private bool isFadingOut = false;
    private float lerpDuration = 0.4f; // duration of the lerp
    private float lerpStartTime;
    private float targetVolume;

    public void PlayDiggingSound(float targetVolume)
    {
        Debug.Log("Starting sound with volume transition for DIGGING");
        StopCurrentSound();
        StartFadeIn(diggingSound, targetVolume);
    }

    public void PlayFlyingSound(float targetVolume)
    {
        Debug.Log("Starting sound with volume transition for FLYING");
        StopCurrentSound();
        StartFadeIn(flyingSound, targetVolume);
    }

    private void StartFadeIn(AudioSource audioSource, float targetVolume)
    {
        lerpStartTime = Time.time;
        isFadingIn = true;
        isFadingOut = false; // Cancel any fade out
        this.targetVolume = targetVolume;
        currentAudioSource = audioSource;
        currentAudioSource.Play();
    }

    public void StopSound()
    {
        Debug.Log("Stopping all sounds with volume transition");
        targetVolume = 0;
        lerpStartTime = Time.time;
        isFadingOut = true;
    }

    public void StopCurrentSound()
    {
        if (currentAudioSource != null)
        {
            Debug.Log("Stopping current sound with volume transition");
            targetVolume = 0;
            lerpStartTime = Time.time;
            isFadingOut = true;
        }
    }

    void Update()
    {
        if (isFadingIn)
        {
            PerformFadeIn();
        }
        if (isFadingOut)
        {
            PerformFadeOut();
        }

    }

    private void PerformFadeIn()
    {
        float timeSinceStarted = Time.time - lerpStartTime;
        float percentageComplete = timeSinceStarted / lerpDuration;

        currentAudioSource.volume = Mathf.Lerp(currentAudioSource.volume, targetVolume, percentageComplete);

        if (percentageComplete >= 1.0f)
        {
            isFadingIn = false;
        }
    }

    private void PerformFadeOut()
    {
        float timeSinceStarted = Time.time - lerpStartTime;
        float percentageComplete = timeSinceStarted / lerpDuration;

        if (currentAudioSource != null)
        {
            currentAudioSource.volume = Mathf.Lerp(currentAudioSource.volume, targetVolume, percentageComplete);

            if (percentageComplete >= 1.0f)
            {
                isFadingOut = false;
                currentAudioSource.Stop();
            }
        }
        else
        {
            diggingSound.volume = Mathf.Lerp(diggingSound.volume, targetVolume, percentageComplete);
            flyingSound.volume = Mathf.Lerp(flyingSound.volume, targetVolume, percentageComplete);

            if (percentageComplete >= 1.0f)
            {
                isFadingOut = false;
                diggingSound.Stop();
                flyingSound.Stop();
            }
        }
    }

    public void MuffleSoundsForPause()
    {
        diggingSound.volume = diggingSound.volume / 10;
        flyingSound.volume = flyingSound.volume / 10;
    }

    public void UnMuffleSoundsForPause()
    {
        diggingSound.volume = diggingSound.volume * 10;
        flyingSound.volume = flyingSound.volume * 10;
    }
}


