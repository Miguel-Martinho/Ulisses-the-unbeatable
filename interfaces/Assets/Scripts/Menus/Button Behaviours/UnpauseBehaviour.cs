using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnpauseBehaviour : MonoBehaviour
{
    private ButtonInfo buttonInfo;
    private PlayerInput pI;

    private void Awake()
    {
        buttonInfo = GetComponent<ButtonInfo>();
        pI = FindObjectOfType<PlayerInput>();

        buttonInfo.OnSubmit += Behaviour;
    }

    private void Behaviour()
    {
        Time.timeScale = 1;
        pI?.SwitchCurrentActionMap("Player");
    }
}
