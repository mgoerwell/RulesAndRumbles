using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Class that handles keyboard navigtion for the settings Menu's save data submenu
public class SettingsSaveKN : KeyNavMenu {
    //handles for the buttons controlling events
    [SerializeField]
    private Button DelButton, BackButton;

    //tracker for menu items and keeping functionality clear
    private enum SelOption { Delete, Back }
    private SelOption Current = SelOption.Delete;

    //reference to our physical parameters
    private RectTransform rt;

    // Start is called before the first frame update
    void Start() {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update() {
        MenuMove();

        //check to see if we're backing out
        if (InputManager.Instance.Control.Menu.Cancel.triggered) {
            GoBack();
        }

        //only call if we actually choose an option
        if (InputManager.Instance.Control.Menu.Confirm.triggered)
        {
            Select();
        }
    }

    //overridden because of simplicity
    protected override void MenuMove() {
        //do not process if still in cooldown
        if (!CooldownCheck())
            return;

        //We only have two options so just swap
        if (InputManager.Instance.Control.Menu.Up.phase == UnityEngine.InputSystem.InputActionPhase.Started 
            || InputManager.Instance.Control.Menu.Down.phase == UnityEngine.InputSystem.InputActionPhase.Started) {
            MoveOther();
            CooldownReset();
        }
    }

    //middleman for swapping
    private void MoveOther() {
        switch (Current) {
            case SelOption.Delete:
                MoveBack();
                break;
            case SelOption.Back:
                MoveDel();
                break;
        }
    }

    //handle for mouse movement
    public override void MoveDirect(string Option) {
        switch (Option.ToLower()) {
            case "delete":
                MoveDel();
                break;
            case "back":
                MoveBack();
                break;
            default:
                Debug.Log("MenuLabel not found, check spelling: " + Option);
                break;
        }
    }

    //helper function that exits the settings menu
    private void GoBack() {
        InputManager.Instance.MenuOff();
        BackButton.onClick?.Invoke();
    }

    //called when we make a selection
    protected override void Select() {
        switch (Current) {
            case SelOption.Delete:
                DelButton.onClick?.Invoke();
                break;
            case SelOption.Back:
                GoBack();
                break;
        }
    }

    //navigation helper block

    //Delete button Helper
    private void MoveDel() {
        Current = SelOption.Delete;
        rt.position = new Vector3(rt.position.x, DelButton.transform.position.y, rt.position.z);
    }
    
    //Back Button Helper
    private void MoveBack() {
        Current = SelOption.Back;
        rt.position = new Vector3(rt.position.x, BackButton.transform.position.y, rt.position.z);
    }
}
