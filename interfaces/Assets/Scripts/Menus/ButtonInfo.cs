using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    private Button button;
    private MenuInputController iC;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        button = GetComponent<Button>();
        iC = FindObjectOfType<MenuInputController>();      
    }

    public void Selected()
    {
        anim.SetBool("selected", true);
    }

    public void Deselected()
    {
        anim.SetBool("selected", false);
    }

    public void Submit()
    {
        OnSubmit?.Invoke();
    }

    public void OnSwitchInput() => iC.SwitchInputManual();

    public event Action OnSubmit;
}
