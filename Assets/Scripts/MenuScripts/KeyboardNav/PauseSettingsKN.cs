using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//port of the main setting menu to the in game menu, removes the return to top level on cancel, and the save menu
public class PauseSettingsKN : KeyNavMenu {
    //handles for the buttons controlling events
    [SerializeField]
    private Button GraphicsButton, Audio, Control, ReturnButton;

    //tracker for menu items and keeping functionality clear
    private enum SelOption { Graphics, Audio, Control, Return }
    private SelOption Current = SelOption.Audio;

    //reference to our physical parameters
    private RectTransform rt;

    // Start is called before the first frame update
    private void Start() {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    private void Update() {
        MenuMove();

        //only call if we actually choose an option
        if (InputManager.Instance.Control.Menu.Confirm.triggered) {
            Select();
        }
    }

    //menu movement handlers
    //upwards menu Movement
    protected override void MoveUp() {
        switch (Current) {
            case SelOption.Audio:
                MoveBack();
                break;
            case SelOption.Graphics:
                MoveAudio(); ;
                break;
            case SelOption.Control:
                MoveGraph();
                break;
            case SelOption.Return:
                MoveCTRL();
                break;
        }
    }

    //downwards menu movement
    protected override void MoveDown() {
        switch (Current) {
            case SelOption.Audio:
                MoveGraph();
                break;
            case SelOption.Graphics:
                MoveCTRL(); ;
                break;
            case SelOption.Control:
                MoveBack();
                break;
            case SelOption.Return:
                MoveAudio();
                break;
        }
    }

    //Direct Menu Movement (mouse over)
    public override void MoveDirect(string Option) {
        switch (Option.ToLower())
        {
            case "graphics":
                MoveGraph();
                break;
            case "audio":
                MoveAudio();
                break;
            case "controls":
                MoveCTRL();
                break;
            case "back":
                MoveBack();
                break;
            default:
                Debug.Log("MenuLabel not found, check spelling: " + Option);
                break;
        }
    }

    //method called when the user actually selects a menu option, works by determining the current menu item and then invoking the onClick method of the associated button
    protected override void Select() {
        switch (Current) {
            case SelOption.Graphics:
                GraphicsButton.onClick?.Invoke();
                break;
            case SelOption.Audio:
                Audio.onClick?.Invoke();
                break;
            case SelOption.Control:
                Control.onClick?.Invoke();
                break;
            case SelOption.Return:
                GoBack();
                break;
        }
    }

    //helper function that exits the settings menu
    private void GoBack() {
        ReturnButton.onClick?.Invoke();
    }

    //Navigation option Helper functions

    //graphics director
    private void MoveGraph() {
        Current = SelOption.Graphics;
        rt.position = new Vector3(rt.position.x, GraphicsButton.transform.position.y, rt.position.z);
    }

    //Audio director
    private void MoveAudio() {
        Current = SelOption.Audio;
        rt.position = new Vector3(rt.position.x, Audio.transform.position.y, rt.position.z);
    }

    //Controls Director
    private void MoveCTRL() {
        Current = SelOption.Control;
        rt.position = new Vector3(rt.position.x, Control.transform.position.y, rt.position.z);
    }

    //Return Director
    private void MoveBack() {
        Current = SelOption.Return;
        rt.position = new Vector3(rt.position.x, ReturnButton.transform.position.y, rt.position.z);
    }
}
