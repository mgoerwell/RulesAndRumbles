using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//record of switch varieties for reference
public enum Switchtype { Generic, Cam, Grav, Terrain, Speed, Jump, Size } 

public class Switch : MonoBehaviour {

    public Switchtype type;
    public GameObject button;
    public AudioSource sfx;

    //generic foundation for switch use
    void UseSwitch() {
        switch (type) {
            case Switchtype.Generic:
                Debug.Log("Generic Switch Triggered");
                break;
            case Switchtype.Cam:
                Debug.Log("CamSwitch Triggered");
                CamSwitch.UseCamSwitch();
                break;
            case Switchtype.Grav:
                Debug.Log("GravSwitch Triggered");
                GravSwitch.UseGravSwitch();
                break;
            case Switchtype.Speed:
                Debug.Log("SpeedSwitch Triggered");
                SpeedSwitch.UseSpeedSwitch();
                break;
            case Switchtype.Jump:
                Debug.Log("JumpSwitch Triggered");
                JumpSwitch.UseJumpSwitch();
                break;
            case Switchtype.Size:
                Debug.Log("SizeSwitch Triggered");
                SizeSwitch.UseSizeSwitch();
                break;
            case Switchtype.Terrain:
                Debug.Log("TerrainSwitch Triggered");
                TerrainSwitch.UseTerrainSwitch();
                break;
            default:
                break;
        }
        sfx.PlayOneShot(sfx.clip);
    }
}
