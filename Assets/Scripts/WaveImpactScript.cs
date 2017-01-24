using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveImpactScript : MonoBehaviour {

    // Attack
    public int hitForce = 100;

    public float waveMaxRadius = 10.0f;
    public float waveMinRadius = 0.0f;
    public float waveDuration = 2.0f; 

    private GameObject thisWave;
    private SphereCollider waveSphereCollider;
    private float currentRadiusHolder;
    private float increaseRadius = 0.0f;
    private float t = 0.0f;
    private EnemyHealthScript enemyHealth;

    void Start () {
        increaseRadius = (waveMaxRadius - waveMinRadius) / waveDuration;
        currentRadiusHolder = waveMinRadius;
        waveSphereCollider = this.transform.GetComponent<SphereCollider>();
    }
	
	void Update () {
        waveDuration -= Time.deltaTime;
        if (waveDuration < 0) {
            Destroy(this.gameObject);
        }
        currentRadiusHolder += increaseRadius * Time.deltaTime;
        waveSphereCollider.radius = currentRadiusHolder;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Building") {

            ScoreImpact.scoreImpact.Score(); //Chama a função do Score cada vez que colide com um Mesh
            //other.gameObject.GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0, 1);
            if (other.gameObject.GetComponent<DestruicaoEdificios>())
                other.gameObject.GetComponent<DestruicaoEdificios>().Destruir();
            //other.gameObject.GetComponent<MeshRenderer>().enabled = false;

            // enemyHealth = other.GetComponent<EnemyHealthScript>();
            // enemyHealth.hp -= hitForce;
        } else if (other.gameObject.tag == "ElectricTower")
        {
            if (other.gameObject.GetComponent<ElectricTowerScript>().isAlive)
            {
                other.gameObject.GetComponent<ElectricTowerScript>().callDestroyTower();
            }
        }
    } 

   
}
