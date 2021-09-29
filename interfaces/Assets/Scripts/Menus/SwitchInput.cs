using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchInput : MonoBehaviour
{
    private PlayerInputHandler  pI;
    private MenuInputController mI;

    [SerializeField]
    private GameObject pauseMenu;

    private void Awake()
    {
        pI = FindObjectOfType<PlayerInputHandler>();
        mI = FindObjectOfType<MenuInputController>();

        pI.Pause += PauseMenuActivate;
    }

    private void PauseMenuActivate()
    {
        Time.timeScale = 0;

        pauseMenu.SetActive(true);
    }
}
