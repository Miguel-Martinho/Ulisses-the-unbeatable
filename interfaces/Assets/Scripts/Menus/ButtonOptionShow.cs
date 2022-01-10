using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonOptionShow : MonoBehaviour
{
    [SerializeField]
    private bool playerChange = true;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Image background, knob;

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
        {
            text.text = OptionsManager.instance.PlayerHealthBase == Difficulty.Easy ? easy
            : OptionsManager.instance.PlayerHealthBase == Difficulty.Medium ? medium : hard;

            slider.value = OptionsManager.instance.PlayerHealthBase == Difficulty.Easy ? 0
         : OptionsManager.instance.PlayerHealthBase == Difficulty.Medium ? 1 : 2;

            background.color = OptionsManager.instance.PlayerHealthBase == Difficulty.Easy ? new Color32(163, 202, 43, 255)
            : OptionsManager.instance.PlayerHealthBase == Difficulty.Medium ? new Color32(202, 141, 43, 255) : new Color32(202, 56, 43, 255);

            knob.color = OptionsManager.instance.PlayerHealthBase == Difficulty.Easy ? new Color32(83, 255,0, 255)
            : OptionsManager.instance.PlayerHealthBase == Difficulty.Medium ? new Color32(255, 111, 0, 255) : new Color32(255, 0, 38, 255);
        }
        else
        {
            text.text = OptionsManager.instance.EnemyAndObstacles == Difficulty.Easy ? easy
            : OptionsManager.instance.EnemyAndObstacles == Difficulty.Medium ? medium : hard;

            slider.value = OptionsManager.instance.EnemyAndObstacles == Difficulty.Easy ? 0
            : OptionsManager.instance.EnemyAndObstacles == Difficulty.Medium ? 1 : 2;

            background.color = OptionsManager.instance.EnemyAndObstacles == Difficulty.Easy ? new Color32(163, 202, 43, 255)
            : OptionsManager.instance.EnemyAndObstacles == Difficulty.Medium ? new Color32(202, 141, 43, 255) : new Color32(202, 56, 43, 255);

            knob.color = OptionsManager.instance.EnemyAndObstacles == Difficulty.Easy ? new Color32(83, 255,0, 255)
            : OptionsManager.instance.EnemyAndObstacles == Difficulty.Medium ? new Color32(255, 111, 0, 255) : new Color32(255, 0, 38, 255);
        }

     
    }
}
