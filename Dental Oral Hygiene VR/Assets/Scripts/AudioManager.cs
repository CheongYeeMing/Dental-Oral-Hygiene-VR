using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private const float SOUND_PITCH = 1;

    [SerializeField] Slider MusicVolumeSlider;
    [SerializeField] Slider EffectsVolumeSlider;

    private float previousMusicVolume;
    private float previousEffectsVolume;

    public Sound[] musicList;
    public Sound[] effectsList;

    void Awake()
    {
        GetSavedVolumeData();
        SetSavedVolumeData();
        Initialise(MusicVolumeSlider, musicList);
        Initialise(EffectsVolumeSlider, effectsList);
    }

    void Start() 
    {
        string scene = SceneChanger.Instance.GetScene();
        if (scene == "Bathroom") 
        {
            StartCoroutine(PlayMusic("bathroom"));
        } 
        else 
        {
            StartCoroutine(PlayMusic("jawmodel"));
        }
    }

    private void GetSavedVolumeData()
    {
        MusicVolumeSlider.value = AudioData.musicVolume;
        EffectsVolumeSlider.value = AudioData.effectsVolume;
    }

    private void SetSavedVolumeData() 
    {
        previousMusicVolume = MusicVolumeSlider.value;
        previousEffectsVolume = EffectsVolumeSlider.value;
    }

    private void Initialise(Slider slider, Sound[] sounds) 
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.pitch = SOUND_PITCH;
            s.source.loop = s.loop;

            s.source.volume = slider.value;
        }
    }

    void Update() 
    {
        if (MusicVolumeChanged()) 
        {
            UpdateMusicVolume();
        }
        if (EffectsVolumeChanged()) 
        {
            UpdateEffectsVolume();
        }
    }

    private bool MusicVolumeChanged()
    {
        return MusicVolumeSlider.value != previousMusicVolume;
    }

    private bool EffectsVolumeChanged()
    {
        return EffectsVolumeSlider.value != previousEffectsVolume;
    }

    private void UpdateMusicVolume()
    {
        previousMusicVolume = MusicVolumeSlider.value;
        AudioData.musicVolume = previousMusicVolume;
        foreach (Sound s in musicList)
        {
            s.source.volume = previousMusicVolume;
        }
    }

    private void UpdateEffectsVolume()
    {
        previousEffectsVolume = EffectsVolumeSlider.value;
        AudioData.effectsVolume = previousEffectsVolume;
        foreach (Sound s in effectsList)
        {
            s.source.volume = previousEffectsVolume;
        }
    }

    public IEnumerator PlayMusic(string name)
    {
        Sound s = Array.Find(musicList, sound => sound.name == name);
        if (s == null || s.source.isPlaying) yield return null;
        s.source.volume = 0;
        s.source.Play();
        // Gradually increase volume of current Music
        while (s.source.volume < MusicVolumeSlider.value)
        {
            s.source.volume += 0.001f;
            yield return null;
        }

        s.source.volume = MusicVolumeSlider.value;
    }

    public IEnumerator StopMusic(string name)
    {
        Sound s = Array.Find(musicList, sound => sound.name == name);
        if (s == null) yield return null;
        // Gradually decrease volume of current Music
        while (s.source.volume > 0)
        {
            s.source.volume -= 0.001f;
            yield return null;
        }
        s.source.Stop();
    }

    public void PlayEffect(string name)
    {
        Sound s = Array.Find(effectsList, sound => sound.name == name);
        if (s == null || s.source.isPlaying) return;
        s.source.Play();
    }

    public void StopEffect(string name)
    {
        Sound s = Array.Find(effectsList, sound => sound.name == name);
        if (s == null) return;
        s.source.Stop();
    }
}
