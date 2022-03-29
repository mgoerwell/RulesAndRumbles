using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSliderControl : MonoBehaviour
{
    public Text SliderText;
    private Slider MouseSlider;

    //called when made active
    private void Awake() {
        MouseSlider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start() {
        //set the slider to currently stored FoV setting if stored, else default
        MouseSlider.value = PlayerPrefs.GetFloat("MSense", 0.5f) * 10;
        SliderText.text = "" + MouseSlider.value; 
    }

}
