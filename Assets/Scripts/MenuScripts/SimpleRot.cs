using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//tiny script that just rotates the menu indicator to show that the system is still running
public class SimpleRot : MonoBehaviour{
    private RectTransform rt;

    // Start is called before the first frame update
    void Start() {
        rt = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update() {

        //rotate 2 degrees per frame
        rt.Rotate(0.0f, 0.0f, -2.0f);
    }
}
