using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Class that exists to handle Graphics Menu Management within the pause menu (IE mid-game). Heavily based on GraphicsEvents.cs but handles slightly different edge cases
public class PauseGraphics : MonoBehaviour
{
    //FoV controls
    public Slider FoVSlider;
    public Text SliderText;

    //reference to script for menu control
    [SerializeField]
    private GameObject MasterMenu;

    private InGameMenu igm;

    private void Start() {
        igm = MasterMenu.GetComponent<InGameMenu>();
    }

    //method for monitoring changes to FOV slider
    public void UpdateFoV() {
        //correct to within minmax values,just in case
        if (FoVSlider.value < FoVSlider.minValue) {
            FoVSlider.value = FoVSlider.minValue;
        } else if (FoVSlider.value > FoVSlider.maxValue) {
            FoVSlider.value = FoVSlider.maxValue;
        }

        //update label
        SliderText.text = "Field of View: " + FoVSlider.value;
    }


    public void ExitGraphics() {
        SaveGraphics();
        igm.OpenSubmenu(InGameMenu.SubMenu.Settings);
    }

    //helper function that handles all the saving and updating of graphics changes
    private void SaveGraphics() {
        //store data
        PlayerPrefs.SetFloat("FoV", FoVSlider.value); //field of view

        //update scene
        ConfigEvents.TriggerFoV();
    }
}
