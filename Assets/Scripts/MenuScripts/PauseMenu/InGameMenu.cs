using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour {

    public enum SubMenu {Top, Settings, Audio, Graphics, Controls };
    private SubMenu Current = SubMenu.Top;
    public static bool GameIsPaused = false;
    [SerializeField]
    private GameObject PauseMenuUI, TopUI, SettingsMenuUI, AudioMenuUI, GraphicsMenuUI, ControlMenuUI;
    

    //enabling of pause button
    private void OnEnable() {
        InputManager.Instance.Control.Gameplay.Quit.Enable();
    }

    //disabling of pause button
    private void OnDisable() {
        InputManager.Instance.Control.Gameplay.Quit.Enable();
    }

    //observer to see if game has been paused
    private void Update() {
        if (InputManager.Instance.Control.Gameplay.Quit.triggered) {
            if (GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    //method for returning to active gameplay
    public void Resume() {
        CloseSubmenu();
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
        InputManager.Instance.MenuOff();
    }

    //method to return to main game menu
    public void ToMainMenu() {
        Resume();
        SceneManager.LoadScene("Main_Menu");
    }

    //work around public method to handle the fact that methods are only visible in the unity editor if parameter has type Float,Int,bool,string or object. Don't know who decided enums don't count.
    public void OpenSubmenuEditor(string MenuName) {
        switch (MenuName) {
            case "Top":
                OpenSubmenu(SubMenu.Top);
                break;
            case "Settings":
                OpenSubmenu(SubMenu.Settings);
                break;
            case "Audio":
                OpenSubmenu(SubMenu.Audio);
                break;
            case "Controls":
                OpenSubmenu(SubMenu.Controls);
                break;
            case "Graphics":
                OpenSubmenu(SubMenu.Graphics);
                break;
            default:
                Debug.Log("No menu with name: " + MenuName + ". Check Spelling? Menus are capitalized.");
                return;
        }

    }

    //method to open specific submenus, made public to be accessed by submenu handlers. 
    public void OpenSubmenu(SubMenu menu) {
        CloseSubmenu();
        switch (menu) {
            case SubMenu.Top:
                TopUI.SetActive(true);
                break;
            case SubMenu.Settings:
                SettingsMenuUI.SetActive(true);
                break;
            case SubMenu.Audio:
                AudioMenuUI.SetActive(true);
                break;
            case SubMenu.Controls:
                InputManager.Instance.GameplayOff(); //rebind operations cannot be performed while they are active.
                ControlMenuUI.SetActive(true);
                break;
            case SubMenu.Graphics:
                GraphicsMenuUI.SetActive(true);
                break;
        }
        Current = menu;
    }

    //method that quits the game
    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    Application.Quit();
        #endif
    }

    //method to close specific submenus, no need to be made public as all submenus will need to open their replacement instead
    private void CloseSubmenu() {
        switch (Current) {
            case SubMenu.Top:
                TopUI.SetActive(false);
                break;
            case SubMenu.Settings:
                SettingsMenuUI.SetActive(false);
                break;
            case SubMenu.Audio:
                AudioMenuUI.SetActive(false);
                break;
            case SubMenu.Controls:
                ControlMenuUI.SetActive(false);
                InputManager.Instance.GameplayOn(); //restart controls now that we aren't modifying them
                break;
            case SubMenu.Graphics:
                GraphicsMenuUI.SetActive(false);
                break;
        }
    }

    //handler for pausing the game
    private void Pause() {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
        OpenSubmenu(SubMenu.Top);
    }
}
