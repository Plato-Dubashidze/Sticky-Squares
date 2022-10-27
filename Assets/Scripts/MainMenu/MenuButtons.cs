using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    private bool isStarted;

    public void StartGameButton()
    {
        if (!isStarted)
        {
            isStarted = true;
            StartCoroutine(LoadScene("LevelMenu"));
        }
    }

    public void ExitGameButton()
    {
        StartCoroutine(Exit());
    }

    public void CreditsButton()
    {
        StartCoroutine(LoadScene("CreditsMenu"));
    }

    public void BackToMenuButton()
    {
        StartCoroutine(LoadDelayBeforeSceneLoad("MainMenu"));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        GlobalEventManager.endLevel();
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator Exit()
    {
        GlobalEventManager.endLevel();
        yield return new WaitForSeconds(1.3f);
        Application.Quit();
    }

    private IEnumerator LoadDelayBeforeSceneLoad(string sceneName)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }

}
