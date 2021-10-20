using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyActor : DamageActor
{
    private PlayerMovement player;
    private Rigidbody2D playerRb;
    private Rigidbody2D rb;
    [SerializeField]
    private GameObject hurtbox, upwardsHitbox;

    [SerializeField]
    private int   speed;
    [SerializeField]
    private float attackBuildup, attackCooldown, attackDuration;
    private WaitForSeconds concreteAttackBuildup, concreteAttackCooldown, concreteAttackDuration;

    [SerializeField]
    private bool attackFromUpwards = false;
    private bool reachedPlayer;
    [SerializeField]
    private bool canAttack;
    private bool isDead;
    private void Awake()
    {
        Health = baseHealth;

        player = FindObjectOfType<PlayerMovement>();
        playerRb = player.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
        

        concreteAttackBuildup  = new WaitForSeconds(attackBuildup);
        concreteAttackCooldown = new WaitForSeconds(attackCooldown);
        concreteAttackDuration = new WaitForSeconds(attackDuration);
        reachedPlayer = false;

        OnDeath += Death;
        gameObject.GetComponent<Animator>().SetTrigger("Running");
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        Chase();

        if (reachedPlayer) Attack();
    }

    private void Chase()
    {
        Vector2 vel = rb.velocity;

        Vector2 p = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 t = new Vector2(transform.position.x, transform.position.y);

        if (Vector2.Dot(t - p, Vector2.right) < 0)
        {
            vel.x = speed > player.WalkingVelocity ? speed : player.WalkingVelocity + speed;
            rb.velocity = vel;
        }
        else
        {
            vel.x = player.WalkingVelocity;
            rb.velocity = vel;            
            reachedPlayer = true;
        }
    }

    private void Attack()
    {
        if (canAttack)
        {
            StartCoroutine("AttackCooldown");
        }
    }

    private void Death()
    {
        //GetComponent<Collider2D>().enabled = false;
        isDead = true;
        GetComponent<Animator>().Play("die");
        //mudar depois
        //Destroy(gameObject);
    }

        IEnumerator AttackCooldown()
    {
        yield return concreteAttackBuildup;

        if (attackFromUpwards && playerRb.velocity.y > 0)
            upwardsHitbox.SetActive(true);
        else
            hurtbox.SetActive(true);

        canAttack = false;

        yield return concreteAttackDuration;

        hurtbox.SetActive(false);
        if (attackFromUpwards) upwardsHitbox.SetActive(false);

        yield return concreteAttackCooldown;

        canAttack = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Health--;
        }
    }
}
