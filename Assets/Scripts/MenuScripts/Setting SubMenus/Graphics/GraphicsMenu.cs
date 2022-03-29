using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Submenu controller for graphics options in the the settings menu 
 * provides control for:
 *      3D Camera FoV
 * 
 * as well as saving those settings to the user preferences and returning to the top level settings menu
 */
public class GraphicsMenu : MonoBehaviour {
    private enum SubOptions {FoVControl};
    public Button ReturnButton;             //reference for graphics buttons we need to handle
    public GameObject Menu;                 //Reference to the menu we're a part of
    public Slider FoVSlider;                //Reference to the FoV Slider
    private Vector3 OrigPos;                //original position of indicator to which this script is attached
    private float OptionsOffset = 3.0f;    //offset in unity space between options in the menu
    private int CurOption = 0;              //tracker for currently selected menu option
    private int NumOptions = 2;             //reference for number of options menu can cycle through. Important for wraparound
    private int MenuCooldown = 25;          //delay before menu movement can be done again. Allows for beter user control
    private int sliderCooldown = 10;        //delay before next slider tick
    private int CooldownTimer = 0;          //tracker for applying cooldown
    private bool InSubOption = false;       //tracker for menuCycling
    private SubOptions SubOption;           //tracker for selected subOption

    // Use this for initialization
    void Start () {
        //set up button controls
        ReturnButton.onClick.AddListener(ReturnToSettings);
        //store menu start position
        OrigPos = transform.position;
	}


    //method that sets position of indicator
    private void MoveIndicator(int x) {
        //quick catch to ensure we don't move while adjusting an option
        if (InSubOption) {
            return;
        }
        //update selected option
        CurOption = x;
        //set position of changed indicator
        Vector3 newPos = new Vector3(OrigPos.x, OrigPos.y - (OptionsOffset * CurOption), OrigPos.z);
        this.transform.position = newPos;
    }


    // Update is called at fixed intervals
    void FixedUpdate () {
        MenuMovement();
        MenuSelect();
        if (InSubOption) {
            switch (SubOption) {
                case SubOptions.FoVControl:
                    break;
                default:
                    break;
            }
        }
	}



    //MenuMovement
    //helper function that controls keyboard movement through the menu
    private void MenuMovement()
    {
        //update timer
        if (CooldownTimer > 0)
        {
            CooldownTimer--;
            return;
        }

        //handle actual movement
        float MoveVertical = Input.GetAxis("Vertical"); // Get direction of movement for selected option

        if (MoveVertical != 0)
        {
            int x; //reference variable for what option should be set to
            if (MoveVertical > 0)
            { //movement is up IE: going backwards in iteration
                x = CurOption - 1; //get the value of the previous option
                if (x < 0)
                {
                    x = NumOptions - 1; //wrap around to last option in list if we underflow
                }
            } else
            { //Movement is down, IE: Going forwards in iteration
                x = CurOption + 1; //get value of next option
                if (x == NumOptions)
                {
                    x = 0; //wrap around to first option if overflow
                }
            }

            //actually perform movement
            MoveIndicator(x);
            //delay next available input
            CooldownTimer = MenuCooldown;
        }
    }

    //MenuSelect
    //helper function that handles the actual selection of a menu item
    private void MenuSelect()
    {
        //quick check to make sure we're navigating menu and not cycling an option
        if (InSubOption) {
            return;
        }

        if (Input.GetButtonUp("Jump"))
        {
            switch (CurOption) //this is super bad practice and should probably be replaced but int -> enum/enum -> int conversion a drag when I need the int values elsewhere
            {
                case 0:
                    InSubOption = true;
                    SubOption = SubOptions.FoVControl;
                    break;
                case 1:
                    ReturnToSettings();
                    break;
                default:
                    break;
            }
        }
    }
    //helper function for FoV Slider selection
    private void SliderControl() {

        //signal that we are done in this option on keyboard
        if (Input.GetButtonUp("Jump")) {
            InSubOption = false;
            return;
        }

        //update timer
        if (CooldownTimer > 0) {
            CooldownTimer--;
            return;
        }

        //handle slider change
        float MoveHorizontal = Input.GetAxis("Horizontal"); //get direction of movement for slider
        if (MoveHorizontal != 0) {
            if (MoveHorizontal < 0) { //user is decreasing fov
                FoVSlider.value--;
            } else { //user is increasing fov
                FoVSlider.value++;
            }
            CooldownTimer = sliderCooldown;
        }
    }

    //helper for returning to top level settings
    private void ReturnToSettings() {
        if (!InSubOption) {
            Menu.SetActive(false);
        }
    }
}
