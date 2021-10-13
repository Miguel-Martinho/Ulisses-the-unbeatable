using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OptionsManager : MonoBehaviour
{
    public static OptionsManager instance;

    [SerializeField]
    private ButtonInfo LifeEvent;
    [SerializeField]
    private ButtonInfo EnemyEvent;
    [SerializeField]
    private ButtonInfo LifeEventS;
    [SerializeField]
    private ButtonInfo EnemyEventS;

    public Difficulty PlayerHealthBase  { get; private set; }
    public Difficulty EnemyAndObstacles { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            LifeEvent.OnSubmit += HealthChange;
            EnemyEvent.OnSubmit += EnemyChange;
            LifeEventS.OnSubmit += HealthChange;
            EnemyEventS.OnSubmit += EnemyChange;

            Destroy(gameObject);
            return;
        }

        instance = this;

        LifeEvent.OnSubmit  += HealthChange;
        EnemyEvent.OnSubmit += EnemyChange;
        //LifeEventS.OnSubmit += HealthChange;
        //EnemyEventS.OnSubmit += EnemyChange;

        DontDestroyOnLoad(gameObject);
    }

    private void HealthChange()
    {
        print(PlayerHealthBase);

        switch (PlayerHealthBase)
        {
            case Difficulty.Easy:
                PlayerHealthBase = Difficulty.Medium;
                break;
            case Difficulty.Medium:
                PlayerHealthBase = Difficulty.Hard;
                break;
            case Difficulty.Hard:
                PlayerHealthBase = Difficulty.Easy;
                break;
        }

        OnHealthChange?.Invoke();
    }

    private void EnemyChange()
    {
        print(EnemyAndObstacles);

        switch (EnemyAndObstacles)
        {
            case Difficulty.Easy:
                EnemyAndObstacles = Difficulty.Medium;
                break;
            case Difficulty.Medium:
                EnemyAndObstacles = Difficulty.Hard;
                break;
            case Difficulty.Hard:
                EnemyAndObstacles = Difficulty.Easy;
                break;
        }

        OnEnemyChange?.Invoke();
    }

    public event Action OnHealthChange, OnEnemyChange;
}

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}
