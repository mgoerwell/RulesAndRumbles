using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//port of the main menu keyboard Nav, adapted for use within the in game menu
public class PauseTopKN : KeyNavMenu {
    //handles for the buttons controlling events
    [SerializeField]
    private Button Resume, MainMenu, Settings, QuitButton;

    //tracker for menu items and keeping functionality clear
    private enum SelOption { Resume, MainMenu, Settings, Quit }
    private SelOption Current = SelOption.Resume;

    //reference to our physical parameters
    private RectTransform rt;

    private void Start() {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update() {
        //check if we've gone to a new option
        MenuMove();

        //only call if we actually choose an option
        if (InputManager.Instance.Control.Menu.Confirm.triggered) {
            Select();
        }
    }

    //helper for upwards menu movement
    protected override void MoveUp() {
        switch (Current)
        {
            case SelOption.Resume:
                MoveQuit();
                break;
            case SelOption.MainMenu:
                MoveResume();
                break;
            case SelOption.Settings:
                MoveMainMenu();
                break;
            case SelOption.Quit:
                MoveSettings();
                break;
        }
    }
    //helper for downwards menu movement
    protected override void MoveDown() {
        switch (Current){
            case SelOption.Resume:
                MoveMainMenu();
                break;
            case SelOption.MainMenu:
                MoveSettings();
                break;
            case SelOption.Settings:
                MoveQuit();
                break;
            case SelOption.Quit:
                MoveResume();
                break;
        }
    }

    //helper for mouse hover events
    public override void MoveDirect(string Option) {
        switch (Option.ToLower()) {
            case "resume":
                MoveResume();
                break;
            case "main":
                MoveMainMenu();
                break;
            case "settings":
                MoveSettings();
                break;
            case "quit":
                MoveQuit();
                break;
            default:
                Debug.Log("MenuLabel not found, check spelling: " + Option);
                break;
        }
    }

    //block of helper functions that handle changes in menu position

    //helper function for when other commands move the cursor to Lvl select
    private void MoveResume() {
        Current = SelOption.Resume;
        rt.position = new Vector3(rt.position.x, Resume.transform.position.y, rt.position.z);
    }

    //helper function for when other commands move the cursor to Credits
    private void MoveMainMenu() {
        Current = SelOption.MainMenu;
        rt.position = new Vector3(rt.position.x, MainMenu.transform.position.y, rt.position.z);
    }

    //helper function for when other commands move the cursor to settings
    private void MoveSettings() {
        Current = SelOption.Settings;
        rt.position = new Vector3(rt.position.x, Settings.transform.position.y, rt.position.z);
    }

    //helper function for when other commands move the cursor to quit
    private void MoveQuit() {
        Current = SelOption.Quit;
        rt.position = new Vector3(rt.position.x, QuitButton.transform.position.y, rt.position.z);
    }


    //method called when the user actually selects a menu option, works by determining the current menu item and then invoking the onClick method of the associated button
    protected override void Select() {
        switch (Current) {
            case SelOption.Resume:
                Resume.onClick?.Invoke();
                break;
            case SelOption.MainMenu:
                MainMenu.onClick?.Invoke();
                break;
            case SelOption.Settings:
                Settings.onClick?.Invoke();
                break;
            case SelOption.Quit:
                QuitButton.onClick?.Invoke();
                break;
        }
    }
}
