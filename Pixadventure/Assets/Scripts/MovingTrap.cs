using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrap : BasicTrap
{
    private Vector3 initPos;
    private void Awake()
    {
        initPos = transform.position;
    }
    
    public void ResetPos()
    {
        transform.position = initPos;
    }
}
