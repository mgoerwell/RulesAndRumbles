using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//class handling the keyboard navigation for the audio settings submenu
public class SettingsAudioKN : KeyNavMenu {
    //references to sliders for actual changes
    [SerializeField]
    private Slider Master, Music, Sounds;

    //reference to our sole actual button
    [SerializeField]
    private Button BackButton;

    //references to the containers for indicator placement
    [SerializeField]
    private GameObject Mas, Mus, Son;

    //tracker for menu items and keeping functionality clear
    protected enum SelOption { Master, Music, Sounds, Back }
    protected SelOption Current = SelOption.Master;

    //reference to our physical parameters
    private RectTransform rt;

    // Start is called before the first frame update
    void Start() {
        rt = GetComponent<RectTransform>();
        FastMod = 4;
    }

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

        //check to see if we're backing out
        if (InputManager.Instance.Control.Menu.Cancel.triggered) {
            GoBack();
        }

        //only call if we actually choose an option
        if (InputManager.Instance.Control.Menu.Confirm.triggered) {
            Select();
        }
    }

    //axial movement helpers

    protected override void MoveUp() {
        switch (Current) {
            case SelOption.Master:
                MoveBack();
                break;
            case SelOption.Music:
                MoveMast();
                break;
            case SelOption.Sounds:
                MoveMuse();
                break;
            case SelOption.Back:
                MoveSound();
                break;
            default:
                //do nothing
                break;
        }
    }

    protected override void MoveDown() {
        switch (Current) {
            case SelOption.Master:
                MoveMuse();
                break;
            case SelOption.Music:
                MoveSound();
                break;
            case SelOption.Sounds:
                MoveBack();
                break;
            case SelOption.Back:
                MoveMast();
                break;
            default:
                //do nothing
                break;
        }
    }

    protected override void MoveLeft() {
        switch (Current) {
            case SelOption.Master:
                DecreaseSilder(Master);
                break;
            case SelOption.Music:
                DecreaseSilder(Music);
                break;
            case SelOption.Sounds:
                DecreaseSilder(Sounds);
                break;
            default:
                //do nothing
                break;
        }
    }

    protected override void MoveRight() {
        switch (Current) {
            case SelOption.Master:
                IncreaseSilder(Master);
                break;
            case SelOption.Music:
                IncreaseSilder(Music);
                break;
            case SelOption.Sounds:
                IncreaseSilder(Sounds);
                break;
            default:
                //do nothing
                break;
        }
    }

    //generic move helper
    public override void MoveDirect(string Option) {
        switch (Option.ToLower()) {
            case "master":
                MoveMast();
                break;
            case "music":
                MoveMuse();
                break;
            case "sounds":
                MoveSound();
                break;
            case "back":
                MoveBack();
                break;
            default:
                //do nothing
                break;
        }
    }

    //button input handler
    protected override void Select() {
        if (Current == SelOption.Back) {
            GoBack();
        }
    }



    
    //generic movement helpers

    private void MoveMast() {
        Current = SelOption.Master;
        rt.position = new Vector3(rt.position.x, Mas.transform.position.y, rt.position.z);
    }

    private void MoveMuse() {
        Current = SelOption.Music;
        rt.position = new Vector3(rt.position.x, Mus.transform.position.y, rt.position.z);
    }

    private void MoveSound() {
        Current = SelOption.Sounds;
        rt.position = new Vector3(rt.position.x, Son.transform.position.y, rt.position.z);
    }

    private void MoveBack() {
        Current = SelOption.Back;
        rt.position = new Vector3(rt.position.x, BackButton.transform.position.y, rt.position.z);
    }

    //linkup for back button
    private void GoBack() {
        BackButton.onClick?.Invoke();
    }
}
