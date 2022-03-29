using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeeSwap : MonoBehaviour {

    
    public BoxCollider bc;


    //event management
    private void OnEnable() {
        EventManager.SwapPer += Swap;
    }

    private void OnDisable() {
        EventManager.SwapPer -= Swap;
    }

    void Swap() {
        bc.enabled = !bc.enabled;
    }

}
