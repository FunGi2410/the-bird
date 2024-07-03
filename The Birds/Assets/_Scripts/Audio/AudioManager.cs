using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] AudioSource musicSource;
    public AudioClip bgMusic;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    public Sound[] sounds;

    private void Awake()
    {
        Instance = this;
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        this.musicSource.clip = bgMusic;
        this.musicSource.Play();
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void SetMusicVolume()
    {
        musicSource.volume = musicSlider.value;
    }
    public void SetSFxVolume()
    {
        //sfxSource.volume = sfxSlider.value;
        foreach (Sound s in sounds)
        {
            s.source.volume = sfxSlider.value;
        }
    }
}
