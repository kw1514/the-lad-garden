using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 1f;


    public void LoadGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void LoadTitleScreen()
    {
        // SceneManager.LoadScene("TitleScreen");
        StartCoroutine(WaitAndLoad("TitleScreen", sceneLoadDelay));
    }

    public void QuitGame()
    {
        
        // Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
