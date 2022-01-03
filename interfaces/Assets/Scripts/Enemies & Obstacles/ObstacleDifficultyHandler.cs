using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDifficultyHandler : MonoBehaviour
{
    [Header("Number of Obstacles")]
    [SerializeField, Range(0, 30)]
    private int easy = 1;
    [SerializeField, Range(0, 30)]
    private int medium = 1;
    [SerializeField, Range(0, 30)]
    private int hard = 1;

    private ObstacleActor[] numberOfObstacles;
    public int MaxNumberOfEntities => (OptionsManager.instance.EnemyAndObstacles == Difficulty.Easy ? easy :
                OptionsManager.instance.EnemyAndObstacles == Difficulty.Medium ? medium : hard);

    public int CurrentEntities { get => currentEntities; set => currentEntities = value; }

    [SerializeField]
    private int currentEntities;
   // public int CurrentEntities { get; private set; }
    
    private void Awake()
    {
        //numberOfObstacles = GetComponentsInChildren<ObstacleActor>(true);

        OptionsManager.instance.OnEnemyChange += Modifier;

        Modifier();
    }

    public void AddEntity(GameObject obs, Vector3 position)
    {
        Instantiate(obs, position, Quaternion.identity, transform);
        CurrentEntities++;
    }

    private void Modifier()
    {
        return;
        try
        {
            int i = numberOfObstacles.Length - 1;

            for (; i >= MaxNumberOfEntities; i--)
                numberOfObstacles[i].gameObject.SetActive(false);

            for (; i >= 0; i--)
                numberOfObstacles[i].gameObject.SetActive(true);
        }
        catch (MissingReferenceException)
        {

        }
    }
}
