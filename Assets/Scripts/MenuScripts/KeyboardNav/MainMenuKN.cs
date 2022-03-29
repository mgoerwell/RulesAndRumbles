using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Class exists to provide input recognition (Keyboard Navigation) for the player in the main menu.
 * It has three different functions it needs to provide
 * 1. Updating the display to make it clear to the player which menu option they've selected
 * 2. Providing a method to switch between menu options without mouse-based controls
 * 3. Providing a handler for what each menu option should actually DO
 */
public class MainMenuKN : KeyNavMenu {
    //handles for the buttons controlling events
    [SerializeField]
    private Button GameButton, SettingsButton, CreditsButton, QuitButton;

    //tracker for menu items and keeping functionality clear
    private enum SelOption {Level,Settings,Credits,Quit}
    private SelOption Current = SelOption.Level;

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
        switch (Current) {
            case SelOption.Level:
                MoveQuit();
                break;
            case SelOption.Settings:
                MoveLevel();
                break;
            case SelOption.Credits:
                MoveSettings();
                break;
            case SelOption.Quit:
                MoveCredits();
                break;
        }
    }
    //helper for downwards menu movement
    protected override void MoveDown() {
        switch (Current) {
            case SelOption.Level:
                MoveSettings();
                break;
            case SelOption.Settings:
                MoveCredits();
                break;
            case SelOption.Credits:
                MoveQuit();
                break;
            case SelOption.Quit:
                MoveLevel();
                break;
        }
    }

    //helper for mouse hover events
    public override void MoveDirect(string Option) {
        switch (Option.ToLower()) {
            case "level":
                MoveLevel();
                break;
            case "settings":
                MoveSettings();
                break;
            case "credits":
                MoveCredits();
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
    private void MoveLevel() {
        Current = SelOption.Level;
        rt.position = new Vector3(rt.position.x, GameButton.transform.position.y, rt.position.z);
    }

    //helper function for when other commands move the cursor to settings
    private void MoveSettings() {
        Current = SelOption.Settings;
        rt.position = new Vector3(rt.position.x,SettingsButton.transform.position.y,rt.position.z);
    }

    //helper function for when other commands move the cursor to Credits
    private void MoveCredits() {
        Current = SelOption.Credits;
        rt.position = new Vector3(rt.position.x, CreditsButton.transform.position.y, rt.position.z);
    }

    //helper function for when other commands move the cursor to quit
    private void MoveQuit() {
        Current = SelOption.Quit;
        rt.position = new Vector3(rt.position.x, QuitButton.transform.position.y, rt.position.z);
    }


    //method called when the user actually selects a menu option, works by determining the current menu item and then invoking the onClick method of the associated button
    protected override void Select() {
        InputManager.Instance.MenuOff();
        switch (Current) {
            case SelOption.Level:
                GameButton.onClick?.Invoke();
                break;
            case SelOption.Settings:
                SettingsButton.onClick?.Invoke();
                break;
            case SelOption.Credits:
                CreditsButton.onClick?.Invoke();
                break;
            case SelOption.Quit:
                QuitButton.onClick?.Invoke();
                break;
        }
    }
    
}
