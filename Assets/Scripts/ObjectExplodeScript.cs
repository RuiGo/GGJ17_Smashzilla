using UnityEngine;

public class ObjectExplodeScript : MonoBehaviour {

    private WaveImpactScript waveScript;
    private ParticleSystem waveParticles;
    
    public GameObject wave;
    public float waveMaxRadius = 0;
    public float waveDuration = 0;
    public int hitForce = 0;
    public Vector3 particlesScale = new Vector3(0,0,0);

    
    

    public void ExplodeObject() {
        GameObject instancia = Instantiate(wave, new Vector3(this.gameObject.transform.position.x, 0.70f, this.gameObject.transform.position.z), Quaternion.identity) as GameObject;

        waveScript = instancia.GetComponent<WaveImpactScript>();
        waveScript.waveDuration = (waveDuration != 0) ? waveDuration : waveScript.waveDuration;
        waveScript.waveMaxRadius = (waveMaxRadius != 0) ? waveMaxRadius : waveScript.waveMaxRadius;

        if (instancia.transform.Find("waveParticles")) {
        instancia.transform.Find("waveParticles").transform.localScale = (particlesScale != new Vector3(0, 0, 0)) ? particlesScale : instancia.transform.Find("waveParticles").transform.localScale;
        }
        Destroy(gameObject);
    }

    /*private void OnDestroy() {
        GameObject instancia = Instantiate(wave, new Vector3(this.gameObject.transform.position.x, 0.70f, this.gameObject.transform.position.z), Quaternion.identity) as GameObject;

        waveScript = instancia.GetComponent<WaveImpactScript>();
        waveScript.waveDuration = (waveDuration != 0) ? waveDuration : waveScript.waveDuration;
        waveScript.waveMaxRadius = (waveMaxRadius != 0) ? waveMaxRadius : waveScript.waveMaxRadius;

        if (instancia.transform.Find("waveParticles")) {
            instancia.transform.Find("waveParticles").transform.localScale = (particlesScale != new Vector3(0, 0, 0)) ? particlesScale : instancia.transform.Find("waveParticles").transform.localScale;
        }
    }*/
}
