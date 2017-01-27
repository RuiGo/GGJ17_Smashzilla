using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreImpact : MonoBehaviour {
    private int currentLevel;
    private int totalScenes;
    private float individualObjectValue;
    private float loadNextLevelTimer = 10f;
    private bool levelEnded = false;
    private Slider sli;
    private int totalObjectives;

    //[SerializeField]
    //private LevelManager sceneManager;

    [SerializeField]
    private Image[] marks;

    public static ScoreImpact scoreImpact;

    

    void Awake() {
        if (scoreImpact == null)
            scoreImpact = this;
        else
            Destroy(gameObject);
    }

    void Start() {
        sli = GetComponent<Slider>();
        //currentLevel = sceneManager.currentScene;
        //totalScenes = sceneManager.sceneTotal;
        totalObjectives = GameObject.FindGameObjectsWithTag("Building").Length;
        totalObjectives += GameObject.FindGameObjectsWithTag("ElectricTower").Length;

        individualObjectValue = 100f / totalObjectives;
    }

    void Update() {
        //print(sli.value);
        if (Input.GetKeyDown(KeyCode.N)) {
            /*if(currentLevel + 1 < totalScenes)
                sceneManager.LoadScene(currentLevel + 1);
            else
                sceneManager.ReturnMenu();*/
        }
        if(levelEnded) {
            /*if(loadNextLevelTimer > 0) {
                loadNextLevelTimer -= Time.deltaTime;
            } else {
                if(currentLevel + 1 < totalScenes)
                    sceneManager.LoadScene(currentLevel + 1);
                else
                    sceneManager.ReturnMenu();
            }*/
        }
    }

    public void Score() {
        sli.value += individualObjectValue;
        if (sli.value >= 20) {
            SoundStorage.soundStorage.mainAudioMixer.SetFloat("EmergencyVolume", -20.0f);
            SoundStorage.soundStorage.mainAudioMixer.SetFloat("SirensVolume", -10.0f);
            SoundStorage.soundStorage.mainAudioMixer.SetFloat("CrowdsVolume", -10.0f);
        }
        if (sli.value >= 52) {
            marks[0].enabled = true;
            SoundStorage.soundStorage.mainAudioMixer.SetFloat("EmergencyVolume", -10.0f);
            SoundStorage.soundStorage.mainAudioMixer.SetFloat("SirensVolume", -5.0f);
            SoundStorage.soundStorage.mainAudioMixer.SetFloat("CrowdsVolume", -5.0f);
        }
        if (sli.value >= 70) {
            marks[1].enabled = true;
            SoundStorage.soundStorage.mainAudioMixer.SetFloat("EmergencyVolume", 0.0f);
        }
        if (sli.value >= 88) 
            marks[2].enabled = true;          
        if (sli.value > 99) 
            marks[3].enabled = true;

        /*if (sli.value > 99 || playerStatistics.strikesAvailable <= 0)
            levelEnded = true;*/
    }
}
    
