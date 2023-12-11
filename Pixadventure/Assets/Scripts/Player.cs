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
    private bool _victory;
    private void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>();
        currentLevel = SceneManager.GetActiveScene().name;
        _victory = false;
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
            _victory = true;
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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.C) && _victory)
        {
            NextLevel();
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
        PlayerData data = SaveLoadSystem.LoadState();
        currentHealth = data.health;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
        uiManager.Loading();

        ResetMovingTraps();
    }

    private void ResetMovingTraps()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag ("MovingTrap");
        foreach (var gameObject in gameObjects)
        {
            SpikeFall trap = gameObject.GetComponent<SpikeFall>();
            if (trap != null)
                trap.ResetPos();
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
