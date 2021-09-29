using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDifficultyHandler : MonoBehaviour
{
    [Header("Number of Obstacles")]
    [SerializeField, Range(1, 30)]
    private int easy = 1;
    [SerializeField, Range(1, 30)]
    private int medium = 1;
    [SerializeField, Range(1, 30)]
    private int hard = 1;

    private ObstacleActor[] numberOfObstacles;

    private void Awake()
    {
        numberOfObstacles = GetComponentsInChildren<ObstacleActor>(true);

        OptionsManager.instance.OnEnemyChange += Modifier;

        Modifier();
    }

    private void Modifier()
    {
        try
        {
            int i = numberOfObstacles.Length - 1;

            for (; i >= (OptionsManager.instance.EnemyAndObstacles == Difficulty.Easy ? easy :
                OptionsManager.instance.EnemyAndObstacles == Difficulty.Medium ? medium : hard); i--)
                numberOfObstacles[i].gameObject.SetActive(false);

            for (; i >= 0; i--)
                numberOfObstacles[i].gameObject.SetActive(true);
        }
        catch (MissingReferenceException)
        {

        }
    }
}
