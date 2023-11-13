using System.Collections;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameover;
    [SerializeField] private GameObject victory;
    [SerializeField] private GameObject saveText;
    [SerializeField] private GameObject loadText;
    [SerializeField] private GameObject helpF1;
    [SerializeField] private GameObject help;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ShowHelp();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideHelp();
        }
    }

    public void GameOver()
    {
        gameover.SetActive(true);
    }

    public void Victory()
    {
        victory.SetActive(true);
    }

    public void Reset()
    {
        gameover.SetActive(false);
        victory.SetActive(false);
        saveText.SetActive(false);
        loadText.SetActive(false);
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

    public void ShowHelp()
    {
        help.SetActive(true);
        helpF1.SetActive(false);
    }

    public void HideHelp()
    {
        help.SetActive(false);
        helpF1.SetActive(false);
    }

    IEnumerator RemoveAfterSeconds(float seconds, GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
    }
}
