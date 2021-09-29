using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float walkingVelocity;
    [SerializeField]
    private float fallingVelocityCap;
    [SerializeField, Range(0f, 1f)]
    private float speedMultiplier;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float radius;
    [SerializeField]
    private LayerMask mask;
    [SerializeField]
    private float punchTime;
    [SerializeField]
    private GameObject startButton;

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private GameObject hitbox;
    private Rigidbody2D rB;
    private PlayerInputHandler pI;

    public float WalkingVelocity { get => walkingVelocity; }

    private Animator anim;

    private WaitForSeconds seconds;

    public bool CanRun { get; set; }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rB = GetComponent<Rigidbody2D>();
        pI = GetComponent<PlayerInputHandler>();

        pI.Jump += Jump;
        pI.Punch += Punch;
        seconds = new WaitForSeconds(punchTime);

        if (startButton == null)
        {
            CanRun = true;
            gameObject.GetComponent<Animator>().SetTrigger("Running");
        }

    }

    private void FixedUpdate()
    {
        if (CanRun)
        {
            Vector2 vel = rB.velocity;
            vel.x = walkingVelocity;

            if (rB.velocity.y < 0)
            {
                vel.y = Mathf.Clamp(vel.y - speedMultiplier, -fallingVelocityCap, 0);
                anim.SetBool("Jump", false);
            }

            rB.velocity = vel;
        }
    }

    private void Jump()
    {
        if (!GroundCheck()) return;

        Vector2 vel = rB.velocity;

        // muda de acordo com a dificuldade, fica mais 'floaty' para ser mais fácil
        vel.y = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);


        rB.velocity = vel;
    }
    private void Punch()
    {
        StartCoroutine("HitboxTimer");
    }

    private IEnumerator HitboxTimer()
    {
        hitbox.SetActive(true);
        yield return seconds;
        hitbox.SetActive(false);
    }

    private bool GroundCheck() =>
        Physics2D.OverlapCircle(groundCheck.position, radius, mask);
}
