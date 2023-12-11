using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    [Header("Audio")]
    [SerializeField] private AudioClip victorySound;
    [SerializeField] private AudioClip deathSound;
    public float CurrentHealth { get; private set; }
    private Animator _animator;
    private UIManager _uiManager;
    private string _currentLevel;
    private bool _victory;
    private void Awake()
    {
        CurrentHealth = maxHealth;
        _animator = GetComponent<Animator>();
        _uiManager = FindObjectOfType<UIManager>();
        _currentLevel = SceneManager.GetActiveScene().name;
        _victory = false;
    }

    public void TakeDamage(float _damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - _damage, 0, maxHealth);

        if (CurrentHealth == 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        GetComponent<PlayerMovement>().enabled = false;
        if (CurrentHealth == 0)
        {
            SoundManager.Instance.PlaySound(deathSound);
            _animator.SetTrigger("die");
            _uiManager.GameOver();
        } else 
        {
            SoundManager.Instance.StopBackground();
            SoundManager.Instance.PlaySound(victorySound);
            _animator.SetTrigger("victory");
            _uiManager.Victory();
            _victory = true;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(_currentLevel);
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
        _uiManager.Saving();
        SaveLoadSystem.SaveState(this);
    }

    public void Load()
    {
        _uiManager.Reset();
        _animator.SetTrigger("reload");

        GetComponent<PlayerMovement>().enabled = true;
        PlayerData data = SaveLoadSystem.LoadState();
        CurrentHealth = data.health;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
        _uiManager.Loading();

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
