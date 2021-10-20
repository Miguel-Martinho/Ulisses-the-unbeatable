using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertPunchActivator : MonoBehaviour
{
    public GameObject alert;
    private Collider2D coll;

    private bool enemyInRange;

    private void Start()
    {
        coll = GetComponent < Collider2D>();
    }

    private void Update()
    {        
        
        //int layerMask = ~LayerMask.GetMask("Enemy");

        //bool tteste = Physics.BoxCast(coll.bounds.center,
        //    coll.bounds.extents, new Vector3(0,0,0),Quaternion.identity, 10,layerMask: 1 << LayerMask.NameToLayer("Enemy"));

        //Debug.Log(tteste);
    }

    
    private void FixedUpdate()
    {
        alert.SetActive(enemyInRange);
        enemyInRange = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {      
        if(collision.gameObject.tag == "enemy")
        {
            enemyInRange = true; 
        }
    }
}
