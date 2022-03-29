using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//helper class to enable the menu to update and reflect mouse over events
public class MenuItem : MonoBehaviour, IPointerEnterHandler {
    [SerializeField]
    private string menuOption; //reference for which option this is 
    [SerializeField]
    private GameObject Indicator;
    private KeyNavMenu menu; //handler for the menu navigation that handles this

    //get the component for reference
    private void Start() {
        menu = Indicator.GetComponent<KeyNavMenu>();
    }

    //event trigger for updating the selector
    public void OnPointerEnter(PointerEventData eventData) {
        menu.MoveDirect(menuOption);
    }

}
