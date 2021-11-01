using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextStatDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI lives, enemies;

    [SerializeField]
    private ScriptableInt health, maxHealth;

    // Update is called once per frame
    void Update()
    {
        lives.text = $"VIDAS: {health.value} / {maxHealth.value}";
    }
}
