using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    public int currentScene;
    public int sceneTotal;

    //public Slider fill;

    // Use this for initialization
    void Awake()  {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        sceneTotal = SceneManager.sceneCountInBuildSettings;
    }

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	 public void LoadScene(int i) {
        SceneManager.LoadScene(i);
        //fill.value = 0.0f;
    }

    public void ReturnMenu() {
        SceneManager.LoadScene(0);
    }

    public void Quit() {
        Application.Quit();
    }

}

