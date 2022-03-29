using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//base class that serves as foundation for all menu keyboard navigation classes
public class KeyNavMenu : MonoBehaviour {
    //variables for managing cooldown of menu movement to prevent accidentally moving past desired option
    private readonly int MenuCooldown = 30; //delay before menu movement can be done again. Allows for beter user control
    private int CooldownTimer = 0; //tracker for applying cooldown
    protected int FastMod = 1;

    //Virtual methods that need to be overriden in child classes to be used: Mouseover Handle, Internal movement, selection
    public virtual void MoveDirect(string Option) { }
    protected virtual void MoveUp() { }
    protected virtual void MoveDown() { }
    protected virtual void MoveLeft() { }
    protected virtual void MoveRight() { }
    protected virtual void Select() { }

    //Enablime of menu controls, Disabling is handled whenever we leave the menu
    private void OnEnable() {
        InputManager.Instance.MenuON();
    }

    

    //helper method that handles reading inputs and cooldown managment
    protected virtual void MenuMove() {
        //do not process if still in cooldown
        if (!CooldownCheck())
            return;

        MoveVert();

    }

    //simplified handle for vertical menu movement
    protected void MoveVert() {
        //process movement
        if (InputManager.Instance.Control.Menu.Up.phase == UnityEngine.InputSystem.InputActionPhase.Started) {
            MoveUp();
            CooldownReset();
        }

        if (InputManager.Instance.Control.Menu.Down.phase == UnityEngine.InputSystem.InputActionPhase.Started) {
            MoveDown();
            CooldownReset();
        }
    }

    protected void MoveHor() {
        //process movement
        if (InputManager.Instance.Control.Menu.Left.phase == UnityEngine.InputSystem.InputActionPhase.Started) {
            MoveLeft();
            CooldownReset();
        }

        if (InputManager.Instance.Control.Menu.Right.phase == UnityEngine.InputSystem.InputActionPhase.Started) {
            MoveRight();
            CooldownReset();
        }
    }

    //slider helpers
    //generic helper for incrementing a given slider
    protected virtual void IncreaseSilder(Slider slider) {
        if (slider.value < slider.maxValue) {
            slider.value++;
        }
            

    }

    //generic helper for decrementing a given slider
    protected virtual void DecreaseSilder(Slider slider) {
        if (slider.value > slider.minValue) {
            slider.value--;
        } 
    }

    protected void FastCooldown() {
        if (FastMod != 0) {
            CooldownTimer /= FastMod;
        }
    }

    protected bool CooldownCheck(){
        //update timer
        if (CooldownTimer > 0) {
            CooldownTimer--;
            return false;
        }
        return true;
    }

    protected void CooldownReset() {
        CooldownTimer = MenuCooldown;
    }


}
