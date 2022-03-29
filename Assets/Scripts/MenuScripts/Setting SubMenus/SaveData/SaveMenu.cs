using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveMenu : MonoBehaviour {

    public GameObject SavingMenu;
    public Text ResultsText;


    //clear save data
    public void DeleteSave() {
        ResultsText.text = SaveData.DeleteSave();
    }

    //exit Save Menu
    public void ExitSaves() {
        SavingMenu.SetActive(false);
        MainSettingMenu.LeaveSubMenu();
    }
}
