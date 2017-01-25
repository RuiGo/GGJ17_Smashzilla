using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ScoreImpact : MonoBehaviour {
    private int currentLevel;
    private int totalScenes;
    private float individualObjectValue;
    private float loadNextLevelTimer = 10f;
    private bool levelEnded = false;
    private Slider sli;
    private int totalObjectives;

    [SerializeField]
    private LevelManager sceneManager;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private Image[] marks;

    public static ScoreImpact scoreImpact;
    public AudioMixer MainAudioMixer;

    

    void Awake() {
        if (scoreImpact == null)
            scoreImpact = this;
        else
            Destroy(gameObject);
    }

    void Start() {
        sli = GetComponent<Slider>();
        currentLevel = sceneManager.currentScene;
        totalScenes = sceneManager.sceneTotal;
        totalObjectives = GameObject.FindGameObjectsWithTag("Building").Length;
        totalObjectives += GameObject.FindGameObjectsWithTag("ElectricTower").Length;
        totalObjectives += GameObject.FindGameObjectsWithTag("Vehicle").Length;

        //TODO: check calculation
        individualObjectValue = 100 / totalObjectives;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.N)) {
            if(currentLevel + 1 < totalScenes)
                sceneManager.LoadScene(currentLevel + 1);
            else
                sceneManager.ReturnMenu();
        }
        if(levelEnded) {
            if(loadNextLevelTimer > 0) {
                loadNextLevelTimer -= Time.deltaTime;
            } else {
                if(currentLevel + 1 < totalScenes)
                    sceneManager.LoadScene(currentLevel + 1);
                else
                    sceneManager.ReturnMenu();
            }
        }
    }

    //TODO: make it so it is called only when scored
    public void Score() {
        Debug.Log("Building Value: " + individualObjectValue);
        sli.value += individualObjectValue;
        if (sli.value >= 20) {
            //start playing sfx?
            //MainAudioMixer.SetFloat("AmbientVolume", -80.0f);
           // MainAudioMixer.SetFloat("EmergencyVolume", -20.0f);
        } else if (sli.value >= 52)  
            marks[0].enabled = true;
        else if (sli.value >= 70) 
            marks[1].enabled = true;
        else if (sli.value >= 88) 
            marks[2].enabled = true;
        else if (sli.value >= 95) 
            marks[3].enabled = true;
        else if (sli.value > 100) //é necessário?
            sli.value = 100;

        if (sli.value == sli.maxValue || playerController.strikesAvailable == 0)
            levelEnded = true;
    }
}
    
