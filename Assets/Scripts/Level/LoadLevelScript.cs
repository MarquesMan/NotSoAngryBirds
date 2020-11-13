using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelScript : MonoBehaviour
{

    // Start is called before the first frame update
    public void LoadLevelByGameObjectName()
    {
        LoadLevel(gameObject.name);
    }

    public void LoadLevelByLevelName(string levelName)
    {
        LoadLevel(levelName);
    }

    public void LoadNextLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestarLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        LoadLevel(0);
    }

    private void LoadLevel(int buildID)
    {
        if (LoadingScreen.Instance)
            LoadingScreen.Instance.DisplayLoadingScreen(SceneManager.LoadSceneAsync(buildID));
        else
            SceneManager.LoadScene(buildID);
    }

    private void LoadLevel(string levelName)
    {
        if (LoadingScreen.Instance)
            LoadingScreen.Instance.DisplayLoadingScreen(SceneManager.LoadSceneAsync(levelName));
        else
            SceneManager.LoadScene(levelName);
    }

}
