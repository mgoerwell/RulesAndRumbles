using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {
    [SerializeField]
    private AudioSource SoundFX;     //reference to our sound effect source

    private int sceneLoaded;        //reference for save data and level progression
    private MoveControl Control;    //reference for our character controller

    // Start is called before the first frame update
    void Start() {
        //check current scene and update save
        sceneLoaded = SceneManager.GetActiveScene().buildIndex;
        if (sceneLoaded > SaveData.highLevel) {
            SaveData.highLevel = sceneLoaded;
        }
        Control = GetComponent<MoveControl>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        string LayerName = LayerMask.LayerToName(hit.collider.gameObject.layer);
        switch (LayerName) {
            case "Floorbox": //player on floor
                if (!Control.GetCanJump()) { //check if we were in the air.
                    //SoundFX.PlayOneShot(SoundFX.clip);
                }
                break;
            case "Spike": // player has hit a spike, and should die if not strong
                if (transform.localScale != Control.GetOrigSize()) { break;}
                goto case "Death";
            case "Death": //player has died, reset level
                EventManager.TriggerRespawn();
                break;
            case "Goal":
                //Advance to next scene. If Final level, build settings should send us to credits.
                SceneManager.LoadScene(sceneLoaded + 1);
                break;
            default:
                break;
        }
    }
}
