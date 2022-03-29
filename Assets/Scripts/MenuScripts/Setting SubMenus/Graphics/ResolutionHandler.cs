using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Helper class for mnaging resolution adjustments within the graphics sub-menu
// Maintains list of supported resolutions, and provides methods to update it
public class ResolutionHandler : MonoBehaviour
{
    //references to display objects
    [SerializeField]
    private Text ResolutionText;
    [SerializeField]
    private Toggle FullScreen;

    //internal trackers
    private int Height,Width;
    private Resolution[] resolutions; //all supported resolutions
    private int SelReso;    // selected resolution
    private int TotalResos; //number of resolutions available
    private bool FS;        //whether we are fullscreen or not

    private void Start() {
        resolutions = Screen.resolutions;
        Height = Screen.height;
        Width = Screen.width;
        SelReso = FindResoIndex();
        if (SelReso == -1) {
            Debug.Log("Current Resolution not supported by monitor? Use first supported instead.");
            SelReso = 0;
        }
        TotalResos = resolutions.Length;
        TextUpdate();
        FS = Screen.fullScreen;
        FullScreen.isOn = FS;
    }


    //helper function to determine where in the list of resolutions our current resolution is
    private int FindResoIndex() {
        int index = -1;
        for (int i = 0; i < resolutions.Length; i++) {
            if (resolutions[i].height == Height && resolutions[i].width == Width) {
                index = i;
                break;
            }
        }
        return index;
    }

    //helper function for scrolling left through resolutions
    public void LeftScroll() {
        SelReso--; //decrement through array
        if (SelReso <0) {
            SelReso = TotalResos - 1; //perform wraparound
        }
        TextUpdate();
    }

    //helper function for scrolling Right through resolutions
    public void RightScroll()
    {
        SelReso++; //increment through array
        if (SelReso == TotalResos)
        {
            SelReso = 0; //perform wraparound
        }
        TextUpdate();
    }

    //method to update full screen mode
    public void UpdateFullScreen() {
        FS = !FS;
    }
    
    //helper method for updating the displayed string for the resolution
    private void TextUpdate() {
        ResolutionText.text = resolutions[SelReso].width.ToString() + " X " + resolutions[SelReso].height.ToString();
    }

    //helper method to apply resolution changes
    public void UpdateReso() {
        Resolution DesiredReso = resolutions[SelReso];
        Screen.SetResolution(DesiredReso.width, DesiredReso.height, FS);
    }
}
