using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderControl : MonoBehaviour
{
    [SerializeField]
    private Text SliderText;
    [SerializeField]
    private string SliderType;
    private Slider VolumeSlider;

    //called when made active
    private void Awake() {
        VolumeSlider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start() {
        //set the slider to currently stored Volume setting if stored, else default
        VolumeSlider.value = PlayerPrefs.GetFloat(SliderType, 1f) * 100;
        SliderText.text += VolumeSlider.value;
    }

}
