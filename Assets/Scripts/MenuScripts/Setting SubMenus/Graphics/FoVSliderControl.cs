using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//simple class for handling FoV changes
public class FoVSliderControl : MonoBehaviour {
    public Text SliderText;
    private Slider FoVSlider;

    private void Awake() {
        FoVSlider = GetComponent<Slider>();
    }

    private void Start() {
        //set the slider to currently stored FoV setting if stored, else default
        FoVSlider.value = PlayerPrefs.GetFloat("FoV",90);
        SliderText.text = "Field of View: " + FoVSlider.value;
    }

}
