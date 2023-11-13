using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    public float currentHealth { get; private set; }
    private Animator ani;
    private UIManager uiManager;
    private int currentLevel;

    private void Awake()
    {
        currentHealth = maxHealth;
        ani = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>();
        currentLevel = 0;
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
            SceneManager.LoadScene(currentLevel);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    public void Save()
    {
        uiManager.Saving();
        SaveLoadSystem.SaveState(this);
    }

    public void Load()
    {
        //uiManager.Reset();
        //ani.SetTrigger("idle");
        //SceneManager.LoadScene(currentLevel);
        PlayerData data = SaveLoadSystem.LoadState();

        currentHealth = data.health;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
        uiManager.Loading();
    }
}
