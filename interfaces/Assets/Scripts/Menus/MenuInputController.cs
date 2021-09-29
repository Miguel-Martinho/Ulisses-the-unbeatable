using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInputController : MonoBehaviour
{
    public void OnPause(InputAction.CallbackContext ctx)
    {
        if (ctx.started) Pause?.Invoke();
    }

    public void OnMoveLeft(InputAction.CallbackContext ctx)
    {
        if (ctx.started) MLeft?.Invoke();
    }

    public void OnMoveRight(InputAction.CallbackContext ctx)
    {
        if (ctx.started) MRight?.Invoke();
    }

    public void SwitchInputManual()
    {

    }
    public event Action Pause, MLeft, MRight;
}
