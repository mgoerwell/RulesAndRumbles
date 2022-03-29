using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//class handling the keyboard navigation for the graphics settings submenu
public class SettingsGraphicsKN : KeyNavMenu{
    //references to the slider and it's container for navigtion/effect references
    [SerializeField]
    private Slider FoVSlider;
    [SerializeField]
    private GameObject SlideBox;

    //reference to our toggle
    [SerializeField]
    private Toggle FullScreenTG;

    //button references for navigation/events
    [SerializeField]
    private Button LeftReso, RightReso, Back;

    //option trackers
    protected enum SelOption { FoV, FS, Reso, Back }
    protected SelOption Current = SelOption.FoV;

    //reference to our physical parameters
    private RectTransform rt;

    // Start is called before the first frame update
    private void Start() {
        rt = GetComponent<RectTransform>();
        FastMod = 4;
    }


    // Update is called once per frame
    void Update() {
        if (!CooldownCheck()) {
            return;
        }
        MoveHor();
        if (Current == SelOption.FoV) {
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

    protected override void Select() {
        switch (Current) {
            case SelOption.FS:
                FullScreenTG.isOn = !FullScreenTG.isOn;
                break;
            case SelOption.Back:
                GoBack();
                break;
            default: //the other two options don't really make sense to have a select option for
                break;
        }
    }

    //Navigation Control methods
    protected override void MoveUp() {
        switch (Current) {
            case SelOption.FoV:
                MoveBack();
                break;
            case SelOption.FS:
                MoveFoV();
                break;
            case SelOption.Reso:
                MoveFS();
                break;
            case SelOption.Back:
                MoveReso();
                break;
            default:
                break;
        }
    }

    protected override void MoveDown() {
        switch (Current) {
            case SelOption.FoV:
                MoveFS();
                break;
            case SelOption.FS:
                MoveReso();
                break;
            case SelOption.Reso:
                MoveBack();
                break;
            case SelOption.Back:
                MoveFoV();
                break;
            default:
                break;
        }
    }

    protected override void MoveLeft() {
        switch (Current) {
            case SelOption.FoV:
                DecreaseSilder(FoVSlider);
                break;
            case SelOption.Reso:
                LeftReso.onClick?.Invoke();
                break;
            default: //neither of the remaining options has a left right component
                break;
        }
    }

    protected override void MoveRight() {
        switch (Current) {
            case SelOption.FoV:
                IncreaseSilder(FoVSlider);
                break;
            case SelOption.Reso:
                RightReso.onClick?.Invoke();
                break;
            default: //neither of the remaining options has a left right component
                break;
        }
    }

    //direct movement, for mouse hover
    public override void MoveDirect(string Option) {
        switch (Option.ToLower()) {
            case "fov":
                MoveFoV();
                break;
            case "fs":
                MoveFS();
                break;
            case "reso":
                MoveReso();
                break;
            case "back":
                MoveBack();
                break;
            default:
                Debug.Log("Label not found, check spelling?");
                break;
        }
    }

    //movement helper methods, all follow the same pattern and move the indicator to the associated menu option

    private void MoveFoV() {
        Current = SelOption.FoV;
        rt.position = new Vector3(rt.transform.position.x, SlideBox.transform.position.y, rt.position.z);
    }

    private void MoveFS() {
        Current = SelOption.FS;
        rt.position = new Vector3(rt.transform.position.x, FullScreenTG.transform.position.y, rt.position.z);
    }

    private void MoveReso() {
        Current = SelOption.Reso;
        rt.position = new Vector3(rt.transform.position.x, LeftReso.transform.position.y, rt.position.z);
    }

    private void MoveBack() {
        Current = SelOption.Back;
        rt.position = new Vector3(rt.transform.position.x, Back.transform.position.y, rt.position.z);
    }

    //hookup for menu returns
    private void GoBack() {
        Back.onClick?.Invoke();
    }
}
