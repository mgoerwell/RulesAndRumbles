using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//covers unique behaviour for the pause menu implementation (In-game) of the control menu
public class PauseControls : MonoBehaviour {
    [SerializeField]
    private Slider MSlider;
    [SerializeField]
    private GameObject MasterMenu;

    private InGameMenu igm;

    private void Start() {
        igm = MasterMenu.GetComponent<InGameMenu>();
    }

    public void ExitControls() {
        SaveControls();
        igm.OpenSubmenu(InGameMenu.SubMenu.Settings);
    }

    private void SaveControls() {
        PlayerPrefs.SetFloat("MSense", MSlider.value / 10);
        ConfigEvents.TriggerMouseSense();
    }
}
