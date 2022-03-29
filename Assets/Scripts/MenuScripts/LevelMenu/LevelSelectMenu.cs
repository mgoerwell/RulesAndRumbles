using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectMenu : MonoBehaviour
{
    //handlers for the page swap buttons
    public GameObject RightButton;
    public GameObject LeftButton;

    //place handlers for menus here.
    public GameObject PageOne;
    public GameObject PageTwo;
    public GameObject PageThree;

    private int PageNum = 0; //tracker for page we're on

    //method for loading level. Remeber to bake in some save checking to make sure it's unlocked
    public void LoadLevel(int level) {
        InputManager.Instance.MenuOff();
        SceneManager.LoadScene(level);
    }

    //Button for returning to main menu
    public void MainMenu() {
        SceneManager.LoadScene("Main_Menu");
    }

    //Control for button that scrolls to later levels. Increment current page, then switch to that page's "menu" If final page, disable button
    public void SwapPageRight() {
        PageNum++;
        switch (PageNum) {
            case 1:
                //switch page (we know we are going left -> right)
                PageOne.SetActive(false);
                PageTwo.SetActive(true);
                //second page, enable button. We only have to do it here since user can only move sequentially
                LeftButton.SetActive(true);
                break;
            case 2:
                //Switch Page (Left -> right)
                PageTwo.SetActive(false);
                PageThree.SetActive(true);
                //final page, disable button
                RightButton.SetActive(false);
                break;
        }

    }

    //Control for button that scrolls to earlier levels. Decrement current page, then switch to that page's "menu" If first page, disable button
    public void SwapPageLeft() {
        PageNum--;
        switch (PageNum) {
            case 0:
                //switch page (we know we are going right -> left)
                PageOne.SetActive(true);
                PageTwo.SetActive(false);
                //first page, disable button
                LeftButton.SetActive(false);

                break;
            case 1:
                //switch page (we know we are going right -> left)
                PageTwo.SetActive(true);
                PageThree.SetActive(false);

                //Second to last page, enable button. Moves when higher menus implemented.
                RightButton.SetActive(true);
                break;
        }
    }
}
