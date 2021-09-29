using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class DamageActor : MonoBehaviour
{
    [SerializeField]
    [Range(0, 20)]
    protected int baseHealth;

    [SerializeField]
    [Range(0, 5)]
    protected int damageToDeal;

    public int Health { get; protected set; }
    public int Damage { get => damageToDeal; }

    private void Update()
    {
        if (Health <= 0)
            OnDeath?.Invoke();
    }

    public event Action<int> OnHealthChange;
    public event Action      OnDeath;
}
