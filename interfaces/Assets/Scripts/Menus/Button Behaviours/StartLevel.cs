using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartLevel : MonoBehaviour
{
    private ButtonInfo buttonInfo;
    [SerializeField]
    private GameObject camera, enemies, player;
    private PlayerInput pI;

    private void Awake()
    {       
        pI = FindObjectOfType<PlayerInput>();
        buttonInfo = GetComponent<ButtonInfo>();

        buttonInfo.OnSubmit += Submit;
    }

    private void Submit()
    {
        camera.SetActive(false);
        enemies.SetActive(true);
        pI?.SwitchCurrentActionMap("Player");
        player.GetComponent<PlayerMovement>().CanRun = true;
        player.GetComponent<Animator>().SetTrigger("Running");
    }
}