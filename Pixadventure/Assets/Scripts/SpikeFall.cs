using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeFall : MovingTrap
{
    [SerializeField] private float gravity;
    [SerializeField] private float range;
    [SerializeField] private LayerMask playerLayer;

    private const float RayLength = 10;

    private void Update()
    {
        Vector3 rayPos = new Vector3(transform.position.x - range, transform.position.y, transform.position.z);
        Debug.DrawRay(rayPos, -transform.up * RayLength, Color.blue);
        RaycastHit2D hit = Physics2D.Raycast(rayPos, -transform.up, RayLength, playerLayer);
        
        if (hit.collider != null)
        {
            Debug.Log("Trigger Falling Spike Trap");
            this.GetComponent<Rigidbody2D>().gravityScale = gravity;
        }
    }

    public void ResetPos()
    {
        Debug.Log("Reset Falling Spike Trap");
        Rigidbody2D body = this.GetComponent<Rigidbody2D>();
        body.gravityScale = 0;
        body.velocity = new Vector2(0, 0);
        base.ResetPos();
    }


}
