using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerActor : MonoBehaviour
{
    [SerializeField][Range(0, 20)]
    private int baseHealth;
    public int Health { get; private set; }

    [SerializeField]
    private float invulTime = 0.3f;
    public bool   invulnerable;

    private void Awake()
    {
        OptionsManager.instance.OnHealthChange += Modifier;

        Health = baseHealth;
        invulnerable = false;

        Modifier();

        HealthChange += Damage;
        Death += KillPlayer;
    }

    private void Update()
    {
        if (Health <= 0)
        {
            Death?.Invoke();
        }
    }

    private void Damage(int dmgDealt)
    {
        Health -= dmgDealt;
    }

    private void KillPlayer()
    {
        Destroy(gameObject, 0.2f);
    }

    private void Modifier()
    {
        switch (OptionsManager.instance.PlayerHealthBase)
        {
            case Difficulty.Easy:
                Health = Mathf.Clamp(20, 1, 20);
                invulTime = 1;
                break;
            case Difficulty.Medium:
                Health = Mathf.Clamp(15, 1, 20);
                invulTime = 1.5f;
                break;
            case Difficulty.Hard:
                invulTime = 0.3f;
                Health = baseHealth;
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (invulnerable || collision.gameObject.layer != 11) return;

        // Bad solution but whatever
        int dmg;
        dmg = collision.gameObject.GetComponentInParent<DamageActor>().Damage;

        StartCoroutine("InvulCountdown");
        HealthChange?.Invoke(dmg);
    }

    private IEnumerator InvulCountdown()
    {
        invulnerable = true;

        // Invul Time can change in playtime or options, so its not constant
        yield return new WaitForSeconds(invulTime);

        invulnerable = false;
    }

    public event Action<int> HealthChange;
    public event Action      Death;
}
