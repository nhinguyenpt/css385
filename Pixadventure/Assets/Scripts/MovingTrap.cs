using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrap : BasicTrap
{
    private Vector3 _initPos;
    private void Awake()
    {
        _initPos = transform.position;
    }
    
    public void ResetPos()
    {
        transform.position = _initPos;
    }
}
