using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Sounds
{
    Positive,
    FuelLow,
    CargoFull,
    Fall,
    Teleporter,
    CashRegister,
    Explosion
}


public class SoundManager : MonoBehaviour
{
    static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance != null) return instance;
            instance = FindObjectOfType<SoundManager>();

            if (instance == null)
            {
                Debug.LogError("No Client Singleton in the scene");
                return null;
            }

            return instance;

        }
    }

    [SerializeField] AudioSource audiosource;
    [SerializeField] AudioSource soundTrack;

    [SerializeField] AudioClip positive;
    [SerializeField] AudioClip fuelLow;
    [SerializeField] AudioClip cargoFull;
    [SerializeField] AudioClip fall;
    [SerializeField] AudioClip teleporter;
    [SerializeField] AudioClip cashRegister;
    [SerializeField] AudioClip explosion;

    [SerializeField, Range(0f, 1f)] float startingMusicVolume;
    [Range(0f, 1f)] float startingAudiosourceVolume;

    private void OnEnable()
    {
        ResourcesManager.MoneyMoved += ResourcesManager_MoneyMoved;
    }

    private void OnDisable()
    {
        ResourcesManager.MoneyMoved -= ResourcesManager_MoneyMoved;        
    }

    private void ResourcesManager_MoneyMoved()
    {
        PlaySound(Sounds.CashRegister);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        startingAudiosourceVolume = audiosource.volume;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartSoundtrack();
    }


    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }




    void StartSoundtrack()
    {
        soundTrack.Play();
    }

    public void PlaySound(Sounds sound)
    {
        //if (Pause.IsPaused) return;
        AudioClip clipToPlay = sound switch
        {
            Sounds.Positive => positive,
            Sounds.FuelLow => fuelLow,
            Sounds.CargoFull => cargoFull,
            Sounds.Fall => fall,
            Sounds.Teleporter => teleporter,
            Sounds.CashRegister => cashRegister,
            Sounds.Explosion => explosion,
            _ => positive
        };
        Debug.Log("PLAY");
        float volume;
        if(sound == Sounds.CashRegister)
        {
            volume = 0.3f; 
        }
        else
        {
            volume = 0.8f;
        }
        audiosource.PlayOneShot(clipToPlay, volume);
    }

    public void ToggleMusicVolume(bool isQuiet)
    {
        float temporarySFXVolume = audiosource.volume;

        if (isQuiet)
        {
            soundTrack.volume = soundTrack.volume / 3;
            //audiosource.volume = soundTrack.volume / 3;
        }
        else
        {
            soundTrack.volume = startingMusicVolume;
           // audiosource.volume = temporarySFXVolume;
        }
    }

}


