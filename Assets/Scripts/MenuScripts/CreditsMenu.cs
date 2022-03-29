using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Simple helper method for managing the return button in the credits page
public class CreditsMenu : MonoBehaviour
{
  public void ReturnMainMenu() {
        SceneManager.LoadScene("Main_Menu");
    }
}
