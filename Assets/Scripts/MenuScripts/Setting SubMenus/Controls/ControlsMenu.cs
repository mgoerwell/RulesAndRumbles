using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControlsMenu : MonoBehaviour
{
    //references to game objects
    public GameObject ControlMenu;
    public Slider MSlider;
    public Text MSliderText;

    //rebind operation reference
    private InputActionRebindingExtensions.RebindingOperation rebindOperation;

    private void OnEnable() {
        InputManager.Instance.GameplayOff();
    }


    //Generic for axis buttons
    public void CompButton(string Name) {
        int bindIndex = InputManager.Instance.Control.Gameplay.MoveAxis.bindings.IndexOf(x => x.isPartOfComposite && x.name == Name.ToLower());
        PerformRebind(InputManager.Instance.Control.Gameplay.MoveAxis, bindIndex);
    }

    //jump button rebind
    public void JumpButton() {
        int bindIndex = InputManager.Instance.Control.Gameplay.Jump.GetBindingIndexForControl(InputManager.Instance.Control.Gameplay.Jump.controls[0]);
        PerformRebind(InputManager.Instance.Control.Gameplay.Jump, bindIndex);
    }

    //Use button rebind
    public void UseButton() {
        int bindIndex = InputManager.Instance.Control.Gameplay.Use.GetBindingIndexForControl(InputManager.Instance.Control.Gameplay.Use.controls[0]);
        PerformRebind(InputManager.Instance.Control.Gameplay.Use, bindIndex);
    }

    //change monitor for mouse sensitivity Slider
    public void UpdateMsense() {
        //correct to within minmax values,just in case
        if (MSlider.value < MSlider.minValue)
        {
            MSlider.value = MSlider.minValue;
        } else if (MSlider.value > MSlider.maxValue)
        {
            MSlider.value = MSlider.maxValue;
        }

        //update label
        MSliderText.text = "" + MSlider.value;
    }

    //button for closing submenu
    public void CloseButton() {
        //save preferences
        PlayerPrefs.SetFloat("MSense",MSlider.value/10);
        ControlMenu.SetActive(false);
        MainSettingMenu.LeaveSubMenu();
    }

    //rebind prcoessing operations
    private void PerformRebind(InputAction action, int Bindindex) {
        InputManager.Instance.MenuOff(); //disable menu to prevent movement while rebinding
        ConfigEvents.TriggerControls();
        //perform rebind
        rebindOperation = action.PerformInteractiveRebinding(Bindindex)
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete())
            .OnCancel(operation => RebindComplete())
            .Start();
    }

    private void RebindComplete() {
        InputManager.Instance.MenuON(); //return menu control
        ConfigEvents.TriggerControls();
        rebindOperation.Dispose();
    }
}
