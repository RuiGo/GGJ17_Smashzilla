using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public static LevelManager levelManager;

    void Awake()  {
        if (levelManager == null)
            levelManager = this;
        else
            Destroy(gameObject);
    }

    public void RestartLevel() {
        print("RestartLevel");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadFirstLevel() {
        SceneManager.LoadScene("Level1");
    }

    public void LoadNextLevel() {
        print("NextLevel");
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index + 1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(index + 1);
        else
            SceneManager.LoadScene("MainMenu");
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit() {
        Application.Quit();
    }

}

