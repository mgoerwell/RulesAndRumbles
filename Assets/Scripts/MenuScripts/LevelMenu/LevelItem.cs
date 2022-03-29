using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelItem : MonoBehaviour
{
    public int level;
    public Text ButtonText;
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        if (level > SaveData.highLevel) {
            button.interactable = false;
            ButtonText.text = "Locked";
        }
    }

}
