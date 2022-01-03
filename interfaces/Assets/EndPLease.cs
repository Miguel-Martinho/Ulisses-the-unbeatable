using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPLease : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.layer == 10)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerMovement>().CanRun = false;
            player.GetComponent<Animator>().Play("Celebrate");
            // EndScreen();
        }
    }
}
