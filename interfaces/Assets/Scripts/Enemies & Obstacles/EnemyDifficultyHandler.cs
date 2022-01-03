using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDifficultyHandler : MonoBehaviour
{
    [SerializeField]
    private bool isStartingLevel = false;
    [Header("Number of Enemies")]
    [SerializeField, Range(0, 30)]
    private int easy = 1;
    [SerializeField, Range(0, 30)]
    private int medium = 1;
    [SerializeField, Range(0, 30)]
    private int hard = 1;

    [SerializeField]
    private EnemyActor[] numberOfEnemies;
    public int MaxNumberOfEntities => (OptionsManager.instance.EnemyAndObstacles == Difficulty.Easy ? easy :
                OptionsManager.instance.EnemyAndObstacles == Difficulty.Medium ? medium : hard);

    public int CurrentEntities { get => currentEntities; set => currentEntities = value; }

    [SerializeField]
    private int currentEntities;

    private void Awake()
    {
        numberOfEnemies = GetComponentsInChildren<EnemyActor>(true);

        OptionsManager.instance.OnEnemyChange += Modifier;

        Modifier();

        if (isStartingLevel)
            gameObject.SetActive(false);
    }
    public GameObject AddEntity(GameObject obs, Vector3 position, float speed)
    {
        GameObject enemy = Instantiate(obs, position, Quaternion.identity, transform);
        enemy.GetComponent<EnemyActor>().Speed = speed;
        CurrentEntities++;
        return enemy;
    }


    private void Modifier()
    {
        return;
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
