using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Transform currentCheckpoint;
    private Player player;

    private void Awake()
    {        
        player = GetComponent<Player>();
    }

    private void Respawn()
    {
        transform.position = currentCheckpoint.position;
    }
}
