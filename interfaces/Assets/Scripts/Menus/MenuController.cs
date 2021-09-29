using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject pMenu;

    private void PauseAction()
    {
        Time.timeScale = 0;
        pMenu.SetActive(true);
    }

}