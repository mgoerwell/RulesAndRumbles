using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
/*
 * [LEGACY]
 * Class created to control the player character within the gamespace. 
 * It needs to cover movement in both the 3d and 2d gamespaces, 
 * as well as provide control over the camera within the 3d space.
 * Will likely also contain the basic player side component of switch interaction.
 */
public class Movement : MonoBehaviour {

    //Public Variables
    //Motion variable collection: All of these are directly related to how quickly the player can move/navigate through the world
    public float speed;             //Base speed for movement
    public float JumpStrength;      //Base power for jump strength
    public float CamSpeed;          //Base speed for camera movement

    public GameObject useBox;       //Reference for our interaction collider, which is only ever active for a frame at a time
    public bool is3D;               //Conditional that checks our camera perspective for determining movement rules
    public AudioSource soundFX;     //audio reference for landing after jump
    public Camera ThreeDeeCam;      //reference to our 3D camera, spelt this way because it won't let me start with numbers in variable name for some reason
    public Camera TwoDeeCam;        //reference to our 2D Camera, As above


    //Private Variables
    //state references
    private Rigidbody rb;           //Physics body reference required for physics
    private bool canJump;           //Conditional that exists to prevent unauthorized double jumping
    
    //camera tracking variables
    private float CamX = 0.0f;      //Tracker for the rotation of the player around the y axis
    private float CamY = 0.0f;      //Tracker for the rotation of the Camera around the X axis
    private float MinY = -45.0f;    //Constraint for camera vertical rotation
    private float MaxY = 45.0f;     //Constraint for camera vertical rotation

    //internal storage for event handling
    private Vector3 spawn;          //reference to original position for death handling;
    private bool Orig3d;            //reference for original camera perspective;
    private float OrigSpeed;        //Reference for original player speed
    private float OrigJump;         //Reference for original player Jump Strength
    private Vector3 InitialSize;    //Reference for the original dimensions of the player hitbox.

    //tracker for level
    int sceneLoaded;

    //event subscriber
    private void OnEnable() {
        EventManager.SwapPer += SwapCamera;
        EventManager.SwapGrav += SwapGravity;
        EventManager.SwapSize += SwapSize;
        EventManager.SwapSpeed += SwapSpeed;
        EventManager.SwapJump += SwapJump;
        //turn game controls on
        InputManager.Instance.GameplayOn();
    }


    // Use this for initialization
    void Start () {
        //get our physics handle and set base constraints
        
        rb = GetComponent<Rigidbody>();
        canJump = false;

        //disable inactive camera
        if (is3D) {
            TwoDeeCam.enabled = false;
        } else {
            ThreeDeeCam.enabled = false;
        }

        //initialize respawn/tracker variables;
        spawn = rb.transform.position;
        Orig3d = is3D;
        OrigJump = JumpStrength;
        OrigSpeed = speed;
        InitialSize = rb.transform.localScale;

        //get current scene
        sceneLoaded = SceneManager.GetActiveScene().buildIndex;
        if (sceneLoaded > SaveData.highLevel) {
            SaveData.highLevel = sceneLoaded;
        }
    }
	
	// FixedUpdate is called periodically independent of frame rate
	void FixedUpdate () {
        MainMenuCheck(); //check to see if we quit the stage

        if (InputManager.Instance.Control.Gameplay.CameraAxis.triggered)
        {
            if (is3D) {
                CameraControl(); //check direction we are facing first
            }
        }

        if (InputManager.Instance.Control.Gameplay.MoveAxis.phase == InputActionPhase.Started) {
            if (is3D) {
                Move3d(); //3d movement controls
            } else {
                Move2d(); //2d movement controls
            }
        }

        JumpCheck(); //jump movement controls (identical between schemes)
	}

    //event unsubscription upon script end.
    private void OnDisable() {
        EventManager.SwapPer -= SwapCamera;
        EventManager.SwapGrav -= SwapGravity;
        EventManager.SwapSize -= SwapSize;
        EventManager.SwapSpeed -= SwapSpeed;
        EventManager.SwapJump -= SwapJump;
        //turn game controls off
        InputManager.Instance.GameplayOff();
    }

    //helper function to seperate out controls for 3D space
    void Move3d() {
        //get move vector
        //Vector2 moveValue = control.Gameplay.MoveAxis.ReadValue<Vector2>();
        Vector2 moveValue = InputManager.Instance.Control.Gameplay.MoveAxis.ReadValue<Vector2>();

        //get directional data
        Vector3 moveHorizontal = transform.right * moveValue.x; //Get the left/right movement vector for sideways motion
        Vector3 moveVertical = transform.forward * moveValue.y;   //Get the up/down movement vector for forwards/backwards motion

        //combine retreived directional data in vector
        Vector3 MoveVector = moveHorizontal + moveVertical;

        //apply vector to player character
        rb.AddForce(MoveVector * speed);
      
    }

    //jumping function, since it's perspective agnostic
    private void JumpCheck() {
        //Check if user tried to jump
        if (InputManager.Instance.Control.Gameplay.Jump.triggered) {
            if (canJump) {
                canJump = false; //disabe the jump button until we land
                Vector3 JumpVector = new Vector3(0.0f, JumpStrength, 0.0f); //store in vector for later modability
                rb.AddForce(JumpVector); //Jump by applying force to the bottom of the object
            }
        }
    }

    //helper function for in game menuing
    private void MainMenuCheck() {
        if (InputManager.Instance.Control.Gameplay.Quit.triggered) {
            SceneManager.LoadScene("Main_Menu");
        }
    }

    //Helper function for determining collisions and performing appropriate steps
    private void OnTriggerEnter(Collider other) {

        string LayerName = LayerMask.LayerToName(other.gameObject.layer);

        switch (LayerName) {
            case "Floorbox":
                if (!canJump) { //check to make sure we've been falling, and not merely stepped on a new piece of ground.
                    soundFX.PlayOneShot(soundFX.clip); //play sound effect since we've landed.
                    canJump = true; //re-enable the jump function since we have landed
                }
                break;
            case "Spike":
                    if (rb.transform.localScale != InitialSize) { break;}
                    goto case "Death";
            case "Death":
                //Debug.Log("You Died!");
                Respawn();
                break;
            case "Goal":
                //Debug.Log("You Win!");
                //Advance to next scene. If Final level, build settings should send us to credits.
                SceneManager.LoadScene(sceneLoaded + 1);
                break;
            default:
                break;
        }
  
    }

    //helper function to seperate out controls for 2D space
    private void Move2d() {
        //get directional data
        float moveHorizontal = InputManager.Instance.Control.Gameplay.MoveAxis.ReadValue<Vector2>().x;
        //float moveHorizontal = control.Gameplay.MoveAxis.ReadValue<Vector2>().x; //Get the left/right movement vector for sideways/stage motion.
        
        //store data in a vector for ease of use/modability. Note that unlike in 3d, since we don't want to move into the background, we only affect z-axis
        Vector3 MoveVector = new Vector3(0.0f, 0.0f, moveHorizontal);

        //add vector to player. 
        rb.AddForce(MoveVector * speed);
    }

    //Helper function to handle mouse movement/camera in the 3d mode
    private void CameraControl() {

        Vector2 camValue = InputManager.Instance.Control.Gameplay.CameraAxis.ReadValue<Vector2>();
        // get mouse input for camera
        CamX += CamSpeed * camValue.x;
        CamY -= CamSpeed * camValue.y;

        //apply mouse input
        rb.transform.eulerAngles = new Vector3(0.0f,CamX,0.0f); //rotate player around y axis only, for movement
        ThreeDeeCam.transform.eulerAngles = new Vector3(Mathf.Clamp(CamY, MinY, MaxY), CamX, 0.0f);
    }

 

    //helper function to handle switching between cameras when perspective shifts
    private void SwapCamera() {
        if (is3D == true) {
            is3D = false;
            ThreeDeeCam.enabled = false;
            TwoDeeCam.enabled = true;
            CamX = 0;
            CamY = 0;
            rb.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        } else {
            is3D = true;
            ThreeDeeCam.enabled = true;
            TwoDeeCam.enabled = false;
        }
    }

    //helper function for swapping gravity
    private void SwapGravity() {
        Vector3 gravVec = Physics.gravity;
        Physics.gravity = new Vector3(gravVec.x, -gravVec.y, gravVec.z);
        JumpStrength *= -1;
    }

    //Helper function for swapping size
    private void SwapSize() {
        if (rb.transform.localScale != InitialSize) {
            rb.transform.localScale = InitialSize;
        } else {
            rb.transform.localScale = InitialSize * 2;
        }
    }

    //Helper function for swapping speed
    private void SwapSpeed() {
        if (speed != OrigSpeed) {
            speed = OrigSpeed;
        } else {
            speed *= 2;
        }
    }

    //Helper function for swapping Jump Height
    private void SwapJump() {
        if (JumpStrength != OrigJump) {
            JumpStrength = OrigJump;
        } else {
            JumpStrength *= 1.5f;
        }
    }
    //helper for death handling
    private void Respawn() {
        //check to make sure we're in correct perspective, correct if not
        if (is3D != Orig3d) {
            EventManager.TriggerPerspective(); //this will perform the needed swap on all objects which change behaviour between perspectives
        }
        if (Physics.gravity.y > 0) {
            SwapGravity();
        }
        rb.position = spawn;
        rb.velocity = new Vector3(0.0f,0.0f,0.0f);
        JumpStrength = OrigJump;
        speed = OrigSpeed;
        rb.transform.localScale = InitialSize;
    }
}
