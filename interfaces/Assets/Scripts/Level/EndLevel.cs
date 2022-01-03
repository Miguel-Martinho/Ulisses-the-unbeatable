using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    [SerializeField]
    private GameObject levelEnd, deathScreen;
    private PlayerInput pI;
    private PlayerActor pA;

    private void Awake()
    {
        pI = FindObjectOfType<PlayerInput>();
        pA = FindObjectOfType<PlayerActor>();

        pA.Death += DeathScreen;
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.layer == 10)
        {
            EndScreen();
        }
    }

    // Event that will activate a menu screen asking if the player wants to
    // continue to the next level, save, or exit.
    private void EndScreen()
    {
        Time.timeScale = 0;
        pI?.SwitchCurrentActionMap("Interface");
        levelEnd.SetActive(true);
    }

    private void DeathScreen()
    {
        Time.timeScale = 0;
        pI?.SwitchCurrentActionMap("Interface");
        deathScreen.SetActive(true);
    }
    

    //Isto é feio
    internal void init(GameObject gameOverPanel, GameObject endLevelPanel)
    {
        levelEnd = endLevelPanel;
        deathScreen = gameOverPanel;
    }
}
