using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    [Header("Audio")]
    [SerializeField] private AudioClip victorySound;
    [SerializeField] private AudioClip deathSound;
    public float currentHealth { get; private set; }
    private Animator animator;
    private UIManager uiManager;
    private string currentLevel;

    private void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>();
        currentLevel = SceneManager.GetActiveScene().name;
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
            SoundManager.instance.PlaySound(deathSound);
            animator.SetTrigger("die");
            uiManager.GameOver();
        } else 
        {
            SoundManager.instance.StopBackground();
            SoundManager.instance.PlaySound(victorySound);
            animator.SetTrigger("victory");
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
        uiManager.Reset();
        animator.SetTrigger("reload");
        GetComponent<PlayerMovement>().enabled = true;
        // animator.SetTrigger("idle");
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
