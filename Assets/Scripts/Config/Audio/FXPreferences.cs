using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXPreferences : MonoBehaviour
{
    private AudioSource SoundFX; //our audio source

    //event handlers
    private void OnEnable() {
        ConfigEvents.UpdateSounds += UpdateSoundVol;
    }

    private void OnDisable() {
        ConfigEvents.UpdateSounds -= UpdateSoundVol;
    }

    //initial setup
    private void Start() {
        SoundFX = GetComponent<AudioSource>();
        UpdateSoundVol();
    }

    //helper method for updating midscene
    private void UpdateSoundVol() {
        float Volume = PlayerPrefs.GetFloat("SoundVolume", 1f) * PlayerPrefs.GetFloat("MasterVolume", 1f);
        SoundFX.volume = Volume;
    }
}
