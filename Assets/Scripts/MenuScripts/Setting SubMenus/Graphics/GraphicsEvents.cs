using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsEvents : MonoBehaviour
{
    //FoV controls
    public Slider FoVSlider;
    public Text SliderText;

    //handle for the submenu
    public GameObject GraphMenu;

    //method for monitoring changes to FOV slider
    public void UpdateFoV() {
        //correct to within minmax values,just in case
        if (FoVSlider.value < FoVSlider.minValue)
        {
            FoVSlider.value = FoVSlider.minValue;
        } else if (FoVSlider.value > FoVSlider.maxValue)
        {
            FoVSlider.value = FoVSlider.maxValue;
        }

        //update label
        SliderText.text = "Field of View: " + FoVSlider.value;
    }

    //store all changes into player prefs, close submenu
    public void ExitGraphics()
    {
        PlayerPrefs.SetFloat("FoV", FoVSlider.value);
        GraphMenu.SetActive(false);
        MainSettingMenu.LeaveSubMenu();
    }
}
