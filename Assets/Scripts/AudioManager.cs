using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource , bulletsfxSource , footstepsfxSource , reloadsfxSource;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        //PlayMusic("BackGroundMusic");
    }
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, c => c.name == name);
        if (s == null)
        {
            Debug.Log("Can't find the music");

        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }

    }
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, c => c.name == name);
        if (s == null)
        {
            Debug.Log("Can't find the SFX");
        }
        else
        {
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }
    }

    public void PlayBulletSFX(string name)
    {
        Sound s = Array.Find(sfxSounds, c => c.name == name);
        if (s == null)
        {
            Debug.Log("Can't find the SFX");
        }
        else
        {
            bulletsfxSource.clip = s.clip;
            bulletsfxSource.Play();
        }
    }

    public void PlayFootStepsSFX(string name)
    {
        Sound s = Array.Find(sfxSounds, c => c.name == name);
        if (s == null)
        {
            Debug.Log("Can't find the SFX");
        }
        else
        {
            footstepsfxSource.clip = s.clip;
            footstepsfxSource.Play();
        }
    }

    public void StopFootStepsSFX(string name)
    {
        Sound s = Array.Find(sfxSounds, c => c.name == name);
        if (s == null)
        {
            Debug.Log("Can't find the SFX");
        }
        else
        {
            footstepsfxSource.clip = s.clip;
            footstepsfxSource.Stop();
        }
    }

    public void PlayReloadSFX(string name)
    {
        Sound s = Array.Find(sfxSounds, c => c.name == name);
        if (s == null)
        {
            Debug.Log("Can't find the SFX");
        }
        else
        {
            reloadsfxSource.clip = s.clip;
            reloadsfxSource.Play();
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
        bulletsfxSource.mute = !bulletsfxSource.mute;
        footstepsfxSource.mute = !footstepsfxSource.mute;
        reloadsfxSource.mute = !reloadsfxSource.mute;
    }
}
