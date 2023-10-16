using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private Health playerHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth.GameOver();
        }
    }
}
