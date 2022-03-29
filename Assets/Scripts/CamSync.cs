using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class exists purely for allowing movement synchronization between the camera's and the player object without subjecting it to any scale adjustments.
//it also provides handling to keep the camera properly rotated in the event of a camera switch

public class CamSync : MonoBehaviour {
    public Transform playerChar;    //Reference to the player object
    private Vector3 offset;         //offset between player and camera
    private float CamBaseRot;       //Original Camera Rotation

    //gravity event trigger
    private void OnEnable() {
        EventManager.SwapGrav += GravSync;
    }

    private void OnDisable() {
        EventManager.SwapGrav -= GravSync;
    }

    // Use this for initialization
    void Start () {
        offset = transform.position - playerChar.position;
        CamBaseRot = transform.rotation.y;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = playerChar.position + offset;
	}

    void GravSync() {
        if (transform.rotation.y == CamBaseRot) {
            transform.Rotate(0f, -180.0f, 0f);
            return;
        }
        transform.Rotate(0f, 180.0f, 0f);
    }
}
