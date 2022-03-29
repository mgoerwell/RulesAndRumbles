using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// A simple helper class to prevent us from triggering other menu items while a keybind is taking place
public class InteractToggle : MonoBehaviour {
    [SerializeField]
    private Selectable SelObj; //the menu object

    //event subscriber
    private void OnEnable() {
        ConfigEvents.ToggleControls += Toggle;
    }

    //event unsubscriber
    private void OnDisable() {
        ConfigEvents.ToggleControls -= Toggle;
    }

    //interactive toggle
    private void Toggle() {
        SelObj.interactable = !SelObj.interactable;
    }
}
