using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput pI;
    [SerializeField]
    private Animator anim;
    private PlayerMovement pM;

    private void Start()
    {
    }
    private void Awake()
    {
        pI = FindObjectOfType<PlayerInput>().GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        pM = GetComponent<PlayerMovement>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Jump?.Invoke();
            anim.SetBool("Jump", true);
        }
    }

    public void OnPunch(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            Punch?.Invoke();
            anim.SetTrigger("Punch");
        }
    }

    public void OnPause(InputAction.CallbackContext ctx)
    {
        if (!ctx.started) return;

        pI.SwitchCurrentActionMap("Interface");
        Pause?.Invoke();
    }

    public event Action Jump, Punch, Pause;
}
