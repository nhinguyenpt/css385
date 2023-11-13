using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FloorTrap : MonoBehaviour
{
    public Player player;

    // Update is called once per frame
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player.GameOver();
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        player.TakeDamage(100);
    }
}
