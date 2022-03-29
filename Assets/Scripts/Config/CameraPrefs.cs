using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPrefs : MonoBehaviour {
    private Camera cam;
    private float FoV;

    //event subscription management
    //currently manages subscription to Field of View Setting changes
    private void OnEnable() {
        ConfigEvents.UpdateFoV += getFoV;
    }

    private void OnDisable() {
        ConfigEvents.UpdateFoV -= getFoV;
    }

    // Use this for initialization
    void Start () {
        cam = GetComponent<Camera>();
        getFoV();
	}

    //FoV getter for updates
    private void getFoV() {
        FoV = PlayerPrefs.GetFloat("FoV", 90.0f);
        cam.fieldOfView = FoV;
    }

}
