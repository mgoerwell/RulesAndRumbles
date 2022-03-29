using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//port of the Audio setting menu to the in game menu, removes the return to top level on cancel
public class PauseAudioKN : SettingsAudioKN {

    // Update is called once per frame
    void Update() {
        if (!CooldownCheck()) {
            return;
        }
        MoveHor();

        if (Current != SelOption.Back) {
            FastCooldown();
        }
        MoveVert();

        //only call if we actually choose an option
        if (InputManager.Instance.Control.Menu.Confirm.triggered) {
            Select();
        }
    }
}
