using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ControlItem : MonoBehaviour
{
    //text element reference
    public Text controlText;

    //Action and sub action name
    public string ActionName;

    //reference for input actions
    private InputActionReference action;
    
    private int bindIndex;

    

    // Start is called before the first frame update
    void Awake() {
           //based on the action name, get the correct action, and then find the correct bind index for display update
        switch (ActionName) {
            case "Jump":
                action = InputActionReference.Create(InputManager.Instance.Control.Gameplay.Jump);
                bindIndex = action.action.GetBindingIndexForControl(action.action.controls[0]);
                break;
            case "Use":
                action = InputActionReference.Create(InputManager.Instance.Control.Gameplay.Use);
                bindIndex = action.action.GetBindingIndexForControl(action.action.controls[0]);
                break;
            case "Up":
            case "Down":
            case "Left":
            case "Right":
                action = InputActionReference.Create(InputManager.Instance.Control.Gameplay.MoveAxis);
                bindIndex = action.action.bindings.IndexOf(x => x.isPartOfComposite && x.name == ActionName.ToLower());
                break;
            default:
                break;
        }
    }

    // Update is called once per frame, this gets the string of the current binding for the display, ommiting the device for brevity
    void Update() {
            controlText.text = InputControlPath.ToHumanReadableString(action.action.bindings[bindIndex].effectivePath,InputControlPath.HumanReadableStringOptions.OmitDevice);
    }
}
