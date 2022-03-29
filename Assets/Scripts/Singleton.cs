using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inherit from this base class to create a singleton.
/// e.g. public class MyClassName : Singleton<MyClassName> {}
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance; //internal instance tracker

    public static T Instance { //public facing interface for our instance
        get {
            if (instance == null) { //if we don't exist
                instance = FindObjectOfType<T>(); //search for an existing instance of this class
                if (instance == null) {  //if no instance exists, create ourself
                    GameObject obj = new GameObject(); //runtime invisible game object
                    obj.name = typeof(T).Name; //naming for reference (distingish between different singleton children)
                    instance = obj.AddComponent<T>(); //add self to new object
                }
            }
            return instance; //return the instance
        }
    }

    //initial check for attachment to game object
    public virtual void Awake() {
        if (instance == null) {//if no instance exists
            instance = this as T; //lazily istantiate instance
            DontDestroyOnLoad(this.gameObject); //persist through scenes
        } else {
            Destroy(gameObject); //one of us already exists, so we die
        }
    }
}

