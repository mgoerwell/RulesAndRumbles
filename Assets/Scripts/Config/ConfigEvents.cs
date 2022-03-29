using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Event manager class. Handles Events related to updating configuration settings
 * contains an event prototype, list of events, and trigger methods for those events
 */
public class ConfigEvents : MonoBehaviour {
    //event prototype
    public delegate void ConfigAction();
    //event list
    public static event ConfigAction UpdateFoV;
    public static event ConfigAction UpdateMusic;
    public static event ConfigAction UpdateSounds;
    public static event ConfigAction UpdateMouseSense;
    public static event ConfigAction ToggleControls;
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
    public static void TriggerFoV() {
        UpdateFoV?.Invoke();
    }

    public static void TriggerMusic() {
        UpdateMusic?.Invoke();
    }

    public static void TriggerSounds() {
        UpdateSounds?.Invoke();
    }

    public static void TriggerMouseSense() {
        UpdateMouseSense?.Invoke();
    }
    public static void TriggerControls() {
        ToggleControls?.Invoke();
    }
}
