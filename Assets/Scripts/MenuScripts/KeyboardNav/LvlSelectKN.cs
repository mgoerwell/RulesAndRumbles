using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//class handling the keyboard navigation for the level select menu
//this is the single most complicated Key nav menu
public class LvlSelectKN : KeyNavMenu {
    //references to the menu buttons
    [SerializeField]
    private Button MainMenu, PrevPage,NextPage, Lvl1, Lvl2, Lvl3, Lvl4, Lvl5, Lvl6, Lvl7, Lvl8, Lvl9, Lvl10, Lvl11, Lvl12, Lvl13, Lvl14, Lvl15;

    //reference for navigation helpers
    [SerializeField]
    private GameObject TopRow, MidRow, LowRow, MainRow;

    //tracker enums
    private enum SelButton { Back, Prev, Next, Lvl1, Lvl2, Lvl3, Lvl4, Lvl5, Lvl6, Lvl7, Lvl8, Lvl9, Lvl10, Lvl11, Lvl12, Lvl13, Lvl14, Lvl15 }
    private SelButton CurButton = SelButton.Lvl1;

    private enum Page { One, Two, Three};
    private Page CurPage = Page.One;

    //reference to our physical parameters
    private RectTransform rt;

    private void Start() {
        rt = GetComponent<RectTransform>();
    }


    // Update is called once per frame
    void Update() {
        //do not process if still in cooldown
        if (!CooldownCheck())
            return;

        //check to see if we're backing out
        if (InputManager.Instance.Control.Menu.Cancel.triggered) {
            GoMain();
        }

        MoveVert();
        MoveHor();

        //only call if we actually choose an option
        if (InputManager.Instance.Control.Menu.Confirm.triggered) {
            Select();
        }
    }

    //helper overrides for compartmentalization
    protected override void MoveUp() {
        switch (CurPage) {
            case Page.One:
                PageOneUp();
                break;
            case Page.Two:
                PageTwoUp();
                break;
            case Page.Three:
                PageThreeHandle();
                break;
            default:
                break;
        }
    }

    protected override void MoveDown() {
        switch (CurPage) {
            case Page.One:
                PageOneDown();
                break;
            case Page.Two:
                PageTwoDown();
                break;
            case Page.Three:
                PageThreeHandle();
                break;
            default:
                break;
        }
    }
    //helper function required because of visible unity types
    public override void MoveDirect(string Option) {
        switch (Option.ToLower()) {
            case "lvl1":
                if (LockCheck(Lvl1)) {
                    MoveButton(Lvl1, TopRow, SelButton.Lvl1);
                }
                break;
            case "lvl2":
                if (LockCheck(Lvl2)) {
                    MoveButton(Lvl2, TopRow, SelButton.Lvl2);
                }
                break;
            case "lvl3":
                if (LockCheck(Lvl3)) {
                    MoveButton(Lvl3, TopRow, SelButton.Lvl3);
                }
                break;
            case "lvl4":
                if (LockCheck(Lvl4)) {
                    MoveButton(Lvl4, LowRow, SelButton.Lvl4);
                }
                break;
            case "lvl5":
                if (LockCheck(Lvl5)) {
                    MoveButton(Lvl5, LowRow, SelButton.Lvl5);
                }
                break;
            case "lvl6":
                if (LockCheck(Lvl6)) {
                    MoveButton(Lvl6, LowRow, SelButton.Lvl6);
                }
                break;
            case "lvl7":
                if (LockCheck(Lvl7)) {
                    MoveButton(Lvl7, TopRow, SelButton.Lvl7);
                }
                break;
            case "lvl8":
                if (LockCheck(Lvl8)) {
                    MoveButton(Lvl8, TopRow, SelButton.Lvl8);
                }
                break;
            case "lvl9":
                if (LockCheck(Lvl9)) {
                    MoveButton(Lvl9, TopRow, SelButton.Lvl9);
                }
                break;
            case "lvl10":
                if (LockCheck(Lvl10)) {
                    MoveButton(Lvl10, LowRow, SelButton.Lvl10);
                }
                break;
            case "lvl11":
                if (LockCheck(Lvl11)) {
                    MoveButton(Lvl11, LowRow, SelButton.Lvl11);
                }
                break;
            case "lvl12":
                if (LockCheck(Lvl12)) {
                    MoveButton(Lvl12, LowRow, SelButton.Lvl12);
                }
                break;
            case "lvl13":
                if (LockCheck(Lvl13)) {
                    MoveButton(Lvl13, MidRow, SelButton.Lvl13);
                }
                break;
            case "lvl14":
                if (LockCheck(Lvl14)) {
                    MoveButton(Lvl14, MidRow, SelButton.Lvl14);
                }
                break;
            case "lvl15":
                if (LockCheck(Lvl15)) {
                    MoveButton(Lvl15, MidRow, SelButton.Lvl15);
                }
                break;
            case "main":
                MoveButton(MainMenu, MainRow, SelButton.Back);
                break;
            case "next":
                MoveButton(NextPage, MainRow, SelButton.Next);
                break;
            case "prev":
                MoveButton(PrevPage, MainRow, SelButton.Prev);
                break;
            default:
                break;
        }
    }

    //helper function for the select key
    protected override void Select() {
        switch (CurButton) {
            case SelButton.Lvl1:
                Lvl1.onClick?.Invoke();
                break;
            case SelButton.Lvl2:
                Lvl2.onClick?.Invoke();
                break;
            case SelButton.Lvl3:
                Lvl3.onClick?.Invoke();
                break;
            case SelButton.Lvl4:
                Lvl4.onClick?.Invoke();
                break;
            case SelButton.Lvl5:
                Lvl5.onClick?.Invoke();
                break;
            case SelButton.Lvl6:
                Lvl6.onClick?.Invoke();
                break;
            case SelButton.Lvl7:
                Lvl7.onClick?.Invoke();
                break;
            case SelButton.Lvl8:
                Lvl8.onClick?.Invoke();
                break;
            case SelButton.Lvl9:
                Lvl9.onClick?.Invoke();
                break;
            case SelButton.Lvl10:
                Lvl10.onClick?.Invoke();
                break;
            case SelButton.Lvl11:
                Lvl11.onClick?.Invoke();
                break;
            case SelButton.Lvl12:
                Lvl12.onClick?.Invoke();
                break;
            case SelButton.Lvl13:
                Lvl13.onClick?.Invoke();
                break;
            case SelButton.Lvl14:
                Lvl14.onClick?.Invoke();
                break;
            case SelButton.Lvl15:
                Lvl15.onClick?.Invoke();
                break;
            case SelButton.Back:
                GoMain();
                break;
            case SelButton.Prev:
                PrevPage.onClick?.Invoke();
                break;
            case SelButton.Next:
                NextPage.onClick?.Invoke();
                break;
            default:
                break;
        }
    }

    //helper functions attached to the navigation buttons between pages to keep internals consistent, attach these on to the on clicks
    //moving back through pages
    public void Previous() {
        switch (CurPage) {
            case Page.Two:
                CurPage = Page.One;
                MoveButton(MainMenu, MainRow, SelButton.Back); //button does not exist on page one, move to the nearest
                break;
            case Page.Three:
                CurPage = Page.Two;
                break;
            default: //should not be able to hit button on page one but just in case
                break;
        }
    }

    //moving forward through pages
    public void Next() {
        switch (CurPage) {
            case Page.One:
                CurPage = Page.Two;
                break;
            case Page.Two:
                CurPage = Page.Three;
                MoveButton(MainMenu, MainRow, SelButton.Back); //button does not exist on page three, move to the nearest
                break;
            default: //should not be able to hit button on page three but just in case
                break;
        }
    }

    //helper function for returning to main menu
    private void GoMain() {
        MainMenu.onClick?.Invoke();
    }
    //helper function that serves as a gerneralizer for moving to a given button
    private void MoveButton(Button button, GameObject row, SelButton NewCurBut) {
        CurButton = NewCurBut;
        rt.position = new Vector3(button.transform.position.x, row.transform.position.y, rt.position.z);
    }

    //helperFunction to check if the level is unlocked for aiding navigation
    private bool LockCheck(Button button) {
        return button.interactable;
    }

    //helper functions for internal vertical movement since these vary depending on the page
    /*
     * Pattern explanation
     * top/mid row -> corresponding main row, or to the main menu button if it doesn't exist
     * low row (page 1+2) -> go to top row, which will be unlocked because of how the lock mechanism works
     * main row check the low row, if it's unlocked go there, otherwise check the top row and if unlocked go there, if neither, stay put
     */
    private void PageOneUp() {
        switch (CurButton) {
            //top row block
            case SelButton.Lvl1:
                MoveButton(MainMenu, MainRow, SelButton.Back);
                break;
            case SelButton.Lvl2:
                MoveButton(MainMenu, MainRow, SelButton.Back);
                break;
            case SelButton.Lvl3:
                MoveButton(NextPage, MainRow, SelButton.Next);
                break;
            //low row block
            case SelButton.Lvl4:
                MoveButton(Lvl1, TopRow, SelButton.Lvl1);
                break;
            case SelButton.Lvl5:
                MoveButton(Lvl2, TopRow, SelButton.Lvl2);
                break;
            case SelButton.Lvl6:
                MoveButton(Lvl3, TopRow, SelButton.Lvl3);
                break;
            //main bar block
            case SelButton.Back:
                if (LockCheck(Lvl5)) {
                    MoveButton(Lvl5, LowRow, SelButton.Lvl5);
                } else if (LockCheck(Lvl2)) {
                    MoveButton(Lvl2, TopRow, SelButton.Lvl2);
                } else {
                    MoveButton(Lvl1, TopRow, SelButton.Lvl1); //unique case, to let us actually start the game since lvl 1 is always unlocked
                }
                break;
            case SelButton.Next:
                if (LockCheck(Lvl6)) {
                    MoveButton(Lvl6, LowRow, SelButton.Lvl6);
                } else if (LockCheck(Lvl3))
                {
                    MoveButton(Lvl3, TopRow, SelButton.Lvl3);
                } else {
                    MoveButton(Lvl1, TopRow, SelButton.Lvl1); //unique case, to let us actually start the game since lvl 1 is always unlocked
                }
                break;
            default:
                break;
        }
    }



    private void PageTwoUp() {
        switch (CurButton) {
            //top row block
            case SelButton.Lvl7:
                MoveButton(PrevPage, MainRow, SelButton.Prev);
                break;
            case SelButton.Lvl8:
                MoveButton(MainMenu, MainRow, SelButton.Back);
                break;
            case SelButton.Lvl9:
                MoveButton(NextPage, MainRow, SelButton.Next);
                break;
            //low row block
            case SelButton.Lvl10:
                MoveButton(Lvl7, TopRow, SelButton.Lvl7);
                break;
            case SelButton.Lvl1:
                MoveButton(Lvl8, TopRow, SelButton.Lvl8);
                break;
            case SelButton.Lvl12:
                MoveButton(Lvl9, TopRow, SelButton.Lvl9);
                break;
            //main bar block
            case SelButton.Prev:
                if (LockCheck(Lvl10)) {
                    MoveButton(Lvl10, LowRow, SelButton.Lvl10);
                } else if (LockCheck(Lvl7)) {
                    MoveButton(Lvl7, TopRow, SelButton.Lvl7);
                }
                break;
            case SelButton.Back:
                if (LockCheck(Lvl11)) {
                    MoveButton(Lvl11, LowRow, SelButton.Lvl11);
                } else if (LockCheck(Lvl8)) {
                    MoveButton(Lvl8, TopRow, SelButton.Lvl8);
                }
                break;
            case SelButton.Next:
                if (LockCheck(Lvl12)) {
                    MoveButton(Lvl12, LowRow, SelButton.Lvl12);
                } else if (LockCheck(Lvl9)) {
                    MoveButton(Lvl9, TopRow, SelButton.Lvl9);
                }
                break;
            default:
                break;
        }
    }

    /*
     * Pattern explanation
     * top/mid row -> check if the lower row is unlocked (if it exists), if it is, move there, otherwise follow low row rules
     * low row (page 1+2) -> go to corresponding main menu bar button. if button is absent on this page, go to the main menu button instead
     * main row -> check if the top row is unlocked, if it is, move there, otherwise do nothing as the the low row will be locked also
     */
    private void PageOneDown() {
        switch (CurButton) {
            //top row block
            case SelButton.Lvl1:
                if (LockCheck(Lvl4)) {
                    MoveButton(Lvl4, LowRow, SelButton.Lvl4);
                } else {
                    MoveButton(MainMenu, MainRow, SelButton.Back);
                }
                break;
            case SelButton.Lvl2:
                if (LockCheck(Lvl5)) {
                    MoveButton(Lvl5, LowRow, SelButton.Lvl5);
                } else {
                    MoveButton(MainMenu, MainRow, SelButton.Back);
                }
                break;
            case SelButton.Lvl3:
                if (LockCheck(Lvl6)) {
                    MoveButton(Lvl6, LowRow, SelButton.Lvl6);
                } else {
                    MoveButton(NextPage, MainRow, SelButton.Next);
                }
                break;
            //low row block
            case SelButton.Lvl4:
                MoveButton(MainMenu, MainRow, SelButton.Back);
                break;
            case SelButton.Lvl5:
                MoveButton(MainMenu, MainRow, SelButton.Back);
                break;
            case SelButton.Lvl6:
                MoveButton(NextPage, MainRow, SelButton.Next);
                break;
            case SelButton.Back:
                if (LockCheck(Lvl2)) {
                    MoveButton(Lvl2, TopRow, SelButton.Lvl2);
                }
                break;
            case SelButton.Next:
                if (LockCheck(Lvl3)) {
                    MoveButton(Lvl3, TopRow, SelButton.Lvl3);
                }
                break;
            default:
                break;
        }
    }

    private void PageTwoDown() {
        switch (CurButton) {
            //top row block
            case SelButton.Lvl7:
                if (LockCheck(Lvl10)) {
                    MoveButton(Lvl10, LowRow, SelButton.Lvl10);
                } else {
                    MoveButton(PrevPage, MainRow, SelButton.Prev);
                }
                break;
            case SelButton.Lvl8:
                if (LockCheck(Lvl11)) {
                    MoveButton(Lvl11, LowRow, SelButton.Lvl11);
                } else {
                    MoveButton(MainMenu, MainRow, SelButton.Back);
                }
                break;
            case SelButton.Lvl9:
                if (LockCheck(Lvl12)){
                    MoveButton(Lvl12, LowRow, SelButton.Lvl12);
                } else {
                    MoveButton(NextPage, MainRow, SelButton.Next);
                }
                break;
            //low row block
            case SelButton.Lvl10:
                MoveButton(PrevPage, MainRow, SelButton.Prev);
                break;
            case SelButton.Lvl11:
                MoveButton(MainMenu, MainRow, SelButton.Back);
                break;
            case SelButton.Lvl12:
                MoveButton(NextPage, MainRow, SelButton.Next);
                break;
            //main row block
            case SelButton.Prev:
                if (LockCheck(Lvl7)) {
                    MoveButton(Lvl7, TopRow, SelButton.Lvl7);
                }
                break;
            case SelButton.Back:
                if (LockCheck(Lvl8)) {
                    MoveButton(Lvl8, TopRow, SelButton.Lvl8);
                }
                break;
            case SelButton.Next:
                if (LockCheck(Lvl9)) {
                    MoveButton(Lvl9, TopRow, SelButton.Lvl9);
                }
                break;
            default:
                break;
        }
    }

    //because there are only two rows on page three, up and down are indistinguishable functionally
    private void PageThreeHandle() {
        switch (CurButton) {
            //top row block
            case SelButton.Lvl13:
                MoveButton(PrevPage, MainRow, SelButton.Prev);
                break;
            case SelButton.Lvl14:
                MoveButton(MainMenu, MainRow, SelButton.Back);
                break;
            case SelButton.Lvl15:
                MoveButton(MainMenu, MainRow, SelButton.Back);
                break;
            //main bar block
            case SelButton.Prev:
                if (LockCheck(Lvl13)){
                    MoveButton(Lvl13, MidRow, SelButton.Lvl13);
                }
                break;
            case SelButton.Back:
                if (LockCheck(Lvl14)) {
                    MoveButton(Lvl14, MidRow, SelButton.Lvl14);
                }
                break;
            default:
                break;
        }
    }


    //helper functions for internal horizontal navigation

    //going right
    /*
     * Explanation of pattern
     *  Because we can only move onto a button if it is unlocked we can exploit our knowledge of how the locking mechanism works to minimize checks.
     * leftmost in row, check the one immediately right, if it's unlocked move there, otherwise do nothing, as the rightost will also not be unlocked
     * middle in row, check if the one to the right is unlocked. If it is, move there, otherwise move to the one on the left which is guaranteed to be unlocked.
     * rightmost in row, move to the leftmost in row, which is guaranteed to be unlocked if we're here
     * 
     * blocks are divided based on which screen row they're in
     */
    protected override void MoveRight() {
        switch (CurButton) {
            //block for top bar
            case SelButton.Lvl1:
                if (LockCheck(Lvl2)) {
                    MoveButton(Lvl2, TopRow, SelButton.Lvl2);
                }
                break;
            case SelButton.Lvl2:
                if (LockCheck(Lvl3)) {
                    MoveButton(Lvl3, TopRow, SelButton.Lvl3); 
                } else {
                    MoveButton(Lvl1, TopRow, SelButton.Lvl1);
                }
                break;
            case SelButton.Lvl3:
                MoveButton(Lvl1, TopRow, SelButton.Lvl1);
                break;
            case SelButton.Lvl7:
                if (LockCheck(Lvl8)) {
                    MoveButton(Lvl8, TopRow, SelButton.Lvl8);
                }
                break;
            case SelButton.Lvl8:
                if (LockCheck(Lvl9)) {
                    MoveButton(Lvl9, TopRow, SelButton.Lvl9);
                } else {
                    MoveButton(Lvl7, TopRow, SelButton.Lvl7);
                }
                break;
            case SelButton.Lvl9:
                MoveButton(Lvl7, TopRow, SelButton.Lvl7);
                break;
            //block for low bar
            case SelButton.Lvl4:
                if (LockCheck(Lvl5)) {
                    MoveButton(Lvl5, LowRow, SelButton.Lvl5);
                }
                break;
            case SelButton.Lvl5:
                if (LockCheck(Lvl6)) {
                    MoveButton(Lvl6, LowRow, SelButton.Lvl6);
                } else {
                    MoveButton(Lvl4, LowRow, SelButton.Lvl4);
                }
                break;
            case SelButton.Lvl6:
                MoveButton(Lvl4, LowRow, SelButton.Lvl4);
                break;
            case SelButton.Lvl10:
                if (LockCheck(Lvl11)) {
                    MoveButton(Lvl11, LowRow, SelButton.Lvl11);
                }
                break;
            case SelButton.Lvl11:
                if (LockCheck(Lvl12)) {
                    MoveButton(Lvl12, LowRow, SelButton.Lvl12);
                } else {
                    MoveButton(Lvl10, LowRow, SelButton.Lvl10);
                }
                break;
            case SelButton.Lvl12:
                MoveButton(Lvl10, LowRow, SelButton.Lvl10);
                break;
            //block for mid bar
            case SelButton.Lvl13:
                if (LockCheck(Lvl14)) {
                    MoveButton(Lvl14, MidRow, SelButton.Lvl14);
                }
                break;
            case SelButton.Lvl14:
                if (LockCheck(Lvl15)) {
                    MoveButton(Lvl15, MidRow, SelButton.Lvl15);
                } else {
                    MoveButton(Lvl13, MidRow, SelButton.Lvl13);
                }
                break;
            case SelButton.Lvl15:
                MoveButton(Lvl13, MidRow, SelButton.Lvl13);
                break;
            //block for main bar
            case SelButton.Back:
                if (CurPage == Page.Three) {
                    MoveButton(PrevPage, MainRow, SelButton.Prev);
                } else {
                    MoveButton(NextPage, MainRow, SelButton.Next);
                }
                break;
            case SelButton.Next:
                if (CurPage == Page.One) {
                    MoveButton(MainMenu, MainRow, SelButton.Back);
                } else {
                    MoveButton(PrevPage, MainRow, SelButton.Prev);
                }
                break;
            case SelButton.Prev:
                MoveButton(MainMenu, MainRow, SelButton.Back);
                break;
            default:
                break;
        }
    }

    //going left
    /*
     * Explanation of pattern
     *  Because we can only move onto a button if it is unlocked we can exploit our knowledge of how the locking mechanism works to minimize checks.
     * leftmost in row, check the one furthest right, if it's unlocked move there, otherwise check the one immediately right and if it's unlocked move there, otherwise nothing
     * Middle, move left, as it will always be unlocked
     * rightmost, move left as it will always be unlocked
     * 
     * blocks are divided based on which screen row they're in
     */
    protected override void MoveLeft() {
        switch (CurButton)
        {
            //block for top bar
            case SelButton.Lvl1:
                if (LockCheck(Lvl3)) {
                    MoveButton(Lvl3, TopRow, SelButton.Lvl3);
                } else if (LockCheck(Lvl2)) {
                    MoveButton(Lvl2, TopRow, SelButton.Lvl2);
                }
                break;
            case SelButton.Lvl2:
                MoveButton(Lvl1, TopRow, SelButton.Lvl1);
                break;
            case SelButton.Lvl3:
                MoveButton(Lvl2, TopRow, SelButton.Lvl2);
                break;
            case SelButton.Lvl7:
                if (LockCheck(Lvl9)) {
                    MoveButton(Lvl9, TopRow, SelButton.Lvl9);
                } else if (LockCheck(Lvl8)) {
                    MoveButton(Lvl8, TopRow, SelButton.Lvl8);
                }
                break;
            case SelButton.Lvl8:
                MoveButton(Lvl7, TopRow, SelButton.Lvl7);
                break;
            case SelButton.Lvl9:
                MoveButton(Lvl8, TopRow, SelButton.Lvl8);
                break;
            //block for low bar
            case SelButton.Lvl4:
                if (LockCheck(Lvl6)) {
                    MoveButton(Lvl6, LowRow, SelButton.Lvl6);
                } else if (LockCheck(Lvl8)) {
                    MoveButton(Lvl5, LowRow, SelButton.Lvl5);
                }
                break;
            case SelButton.Lvl5:
                MoveButton(Lvl4, LowRow, SelButton.Lvl4);
                break;
            case SelButton.Lvl6:
                MoveButton(Lvl5, LowRow, SelButton.Lvl5);
                break;
            case SelButton.Lvl10:
                if (LockCheck(Lvl12)) {
                    MoveButton(Lvl12, LowRow, SelButton.Lvl12);
                } else if (LockCheck(Lvl11)) {
                    MoveButton(Lvl11, LowRow, SelButton.Lvl11);
                }
                break;
            case SelButton.Lvl11:
                MoveButton(Lvl10, LowRow, SelButton.Lvl10);
                break;
            case SelButton.Lvl12:
                MoveButton(Lvl11, LowRow, SelButton.Lvl11);

                break;
            //block for mid bar
            case SelButton.Lvl13:
                if (LockCheck(Lvl15)) {
                    MoveButton(Lvl15, MidRow, SelButton.Lvl15);
                } else if (LockCheck(Lvl14)) {
                    MoveButton(Lvl14, MidRow, SelButton.Lvl14);
                }
                break;
            case SelButton.Lvl14:
                MoveButton(Lvl13, MidRow, SelButton.Lvl13);
                break;
            case SelButton.Lvl15:
                MoveButton(Lvl14, MidRow, SelButton.Lvl14);
                break;
            //block for main bar
            case SelButton.Back:
                if (CurPage == Page.One) {
                    MoveButton(NextPage, MainRow, SelButton.Next);
                } else {
                    MoveButton(PrevPage, MainRow, SelButton.Prev);
                }
                break;
            case SelButton.Next:
                MoveButton(MainMenu, MainRow, SelButton.Back);
                break;
            case SelButton.Prev:
                if (CurPage == Page.Three) {
                    MoveButton(MainMenu, MainRow, SelButton.Back);
                } else {
                    MoveButton(NextPage, MainRow, SelButton.Next);
                }
                break;
            default:
                break;
        }
    }
}
