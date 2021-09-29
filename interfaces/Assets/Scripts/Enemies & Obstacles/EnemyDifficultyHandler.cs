using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDifficultyHandler : MonoBehaviour
{
    [SerializeField]
    private bool isStartingLevel = false;
    [Header("Number of Enemies")]
    [SerializeField, Range(1, 30)]
    private int easy = 1;
    [SerializeField, Range(1, 30)]
    private int medium = 1;
    [SerializeField, Range(1, 30)]
    private int hard = 1;

    private EnemyActor[] numberOfEnemies;

    private void Awake()
    {
        numberOfEnemies = GetComponentsInChildren<EnemyActor>(true);

        OptionsManager.instance.OnEnemyChange += Modifier;

        Modifier();

        if (isStartingLevel)
            gameObject.SetActive(false);
    }

    private void Modifier()
    {
        print("here");

        int i = numberOfEnemies.Length - 1;

        try
        {
            for (; i >= (OptionsManager.instance.EnemyAndObstacles == Difficulty.Easy ? easy :
                        OptionsManager.instance.EnemyAndObstacles == Difficulty.Medium ? medium : hard); i--)
                numberOfEnemies[i].gameObject.SetActive(false);

            for (; i >= 0; i--)
                numberOfEnemies[i].gameObject.SetActive(true);
        }
        catch (MissingReferenceException)
        {

        }
            
    }
}
