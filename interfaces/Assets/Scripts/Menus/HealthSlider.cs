using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    [SerializeField] private Image image;
    private Slider slider;
    private PlayerActor playerHealth;

    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerActor>();
        slider = GetComponent<Slider>();

        slider.maxValue = playerHealth.Health;
        slider.value = playerHealth.Health;

        playerHealth.HealthChange += ImageUpdate;
    }

    private void ImageUpdate(int newNumb)
    {
        slider.value -= newNumb;
        //image.fillAmount - tem que se usar o image fill amount que vai de 0 a 1, em vez do slider,
    }
}
