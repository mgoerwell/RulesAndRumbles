using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//singleton to allow rebindings to carry across scenes
public class InputManager : Singleton<InputManager>
{
    //protected constructor (required to ensure remains singleton)
    protected InputManager() { }

    // Control Scheme object
    public PlayerActions Control;

    //method for enabling all gameplay controls 
    public void GameplayOn() {
        Control.Gameplay.Enable();
        Control.Gameplay.MoveAxis.Enable();
        Control.Gameplay.CameraAxis.Enable();
        Control.Gameplay.Jump.Enable();
        Control.Gameplay.Use.Enable();
        Control.Gameplay.Quit.Enable();
    }

    //method for disabling all gameplay controls
    public void GameplayOff() {
        Control.Gameplay.MoveAxis.Disable();
        Control.Gameplay.CameraAxis.Disable();
        Control.Gameplay.Jump.Disable();
        Control.Gameplay.Use.Disable();
        Control.Gameplay.Quit.Disable();
        Control.Gameplay.Disable();
    }

    //method for enabling all Menu Controls
    public void MenuON() {
        Control.Menu.Enable();
        Control.Menu.Up.Enable();
        Control.Menu.Down.Enable();
        Control.Menu.Left.Enable();
        Control.Menu.Right.Enable();
        Control.Menu.Confirm.Enable();
        Control.Menu.Cancel.Enable();
    }

    //method for disabling all menu controls
    public void MenuOff(){
        Control.Menu.Up.Disable();
        Control.Menu.Down.Disable();
        Control.Menu.Left.Disable();
        Control.Menu.Right.Disable();
        Control.Menu.Confirm.Disable();
        Control.Menu.Cancel.Disable();
        Control.Menu.Disable();
    }

    //method for diabling the control object
    public void NoControl() {
        Control.Disable();
    }

    //unique singleton requirements
    public override void Awake() {
        base.Awake();
        Control = new PlayerActions();
        Control.Enable();
    }

    private void OnApplicationQuit() {
        NoControl();
    }

}
