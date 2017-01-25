using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveImpactScript : MonoBehaviour {

    private GameObject thisWave;
    private SphereCollider waveSphereCollider;
    private float currentRadiusHolder;
    private float increaseRadius = 0.0f;

    // Attack
    public float waveMaxRadius = 10.0f;
    public float waveMinRadius = 0.0f;
    public float waveDuration = 2.0f; 

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
        if (other.tag == "Building" || other.tag == "Vehicle") {
            if (other.gameObject.GetComponent<DestruicaoEdificios>())
                other.gameObject.GetComponent<DestruicaoEdificios>().Destruir();
            else 
                print("Missing <DestruicaoEdificios> component!");

        } else if (other.gameObject.tag == "ElectricTower") {
            if (other.gameObject.GetComponent<ElectricTowerScript>().isAlive) {
                other.gameObject.GetComponent<ElectricTowerScript>().callDestroyTower();
            }
        }
    } 

   
}
