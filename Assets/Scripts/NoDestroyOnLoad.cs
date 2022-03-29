using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDestroyOnLoad : MonoBehaviour
{
    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
}
