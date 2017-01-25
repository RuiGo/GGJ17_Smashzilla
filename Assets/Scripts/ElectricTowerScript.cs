using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricTowerScript : MonoBehaviour {

    private bool startCounter;
    private ElectricTowerScript leftTowerScript;
    private ElectricTowerScript rightTowerScript;
    private GameObject normalTower;
    private GameObject destroyedTower;
    private WaveImpactScript waveScript;
    private ParticleSystem waveParticles;

    public bool isAlive;
    public float timeToWait = 0.5f;
    public GameObject leftTower;
    public GameObject rightTower;
    public GameObject wave;
    public float waveMaxRadius = 0;
    public float waveDuration = 0;
    public Vector3 particlesScale = new Vector3(0, 0, 0);

    

    void Start () {
        isAlive = true;
        startCounter = false;

        if (leftTower) {
            leftTowerScript = leftTower.GetComponent<ElectricTowerScript>();
        }
        if (rightTower) {
            rightTowerScript = rightTower.GetComponent<ElectricTowerScript>();
        }

        normalTower = this.transform.Find("Torre_Electrecidade").gameObject;
        destroyedTower = this.transform.Find("Torre_Electrecidade_Destroco").gameObject;
        destroyedTower.SetActive(false);
	}
	
	void Update () {

		if (startCounter) {
            timeToWait -= Time.deltaTime;
            if (timeToWait <= 0) {
                DestroyTower();
                startCounter = false;
            }
        }
	}

    public void callDestroyTower() {
        isAlive = false;
        GameObject instancia = Instantiate(wave, new Vector3(this.gameObject.transform.position.x, 0.70f, this.gameObject.transform.position.z), Quaternion.identity) as GameObject;

        waveScript = instancia.GetComponent<WaveImpactScript>();
        waveScript.waveDuration = (waveDuration != 0) ? waveDuration : waveScript.waveDuration;
        waveScript.waveMaxRadius = (waveMaxRadius != 0) ? waveMaxRadius : waveScript.waveMaxRadius;

        if (instancia.transform.Find("waveParticles")) {
            instancia.transform.Find("waveParticles").transform.localScale = (particlesScale != new Vector3(0, 0, 0)) ? particlesScale : instancia.transform.Find("waveParticles").transform.localScale;
        }
        normalTower.SetActive(false);
        destroyedTower.SetActive(true);
        startCounter = true;
    }

    private void DestroyTower() {
        ScoreImpact.scoreImpact.Score();
        if (leftTower) {
            if (leftTowerScript.isAlive) {
                leftTowerScript.callDestroyTower();
            }
        }

        if (rightTower)  {
            if (rightTowerScript.isAlive) {
                rightTowerScript.callDestroyTower();
            }
        }
    }
}
