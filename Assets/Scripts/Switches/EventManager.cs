using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Event manager class. Handles messaging between objects within the game space (IE collision and interaction)
 * contains an event prototype, list of events, and trigger methods for those events
 */

public class EventManager : MonoBehaviour {

    //event type definition
    public delegate void UseAction();
    //list of events
    public static event UseAction SwapPer;      //SwapPerspective, used for switching between 2d + 3d
    public static event UseAction SwapGrav;     //SwapGravity, used for flipping gravity
    public static event UseAction SwapSpeed;    //SwapSpeed, used for switching speed of player movement
    public static event UseAction SwapJump;     //SwapJump, used for switching player Jump height
    public static event UseAction SwapSize;     //SwapSize, used for switching player character size
    public static event UseAction SwapGround;   //SwapGround, used for toggling active elements of the level
    public static event UseAction Respawn;      //Used on death to reset level.

    private static int Cooldown = 0;            //Cooldown timer for how fast switches can be pressed
    private static int CoolMax = 20;            //Length of Cooldown
    
    /*
     * Event Trigger methods, all of them follow the same basic structure
     *  Public static void to allow other scripts to activate the event when appropriate
     *  Name relating to the event being called
     *  Check to see that the event has subscribers, for memory/thread safety
     *          Trigger event
     *          
     *  For efficiency, some steps have been converted to ternary invocations
     *  One of these needs to be made for each event in the above list.
     */
    public static void TriggerPerspective() {
        
        if (Cooldown > 0) {
            ReduceCooldown();
            return;
        }
        SwapPer?.Invoke();
        Cooldown = CoolMax;
    }

    public static void TriggerGrav() {
        if (Cooldown > 0)
        {
            ReduceCooldown();
            return;
        }
        SwapGrav?.Invoke();
        Cooldown = CoolMax;
    }

    public static void TriggerSpeed() {
        if (Cooldown > 0)
        {
            ReduceCooldown();
            return;
        }
        SwapSpeed?.Invoke();
        Cooldown = CoolMax;
    }

    public static void TriggerJump(){
        if (Cooldown > 0)
        {
            ReduceCooldown();
            return;
        }
        SwapJump?.Invoke();
        Cooldown = CoolMax;
    }

    public static void TriggerSize() {
        if (Cooldown > 0)
        {
            ReduceCooldown();
            return;
        }
        SwapSize?.Invoke();
        Cooldown = CoolMax;
    }

    public static void TriggerGround() {
        if (Cooldown > 0)
        {
            ReduceCooldown();
            return;
        }
        SwapGround?.Invoke();
        Cooldown = CoolMax;
    }

    public static void TriggerRespawn(){
        if (Cooldown > 0)
        {
            ReduceCooldown();
            return;
        }
        Respawn?.Invoke();
        Cooldown = CoolMax;
    }

    //Cooldown methods for management and ensuring event has time to fire.
    //helper method for cooldown timing
    private static void ReduceCooldown() {
        Cooldown--;
    } 

    //Helper method for resetting cooldown 
    public static void ResetCooldown() {
        Cooldown = 0;
    }
}
