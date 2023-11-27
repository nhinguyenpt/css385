using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FloorTrap : MonoBehaviour
{
    public Player player;

    private void OnTriggerEnter2D(Collider2D target)
    {
        print("FloorTrap damage");
        player.TakeDamage(100);
    }
}
