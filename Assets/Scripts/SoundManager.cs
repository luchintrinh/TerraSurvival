using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public enum SFXType { gunLaser, gunLaserUltimate, sword, swordUltimate, exploise, flash, run}
    
    public AudioClip[] sfx;


    public AudioSource gameMusic;

    [Range(0, 1)]
    public float volume;

    public bool muteSFX;
    public bool muteMusic;
    public void Init()
    {
        for(int i =0; i < sfx.Length; i++)
        {
            AudioSource audioSource= gameObject.AddComponent<AudioSource>();
            audioSource.clip = sfx[i];
            audioSource.playOnAwake = false;
        }
    }

    public void playSFX(SFXType SFX)
    {

        GetComponents<AudioSource>()[(int)SFX].PlayOneShot(sfx[(int)SFX]);
    }

    private void Start()
    {
        Init();
        ChangeSoundSetting();
    }

    public void ChangeSoundSetting()
    {
        AudioSource[] audios= GetComponents<AudioSource>();
        foreach(AudioSource audio in audios)
        {
            audio.mute = muteSFX;
            audio.volume = volume;
        }
        gameMusic.mute = muteMusic;
        gameMusic.volume = volume;
    }
}
