using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenuReturn : MonoBehaviour
{   
    private GameObject previousMenu;
    [SerializeField]
    private GameObject currentMenu;

    public void SetPreviousMenu(GameObject menu)
    {
        previousMenu = menu;
    }

    public void ReturnToMenu()
    {
        currentMenu.SetActive(false);
        previousMenu.SetActive(true);
    }

}
