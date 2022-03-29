using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Class that exists to provide top level menu control for the settings scene
 * It has three different functions it needs to provide
 * 1. Updating the display to make it clear to the player which menu option they've selected
 * 2. Providing a method to switch between menu options without mouse-based controls
 * 3. Providing a handler for what each menu option should actually DO
 */
public class SettingsMenu : MonoBehaviour {
    public Button GraphicsButton, AudioButton, ControlsButton, SaveButton, ReturnButton; //buttons for determining what was clicked
    public GameObject GraphicsMenu, AudioMenu, ControlMenu, SaveMenu; //handles for enabling submenus
    private Vector3 OrigPos;                //original position of indicator to which this script is attached
    private float OptionsOffset = 1.65f;    //offset in unity space between options in the menu
    private int CurOption = 0;              //tracker for currently selected menu option
    private int NumOptions = 5;             //reference for number of options menu can cycle through. Important for wraparound
    private int MenuCooldown = 25;          //delay before menu movement can be done again. Allows for beter user control
    private int CooldownTimer = 0;          //tracker for applying cooldown
    private bool InSubMenu = false;         //Tracker for if we've entered on of the sub menus for individual setting groups

    // Use this for initialization
    void Start () {
        //add button click listeners
        GraphicsButton.onClick.AddListener(GoToGraphics);
        AudioButton.onClick.AddListener(GoToAudio);
        ControlsButton.onClick.AddListener(GoToControls);
        SaveButton.onClick.AddListener(GoToSave);
        ReturnButton.onClick.AddListener(GoToMain);
        //store indicator original position
        OrigPos = transform.position;
	}



    //method that sets position of indicator
    private void MoveIndicator(int x) {
        //quick catch to ensure we don't accidentally move through two menus at once
        if (InSubMenu) {
            return;
        }
        //update selected option
        CurOption = x;
        //set position of changed indicator
        Vector3 newPos = new Vector3(OrigPos.x, OrigPos.y - (OptionsOffset * CurOption), OrigPos.z);
        this.transform.position = newPos;
    }

    //method that lets us know we have control again
    private void UnSub () {
        InSubMenu = false;
    }

    // Update is called once per fixed interval, regardless of framerate
    void FixedUpdate () {
        MenuMovement();
        MenuSelect();
	}

    //helper function that controls keyboard movement through the menu
    private void MenuMovement() {
        //update timer
        if (CooldownTimer > 0) {
            CooldownTimer--;
            return;
        }

        //handle actual movement
        float MoveVertical = Input.GetAxis("Vertical"); // Get direction of movement for selected option

        if (MoveVertical != 0) {
            int x; //reference variable for what option should be set to
            if (MoveVertical > 0) { //movement is up IE: going backwards in iteration
                x = CurOption - 1; //get the value of the previous option
                if (x < 0) {
                    x = NumOptions - 1; //wrap around to last option in list if we underflow
                }
            } else { //Movement is down, IE: Going forwards in iteration
                x = CurOption + 1; //get value of next option
                if (x == NumOptions) {
                    x = 0; //wrap around to first option if overflow
                }
            }

            //actually perform movement
            MoveIndicator(x);
            //delay next available input
            CooldownTimer = MenuCooldown;
        }
    }

    //helper function that handles the actual selection of a menu item
    private void MenuSelect()
    {
        //quick check to make sure we're actually in this menu
        if (InSubMenu) {
            return;
        }

        if (Input.GetButtonUp("Jump")) {
            switch (CurOption) //this is super bad practice and should probably be replaced but int -> enum/enum -> int conversion a drag when I need the int values elsewhere
            {
                case 0:
                    GoToGraphics();
                    break;
                case 1:
                    GoToAudio();
                    break;
                case 2:
                    GoToControls();
                    break;
                case 3:
                    GoToSave();
                    break;
                case 4:
                    GoToMain();
                    break;
                default:
                    break;
            }
        }
    }

    //Helper functions for menu navigation

    private void GoToGraphics() {
        //disable while in a sub menu
        if (!InSubMenu) {
            GraphicsMenu.SetActive(true);
            InSubMenu = true;
        }
        
    }

    private void GoToAudio() {
        //disable in submenus
        if (!InSubMenu) {

        }
    }

    private void GoToControls() {
        //disable in submenus
        if (!InSubMenu) {

        }
    }

    private void GoToSave() {
        //disable in submenus
        if (!InSubMenu) {

        }
    }

    private void GoToMain(){
        //disable in sub menus
        if (!InSubMenu) {
            SceneManager.LoadScene("Main_Menu");
        }
    }
}
