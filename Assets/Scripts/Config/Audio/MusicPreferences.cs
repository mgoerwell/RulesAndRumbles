using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPreferences : MonoBehaviour
{
    private AudioSource BGM; //our music source

    //event handlers
    private void OnEnable() {
        ConfigEvents.UpdateMusic += UpdateMusicVol;
    }

    private void OnDisable() {
        ConfigEvents.UpdateMusic -= UpdateMusicVol;
    }

    //initial setup
    private void Start() {
        BGM = GetComponent<AudioSource>();
        UpdateMusicVol();
    }

    //helper method for updating midscene
    private void UpdateMusicVol() {
        float Volume = PlayerPrefs.GetFloat("MusicVolume", 1f) * PlayerPrefs.GetFloat("MasterVolume", 1f);
        BGM.volume = Volume;
    }
}
