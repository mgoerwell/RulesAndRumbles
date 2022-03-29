using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//simple function to enable keyboard control in the credits
public class CreditsKN : KeyNavMenu {
    [SerializeField]
    private Button ExitButton;

    // Update is called once per frame
    void Update() {
        if (InputManager.Instance.Control.Menu.Confirm.triggered || InputManager.Instance.Control.Menu.Cancel.triggered) {
            Select();
        }
    }

    //Called when a key is pushed
    protected override void Select() {
        InputManager.Instance.MenuOff();
        ExitButton.onClick?.Invoke();
    }
}
