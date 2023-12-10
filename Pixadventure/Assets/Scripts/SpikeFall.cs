using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeFall : BasicTrap
{
    [SerializeField] private float gravity;
    [SerializeField] private float range;
    [SerializeField] private LayerMask playerLayer;

    private Vector3[] directions = new Vector3[2];

    private void Update()
    {
        findProximity();

        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.blue);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null)
            {
                Debug.Log("Trigger Falling Trap");
                this.GetComponent<Rigidbody2D>().gravityScale = gravity;
            }
        }
    }

    private void findProximity()
    {
        directions[0] = transform.right * range;
        directions[1] = -transform.right * range;
    }
}
