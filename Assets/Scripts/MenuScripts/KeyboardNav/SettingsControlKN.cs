using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//class handling the keyboard navigation for the control settings submenu
public class SettingsControlKN : KeyNavMenu {

    //button references for event use
    [SerializeField]
    private Button Forward, Back, Left, Right, Jump, Use, Return;

    //reference to slider for actual value changes
    [SerializeField]
    private Slider MsenseSlider;

    //navigation references
    [SerializeField]
    private GameObject LeftCol, RightCol, MidCol, SlideBox;

    //option trackers
    protected enum SelOption { Forward, Back, Left, Right, Jump, Use, Mouse, Return }
    protected SelOption Current = SelOption.Forward;

    //reference to our physical parameters
    private RectTransform rt;

    //reference for suspending mouseovers
    private bool CanMouse = true;

    //event handlers
    private void OnEnable() {
        ConfigEvents.ToggleControls += ToggleMouseControl;
    }

    private void OnDisable() {
        ConfigEvents.ToggleControls -= ToggleMouseControl;
    }
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
        if (Current == SelOption.Mouse) {
            FastCooldown();
        }

        MoveVert();

        //check to see if we're backing out
        if (InputManager.Instance.Control.Menu.Cancel.triggered) {
            MenuReturn();
        }

        //only call if we actually choose an option
        if (InputManager.Instance.Control.Menu.Confirm.triggered) {
            Select();
        }
    }


    //navigation control methods
    protected override void MoveUp() {
        switch (Current) {
            case SelOption.Forward: //purposely left empty, the top two buttons both converge on moving up
            case SelOption.Back:
                MoveReturn();
                break;
            case SelOption.Left:
                MoveForward();
                break;
            case SelOption.Right:
                MoveBack();
                break;
            case SelOption.Jump:
                MoveLeftButton();
                break;
            case SelOption.Use:
                MoveRightButton();
                break;
            case SelOption.Mouse:
                MoveJump();
                break;
            case SelOption.Return:
                MoveMouse();
                break;
            default:
                break;
        }
    }

    protected override void MoveDown() {
        switch (Current) {
            case SelOption.Forward:
                MoveLeftButton();
                break;
            case SelOption.Back:
                MoveRightButton();
                break;
            case SelOption.Left:
                MoveJump();
                break;
            case SelOption.Right:
                MoveUse();
                break;
            case SelOption.Jump: //purposely left empty, the top bottom control buttons both converge on moving down
            case SelOption.Use:
                MoveMouse();
                break;
            case SelOption.Mouse:
                MoveReturn();
                break;
            case SelOption.Return:
                MoveForward();
                break;
            default:
                break;
        }
    }

    protected override void MoveLeft() {
        switch (Current) {
            case SelOption.Forward:
                MoveBack();
                break;
            case SelOption.Back:
                MoveForward();
                break;
            case SelOption.Left:
                MoveRightButton();
                break;
            case SelOption.Right:
                MoveLeftButton();
                break;
            case SelOption.Jump:
                MoveUse();
                break;
            case SelOption.Use:
                MoveJump();
                break;
            case SelOption.Mouse:
                DecreaseSilder(MsenseSlider);
                break;
            case SelOption.Return: //purposely left empty, there is no left/right move on return
            default:
                break;
        }
    }

    protected override void MoveRight() {
        switch (Current) {
            case SelOption.Forward:
                MoveBack();
                break;
            case SelOption.Back:
                MoveForward();
                break;
            case SelOption.Left:
                MoveRightButton();
                break;
            case SelOption.Right:
                MoveLeftButton();
                break;
            case SelOption.Jump:
                MoveUse();
                break;
            case SelOption.Use:
                MoveJump();
                break;
            case SelOption.Mouse:
                IncreaseSilder(MsenseSlider);
                break;
            case SelOption.Return: //purposely left empty, there is no left/right move on return
            default:
                break;
        }
    }

    //direct movement, based on mouse hover
    public override void MoveDirect(string Option) {
        if (CanMouse == false){ //check to see if a binding is ongoing
            return;
        }
        switch (Option.ToLower()) {
            case "forward":
                MoveForward();
                break;
            case "back":
                MoveBack();
                break;
            case "left":
                MoveLeftButton();
                break;
            case "right":
                MoveRightButton();
                break;
            case "jump":
                MoveJump();
                break;
            case "use":
                MoveUse();
                break;
            case "mouse":
                MoveMouse();
                break;
            case "return":
                MoveReturn();
                break;
            default:
                Debug.Log("Label not found. Check spelling?");
                break;
        }
    }

    //interaction helper
    protected override void Select() {
        switch (Current) {
            case SelOption.Forward:
                Forward.onClick?.Invoke();
                break;
            case SelOption.Back:
                Back.onClick?.Invoke();
                break;
            case SelOption.Left:
                Left.onClick?.Invoke();
                break;
            case SelOption.Right:
                Right.onClick?.Invoke();
                break;
            case SelOption.Jump:
                Jump.onClick?.Invoke();
                break;
            case SelOption.Use:
                Use.onClick?.Invoke();
                break;
            case SelOption.Mouse: //doesn't do anything because this is a slider
                break;
            case SelOption.Return:
                MenuReturn();
                break;
            default:
                break;
        }
    }


    //movement helper methods, all of them follow the same pattern and move to the associated menu option
    private void MoveForward() {
        Current = SelOption.Forward;
        rt.position = new Vector3(LeftCol.transform.position.x, Forward.transform.position.y, rt.position.z);
    }

    private void MoveBack() {
        Current = SelOption.Back;
        rt.position = new Vector3(RightCol.transform.position.x, Back.transform.position.y, rt.position.z);
    }

    private void MoveLeftButton() {
        Current = SelOption.Left;
        rt.position = new Vector3(LeftCol.transform.position.x, Left.transform.position.y, rt.position.z);
    }

    private void MoveRightButton() {
        Current = SelOption.Right;
        rt.position = new Vector3(RightCol.transform.position.x, Right.transform.position.y, rt.position.z);
    }

    private void MoveJump() {
        Current = SelOption.Jump;
        rt.position = new Vector3(LeftCol.transform.position.x, Jump.transform.position.y, rt.position.z);
    }

    private void MoveUse() {
        Current = SelOption.Use;
        rt.position = new Vector3(RightCol.transform.position.x, Use.transform.position.y, rt.position.z);
    }

    private void MoveMouse() {
        Current = SelOption.Mouse;
        rt.position = new Vector3(MidCol.transform.position.x, SlideBox.transform.position.y, rt.position.z);
    }

    private void MoveReturn() {
        Current = SelOption.Return;
        rt.position = new Vector3(MidCol.transform.position.x, Return.transform.position.y, rt.position.z);
    }


    //linkup for return button
    private void MenuReturn() {
        Return.onClick?.Invoke();
    }

    //helper function that listens for bindings to diasble mouse controls
    private void ToggleMouseControl() {
        CanMouse = !CanMouse;
    }
}
