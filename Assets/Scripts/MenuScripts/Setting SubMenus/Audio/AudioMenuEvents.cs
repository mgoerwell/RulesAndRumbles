using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioMenuEvents : MonoBehaviour {

    //Volume Sliders
    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider SoundSlider;

    //Slider texts
    public Text MasterText;
    public Text MusicText;
    public Text SoundText;

    //handle for submenu
    public GameObject NoiseMenu;

    //method for monitoring changes to the Master Volume Slider
    public void UpdateMasterVolume() {
        //check constraints
        UpdateVolumeSlider(MasterSlider);
        //update label
        MasterText.text = "Master Volume: " + MasterSlider.value;
    }

    //method for monitoring changes to the Music Volume Slider
    public void UpdateMusicVolume() {
        //check constraints
        UpdateVolumeSlider(MusicSlider);
        //update label
        MusicText.text = "Music Volume: " + MusicSlider.value;
    }

    //method for monitoring changes to the Sounds Volume Slider
    public void UpdateSoundVolume() {
        //check constraints
        UpdateVolumeSlider(SoundSlider);
        //update label
        SoundText.text = "Sounds Volume: " + SoundSlider.value;
    }


    //method for closing submenu
    public void ExitAudio() {
        SaveVolumes();
        NoiseMenu.SetActive(false);
        MainSettingMenu.LeaveSubMenu();
    }

    //helper function for slider constraint control
    private void UpdateVolumeSlider(Slider slider) {
        //correct to within minmax values,just in case
        if (slider.value < slider.minValue) {
            slider.value = slider.minValue;
        } else if (slider.value > slider.maxValue) {
            slider.value = slider.maxValue;
        }
    }

    //helper function for saving volumes
    private void SaveVolumes() {
        //save master volume for future reference
        PlayerPrefs.SetFloat("MasterVolume", MasterSlider.value / 100);

        //extract from slider and save
        PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value / 100);

        //extract from slider and save
        PlayerPrefs.SetFloat("SoundVolume", SoundSlider.value / 100);

        //apply updates
        ConfigEvents.TriggerMusic();
        ConfigEvents.TriggerSounds();
    }
}
