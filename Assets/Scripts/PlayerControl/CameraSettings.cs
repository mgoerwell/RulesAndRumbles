using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/*
 * Class Created to handle the camera settings and control within the gamespace.
 * It is responsible for:
 *  - Initial Perspective Setup
 *  - Control of the Camera in response to user action (Primarily in the 3D mode)
 *  - Maintaining proper states and limits on Camera as Gameplay progresses (including Mouse Sensitivity)
 */
public class CameraSettings : MonoBehaviour
{
    //camera references
    public Camera Cam3D;
    public Camera Cam2D;

    //active camera mode
    [SerializeField]
    private bool is3D;

    //reference to oue use boxes for better movement
    [SerializeField]
    private GameObject UseBox3D;
    [SerializeField]
    private GameObject UseBox2D;

    //camera tracking variables
    private float CamX = 0.0f;      //Tracker for the rotation of the player around the y axis
    private float CamY = 0.0f;      //Tracker for the rotation of the Camera around the X axis
    private float MinY = -60.0f;    //Constraint for camera vertical rotation
    private float MaxY = 60.0f;     //Constraint for camera vertical rotation

    
    private float MouseSense;       //modifier for mouse sensitivity

    private bool Orig3d;            //reference for original camera perspective when respawning;
    

    //event subscribers
    private void OnEnable() {
        //enable controls
        InputManager.Instance.GameplayOn();

        //event subscribers
        EventManager.SwapPer += SwapCamera; //subscribe to perspective swapping event
        EventManager.Respawn += ResetCam;   //subscribe to respawn event
        ConfigEvents.UpdateMouseSense += GetMSense; //Subscribe to mouse sensitivity event
    }

    //event unsubscribers
    private void OnDisable() {
        //disable controls
        InputManager.Instance.GameplayOff();

        //unsubscribe events
        EventManager.SwapPer -= SwapCamera;
        EventManager.Respawn -= ResetCam;
        ConfigEvents.UpdateMouseSense -= GetMSense; //UnSubscribe from mouse sensitivity event
    }

    // Start is called before the first frame update
    void Start()
    {
        //store original 3D value
        Orig3d = is3D;

        //Identify and disable inactive camera
        if (is3D) {
            Cam2D.enabled = false;
        } else {
            Cam3D.enabled = false;
        }

        //apply mouse sensitivity settings
        GetMSense();
    }

    //getter for 3D state to reference elsewhere
    public bool GetIs3d() => is3D;

    //public facing method for handling camera
    public void CamCheck() {
        if (InputManager.Instance.Control.Gameplay.CameraAxis.triggered) {
            if (is3D){
                CameraControl();
            }
        }
    }

    //Helper function to handle mouse movement/camera in the 3d mode
    private void CameraControl() {
        Vector2 camValue = InputManager.Instance.Control.Gameplay.CameraAxis.ReadValue<Vector2>();
        // get mouse input for camera
        CamX += MouseSense * camValue.x;
        CamY -= MouseSense * camValue.y;

        //apply mouse input
        transform.eulerAngles = new Vector3(0.0f, CamX, 0.0f); //rotate player around y axis only, for movement
        Cam3D.transform.eulerAngles = new Vector3(Mathf.Clamp(CamY, MinY, MaxY), CamX, 0.0f);
    }

    //helper function to handle switching between cameras when perspective shifts
    private void SwapCamera() {
        if (is3D == true) {
            //update states
            is3D = false;
            Cam3D.enabled = false;
            Cam2D.enabled = true;
            UseBox2D.SetActive(true);
            UseBox3D.SetActive(false);

            //reset camera/player pos
            CamX = 0;
            CamY = 0;
            transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
        } else {
            is3D = true;
            Cam3D.enabled = true;
            Cam2D.enabled = false;
            UseBox2D.SetActive(false);
            UseBox3D.SetActive(true);
        }
    }

    //method for enabling respawning behaviour
    private void ResetCam() {
        if (is3D != Orig3d)
            EventManager.TriggerPerspective(); //this will perform the needed swap on all objects which change behaviour between perspectives
    }

    //helper method to get and update Mouse sensitivity, in the event it is changed through in game menu
    private void GetMSense() {
        MouseSense = PlayerPrefs.GetFloat("MSense", 0.5f);
    }
}
