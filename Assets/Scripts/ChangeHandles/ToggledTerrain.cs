using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Class that exists to help toggleable terrain know when it needs to update.
public class ToggledTerrain : MonoBehaviour {


    public BoxCollider MainBox;
    public MeshRenderer render;

    [SerializeField]
    private bool StartsEnabled;
    private bool Swapped = false;

    //event management
    private void OnEnable()
    {
        EventManager.SwapGround += Swap;
        EventManager.Respawn += ResetTerrain;
    }

    private void OnDisable()
    {
        EventManager.SwapGround -= Swap;
        EventManager.Respawn -= ResetTerrain;
    }

    //actual swap function
    private void Swap() {
        if (Swapped) {
            if (StartsEnabled) {
                MainBox.enabled = true;
                render.enabled = true;
            } else {
                MainBox.enabled = false;
                render.enabled = false;
            }
        } else {
            if (StartsEnabled) {
                MainBox.enabled = false;
                render.enabled = false;
            } else {
                MainBox.enabled = true;
                render.enabled = true;
            }
        }
        Swapped = !Swapped;
    }

    //reset if the player dies. doesn't call the event because that would trigger EVERY piece and we want them to handle it individually
    private void ResetTerrain() {
        if (Swapped) {
            Swap();
        }
    }

}
