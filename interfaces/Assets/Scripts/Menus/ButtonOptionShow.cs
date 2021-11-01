using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonOptionShow : MonoBehaviour
{
    [SerializeField]
    private bool playerChange = true;


    [SerializeField]
    private string easy = "MUITA";
    [SerializeField]
    private string medium = "MÉDIA";
    [SerializeField]
    private string hard = "POUCA";

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();

        OptionsManager.instance.OnHealthChange += Modifier;
        OptionsManager.instance.OnEnemyChange += Modifier;


        Modifier();
    }

    private void Modifier()
    {
        if (playerChange)
            text.text = OptionsManager.instance.PlayerHealthBase == Difficulty.Easy ? easy 
            : OptionsManager.instance.PlayerHealthBase == Difficulty.Medium ? medium : hard;
        else
            text.text = OptionsManager.instance.EnemyAndObstacles == Difficulty.Easy ? easy
            : OptionsManager.instance.EnemyAndObstacles == Difficulty.Medium ? medium : hard;
    }
}
