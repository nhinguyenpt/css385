using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameover;
    [SerializeField] private GameObject victory;

    public void GameOver()
    {
        gameover.SetActive(true);
    }

    public void Victory()
    {
        victory.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
