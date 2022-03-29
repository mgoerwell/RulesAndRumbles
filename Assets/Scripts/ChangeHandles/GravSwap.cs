using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravSwap : MonoBehaviour
{
    //class to rotate the world because character controllers are dumb

    //tracker
    private GameObject world;

    //event handlers
    private void OnEnable() {
        EventManager.SwapGrav += Swap;
    }

    private void OnDisable() {
        EventManager.SwapGrav -= Swap;
    }

    // Start is called before the first frame update
    void Start() {
        world = this.gameObject;
    }
    
    private void Swap() {
        if (world.transform.rotation.z == 0) {
            world.transform.Rotate(0.0f, 0.0f, 180.0f);
        } else {
            world.transform.Rotate(0.0f, 0.0f, -180.0f);
        }

    }
}
