using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    [SerializeField] private Image image;
    private Slider slider;
    private PlayerActor playerHealth;
    [SerializeField]
    private ScriptableInt health, maxHealth;
    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerActor>();
        slider = GetComponent<Slider>();

        //slider.maxValue = playerHealth.Health;
        //slider.value = playerHealth.Health;

        slider.maxValue = maxHealth.value;
        slider.value = health.value;

        playerHealth.HealthChange += ImageUpdate;
    }

    private void ImageUpdate()
    {
        slider.value = health.value;
        //image.fillAmount - tem que se usar o image fill amount que vai de 0 a 1, em vez do slider,
    }
}
