using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ScoreImpact : MonoBehaviour
{
    public static ScoreImpact scoreImpact;
    private GameObject[] enemy;
    private int enemyTotal;

    [SerializeField]
    public AudioMixer MainAudioMixer;
    [SerializeField]
    private LevelManager sceneManager;
    [SerializeField]
    PlayerController playerController;
    [SerializeField]
    private Image[] marks;
    private int currentLevel;
    private int totalScenes;
    public float increment;
    private float loadNextLevelTimer = 5f;
    private bool levelEnded = false;
    private Slider sli;

    void Awake() {
        if (scoreImpact == null)
            scoreImpact = this;
        else
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        sli = GetComponent<Slider>();
        currentLevel = sceneManager.currentScene;
        totalScenes = sceneManager.sceneTotal;
        enemyTotal = GameObject.FindGameObjectsWithTag("Building").Length;
        increment = (((float)enemyTotal + 1f) / 100f) * 10;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if(currentLevel + 1 < totalScenes)
                sceneManager.LoadScene(currentLevel + 1);
            else
                sceneManager.ReturnMenu();
        }
        if(levelEnded){
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

    // Update is called once per frame
    public void Score()
    {
        Debug.Log("increment " + increment);
        sli.value += increment * 10;
        /*if (sli.value >= 20)
        {
            MainAudioMixer.SetFloat("AmbientVolume", -80.0f);
            MainAudioMixer.SetFloat("EmergencyVolume", -20.0f);
        }*/
        if (sli.value >= 52)
        {
            marks[0].enabled = true;
        }
        if (sli.value >= 70)
        {
            marks[1].enabled = true;
        }
        if (sli.value >= 88)
        {
            marks[2].enabled = true;
        }
        if (sli.value >= 95)
        {
            marks[3].enabled = true;
        }

        //StartCoroutine("ScoreTally");
        if (sli.value == sli.maxValue || playerController.strikesAvailable == 0)
            levelEnded = true;
    }

        /*enemy = GameObject.FindGameObjectsWithTag("Building");*/
        /*for (i = 0; i <= j; i++)
        {
            Debug.Log(i);
            sli.value = (j/ (float)enemyTotal)*100;
            
        }
        j = (enemyTotal+2) - enemy.Length;*/
        //sli.value = ((((float)enemyTotal)-(float)enemy.Length+1)/enemyTotal)*10;

}

    /*IEnumerator ScoreTally() {
        yield return new WaitForSeconds(3);
        if (sli.value == enemy.Length)
        {
            if (playerController.strikesAvailable <= 2)
            {
                Debug.Log("yey");
                sli.value = sli.value * 2;
                yield return new WaitForSeconds(3);
                sceneManager.LoadScene(1);

            }
            else {
                Debug.Log("nop");
                yield return new WaitForSeconds(3);
                sceneManager.LoadScene(0);
            }
            
        }
        else { 
            Debug.Log("nay"); 
        }

    }*/
    
