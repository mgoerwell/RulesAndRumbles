using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//event handler for the main menu buttons. Functions should be self explanatory between their brevity and names.
public class MenuEventManager : MonoBehaviour {
    
    public void GoToLevelSelect() {
        SceneManager.LoadScene("Level_Select_Menu");
    }

    public void GoToSettings() {
        SceneManager.LoadScene("Setting_Menu");
    }

    public void GoToCredits() {
        SceneManager.LoadScene("Credits");
    }

    //This function has additional handling to allow it to function while in the editor.
    public void QuitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

}
