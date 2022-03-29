using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSettingMenu : MonoBehaviour
{
    //references to the sub menus
    public GameObject GraphicsMenu;
    public GameObject AudioMenu;
    public GameObject ControlsMenu;
    public GameObject SaveMenu;
    [SerializeField]
    private GameObject Top;
    public static GameObject TopMenu;


    public void Start() {
        TopMenu = Top;
    }

    public void GoToGraphics() {
        GraphicsMenu.SetActive(true);
        TopMenu.SetActive(false);
    }

    public void GoToAudio() {
        AudioMenu.SetActive(true);
        TopMenu.SetActive(false);
    }

    public void GoToControls() {
        ControlsMenu.SetActive(true);
        TopMenu.SetActive(false);
    }

    public void GoToSaves() {
        SaveMenu.SetActive(true);
        TopMenu.SetActive(false);
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene("Main_Menu");
    }


    public static void LeaveSubMenu() {
        TopMenu.SetActive(true);
    }

}
