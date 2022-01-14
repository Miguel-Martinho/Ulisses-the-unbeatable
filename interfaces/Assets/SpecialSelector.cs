using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SpecialSelector : MonoBehaviour
{
    [SerializeField]
    private EventTrigger current;
    private int index;
    [SerializeField]
    private EventTrigger[] buttons;

    [SerializeField]
    private InputActionAsset asset;
    private EventSystem system;

    private void Start()
    {
        index = 0;
        asset.actionMaps[1].actions[0].started += Submit;
        asset.actionMaps[1].actions[3].started += OnFire;
        asset.actionMaps[1].actions[2].started += OnWater;
        system = GetComponent<EventSystem>();
    }

    private void Submit(InputAction.CallbackContext obj)
    {       
        BaseEventData bass = new BaseEventData(system);
        ButtonInfo info = current.gameObject.GetComponent<ButtonInfo>();
        info.Submit();
    }

    private void OnWater(InputAction.CallbackContext obj)
    {
        //Left
        BaseEventData bass = new BaseEventData(system);
        ButtonInfo info = current.gameObject.GetComponent<ButtonInfo>();
        //info.Deselected();
        current.gameObject.GetComponent<Image>().color = Color.white;

        index++;

        if (index >= buttons.Length)
        {
            index = 0;
        }
        else if (index < 0)
        {
            index = buttons.Length - 1;
        }

        current = buttons[index];
        current.gameObject.GetComponent<Image>().color = Color.red;

    }

    public void OnFire(InputAction.CallbackContext context)
    {
        //Right
        BaseEventData bass = new BaseEventData(system);
        ButtonInfo info = current.gameObject.GetComponent<ButtonInfo>();
        current.gameObject.GetComponent<Image>().color = Color.white;

        index--;

        if (index >= buttons.Length)
        {
            index = 0;
        }
        else if (index < 0)
        {
            index = buttons.Length - 1;
        }

        current = buttons[index];
        current.gameObject.GetComponent<Image>().color = Color.red;
    }
}
