using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameover;
    [SerializeField] private GameObject victory;
    [SerializeField] private GameObject saveText;
    [SerializeField] private GameObject loadText;

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

    public void Saving()
    {
        StartCoroutine(RemoveAfterSeconds(0.5f, saveText));
    }

    public void Loading()
    {
        StartCoroutine(RemoveAfterSeconds(0.3f, loadText));
    }

    IEnumerator RemoveAfterSeconds(float seconds, GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
    }
}
