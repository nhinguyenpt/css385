using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    public float currentHealth { get; private set; }
    private Animator ani;
    private UIManager uiManager;

    private void Awake()
    {
        currentHealth = maxHealth;
        ani = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, maxHealth);

        if (currentHealth == 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        GetComponent<PlayerMovement>().enabled = false;
        if (currentHealth == 0)
        {
            ani.SetTrigger("die");
            uiManager.GameOver();
        } else 
        {
            ani.SetTrigger("victory");
            uiManager.Victory();
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }
}
