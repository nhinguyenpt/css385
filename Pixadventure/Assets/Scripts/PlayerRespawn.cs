using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Transform currentCheckpoint;
    private Player playerHealth;

    private void Awake()
    {        
        playerHealth = GetComponent<Player>();
    }

    private void Respawn()
    {
        transform.position = currentCheckpoint.position;
    }
}
