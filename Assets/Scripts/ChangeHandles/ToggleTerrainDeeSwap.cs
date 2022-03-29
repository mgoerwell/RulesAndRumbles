using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTerrainDeeSwap : MonoBehaviour {

    //reference for the actual collider
    [SerializeField]
    private BoxCollider bc;

    [SerializeField]
    private bool TerrainOn, DeeSwapped;
    private bool TerrainSwapped = false;

    //event management
    private void OnEnable() {
        EventManager.SwapPer += SwapPer;
        EventManager.SwapGround += SwapTer;
        EventManager.Respawn += ResetTerrain;
    }

    private void OnDisable() {
        EventManager.SwapPer -= SwapPer;
        EventManager.SwapGround -= SwapTer;
        EventManager.Respawn += ResetTerrain;
    }

    //updates tracker for if we're in 2D
    void SwapPer() {
        DeeSwapped = !DeeSwapped;
        UpdateStatus();
    }

    //updates Tracker for if the right terrain is active
    void SwapTer () {
        TerrainOn = !TerrainOn;
        TerrainSwapped = !TerrainSwapped;
        UpdateStatus();
    }

    //checks conditions and enables ground if both met
    void UpdateStatus() {
        if (DeeSwapped && TerrainOn) {
            bc.enabled = true;
            return;
        }
        bc.enabled = false;
    }

    //helper function to keep terrain swaps syncronized on death
    void ResetTerrain() {
        if (TerrainSwapped) {
            SwapTer();
        }
    }

}
