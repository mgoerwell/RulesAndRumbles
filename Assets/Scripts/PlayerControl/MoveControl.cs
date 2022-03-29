using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveControl : MonoBehaviour {
    //private variables
    private CharacterController PC; // char controller reference
    private float VerticalVelocity; // tracker for jump velocity
    private float GroundTimer;      // for allowing jumps off edges
    private bool CanJump;           // check if we're on ground
    private Vector3 Move;           // reference handle for move vector


    //speed control variables
    private float speed = 5.0f;         //ground speed
    private float jumpStrength = 5.0f;  //jump speed
    private float grav = 9.81f;         //gravity control

    //storage for original states
    private Vector3 OrigSize;           //reference to original Size
    private Vector3 OrigPos;            //reference to original Position of the character
    private bool OrigSpeed;            //reference to original Speed
    private float OrigJump;             //reference to original Jump Strength
    private bool OrigGrav;             //reference to original Gravity

    private CameraSettings CamCon;      //reference to our camera controller



    //Public getters for processing in collision
    public Vector3 GetOrigSize() { return OrigSize; }
    public bool GetCanJump() { return CanJump; }

    //activate external functions
    private void OnEnable() {
        InputManager.Instance.GameplayOn(); //enable controls

        //event handler subscriptions
        EventManager.SwapGrav += SwapGrav;
        EventManager.SwapSize += SwapSize;
        EventManager.SwapSpeed += SwapSpeed;
        EventManager.SwapJump += SwapJump;
        EventManager.Respawn += Respawn;
    }

    //deactivate external functions
    private void OnDisable() {
        InputManager.Instance.GameplayOff(); //turn off controls
        
        //event Handler desubscriptions
        EventManager.SwapGrav -= SwapGrav;
        EventManager.SwapSize -= SwapSize;
        EventManager.SwapSpeed -= SwapSpeed;
        EventManager.SwapJump -= SwapJump;
        EventManager.Respawn -= Respawn;
    }

    // Start is called before the first frame update
    void Start() {
        PC = GetComponent<CharacterController>(); //get reference to our character
        CamCon = GetComponent<CameraSettings>(); //get reference to our camera

        //store initial states
        OrigSize = PC.transform.localScale;
        OrigPos = transform.position;
        OrigSpeed = true;
        OrigJump = jumpStrength;
        OrigGrav = true;
    }

    // Update is called at set intervals
    void Update() {
        //checking if paused to disable controls and end execution
        if (InGameMenu.GameIsPaused) { return; }

        //pre-movement condition verification
        MoveChecks();

        //if movement detected
        if (InputManager.Instance.Control.Gameplay.MoveAxis.phase == InputActionPhase.Started) {
            if (CamCon.GetIs3d()) { //check 3d sate, choose appropriate handler
                Move3D();
            } else {
                Move2D();
            }
        }
        //Jump checks
        Jump();

        //call .move only once per frame to avoid bugs
        PC.Move(Move * Time.deltaTime);

    }

    //default check helper - handles needed updates before any movement operations (on ground, camera rotations, etc)
    private void MoveChecks() {
        //check to see if we're on the ground
        CanJump = PC.isGrounded;

        //cooldown to let player jump even if they're going down ramps/just left the platform
        if (CanJump) {
            GroundTimer = 0.2f;
        }

        if (GroundTimer > 0) {
            GroundTimer -= Time.deltaTime;
        }

        //if we've hit ground, set vertical velocity to 0
        if (CanJump && VerticalVelocity < 0) {
            VerticalVelocity = 0f;
        }

        //always apply gravity
        VerticalVelocity -= grav * Time.deltaTime;

        CamCon.CamCheck();

        //reset move vector in case we aren't moving this frame
        Move = Vector3.zero;
    }

    //helper for 3d movement
    private void Move3D() {
        //gather lateral input controls

        //get input
        Vector2 moveValue = InputManager.Instance.Control.Gameplay.MoveAxis.ReadValue<Vector2>();

        //store for processing on correct axii
        Move = transform.right * moveValue.x + transform.forward * moveValue.y;
        //scale by player speed
        Move *= speed;
    }

    //helper for 2d Movement
    private void Move2D() {
        //get directional data
        float moveHorizontal = InputManager.Instance.Control.Gameplay.MoveAxis.ReadValue<Vector2>().x;

        //store for processing on correct axis
        Move = transform.forward * moveHorizontal;

        //scale by player speed, 
        Move *= speed;

        //check for if reversed grav(backwards controls)
        if (!OrigGrav) {
            Move *= -1;
        }

    }

    //jump helper
    private void Jump() {
        //allow jump while on ground
        if (InputManager.Instance.Control.Gameplay.Jump.triggered) {
            //check to see we were recently grounded
            if (GroundTimer > 0) {
                //prevent additional jumps
                GroundTimer = 0;

                // Physics dynamics formula for calculating jump up velocity based on height and gravity
                VerticalVelocity += Mathf.Sqrt(jumpStrength * 2 * grav);
            }
        }
        //inject vertical velocity before use
        Move.y = VerticalVelocity;
    }

    //Event handling Methods — Each of the following is a method that processes one of the subscribed events for the class
    //The name of each reflects the associated event.

    private void SwapSpeed() {
        if (OrigSpeed) {
            speed *= 5;
            OrigSpeed = false;
        } else {
            speed /= 5;
            OrigSpeed = true;
        }
    }

    private void SwapJump() {
        if (jumpStrength == OrigJump) {
            jumpStrength *= 2.0f;
        } else {
            jumpStrength /= 2.0f;
        }
    }

    private void SwapGrav() {
        OrigGrav = !OrigGrav;
    }

    private void SwapSize() {
        if (PC.transform.localScale == OrigSize) {
            PC.transform.localScale *= 2;
        } else {
            PC.transform.localScale = OrigSize;
        }
    }

    private void Respawn() {
        //stop movement
        Move = Vector3.zero;
        VerticalVelocity = 0f;

        //check gravity
        if (OrigGrav == false) {
            EventManager.TriggerGrav();
        }

        //check speed
        if (OrigSpeed == false) {
            SwapSpeed();
        }

        //reset possible changes
        transform.localScale = OrigSize;
        transform.position = OrigPos;
        jumpStrength = OrigJump;

    }
}
