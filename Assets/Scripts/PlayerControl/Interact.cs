using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour {
    private BoxCollider bc; //reference to ourself
    private readonly int MaxFrames = 20; //cooldown value between activations
    private int FrameCount = 0; //cooldown tracker

    // Use this for initialization
    void Start () {
        bc = GetComponent<BoxCollider>();
	}


    // Update is called once per frame
    void FixedUpdate() {
        if (bc.enabled) {
            if (FrameCount > MaxFrames) {
                FrameCount = 0;
                bc.enabled = false;
                EventManager.ResetCooldown();
            } else {
                FrameCount++;
            }       
        }
        Use();
    }

    private void OnTriggerEnter(Collider other) {
        string LayerName = LayerMask.LayerToName(other.gameObject.layer);
        //check number of collisions
        if (LayerName.Equals("Switchbox")) {
            other.SendMessage("UseSwitch");
        }
    }

    private void Use() {
        //activate the collider for one frame to trigger collision
        if (InputManager.Instance.Control.Gameplay.Use.triggered && bc.enabled == false) {
            bc.enabled = true;
        }
    }

}
