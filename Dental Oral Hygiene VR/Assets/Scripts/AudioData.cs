using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioData 
{
    // Audio Manager Data
    public static float musicVolume = 0.15f;
    public static float effectsVolume = 0.5f;

    public static void SaveAudioData() {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("effectsVolume", effectsVolume);
    }

    public static void LoadAudioData() {
        musicVolume = PlayerPrefs.GetFloat("musicVolume");
        effectsVolume = PlayerPrefs.GetFloat("effectsVolume");
    }
}
