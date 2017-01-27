using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClickedScript : MonoBehaviour {

    private Camera cam;
    private bool pawAttacking = false;
    private PlayerStatistics playerStats;
    private float smashCooldownTimer;

    [SerializeField]
    protected GameObject paw;

    // Attack configuration
    public float smashCooldown = 1.0f;
    public GameObject impactWaves;


    void Start() {
        cam = Camera.main;
        smashCooldownTimer = 0;
        playerStats = GetComponentInParent<PlayerStatistics>();
    }

    void Update() {
        if (smashCooldownTimer > 0) {
            smashCooldownTimer -= Time.deltaTime;
        }
        if (Input.GetButton("Fire1")) {
            if (smashCooldownTimer <= 0) {
                smashCooldownTimer = smashCooldown;
                if (playerStats.strikesAvailable > 0) {
                    PlayerClicked();
                }

            }
        }

        if (pawAttacking) {
            if (paw.transform.position.y >= 0.80f)
                paw.transform.position = Vector3.Lerp(paw.transform.position, new Vector3(paw.transform.position.x, 0.80f, paw.transform.position.z), 10f * Time.deltaTime);
                // paw.transform.Translate(-Vector3.up * 90 * Time.deltaTime);
            else
                pawAttacking = false;
        }        
    }

    // Call player attack
    public void PlayerClicked() {

        playerStats.UseStrike();

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit)) {

            Transform objectHit = hit.transform;
            //Debug.Log("Clicado" + objectHit.transform);
            paw.transform.position = new Vector3(hit.point.x, 30, hit.point.z);

            if (hit.collider.gameObject.CompareTag("Enemy")) {
                if (!objectHit.GetComponent<AudioSource>().isPlaying) {
                    objectHit.GetComponent<AudioSource>().Play();
                    print("Sound: " + objectHit.GetComponent<AudioSource>().clip);
                }
                return;
            }
            if (hit.collider.gameObject.tag == "Building" || hit.collider.gameObject.tag == "Floor"
                || hit.collider.gameObject.tag == "ElectricTower" || hit.collider.gameObject.tag == "Vehicle") {
                pawAttacking = true;
                Instantiate(impactWaves, new Vector3(hit.point.x, 0.70f, hit.point.z), Quaternion.identity);
                cam.GetComponent<CameraShake>().ShakeCamera();
                
                if (objectHit.GetComponent<AudioSource>()) {
                    int n = Random.Range(0, SoundStorage.soundStorage.crumbleArray.Length);
                    objectHit.GetComponent<AudioSource>().PlayOneShot(SoundStorage.soundStorage.crumbleArray[n]);
                }
            }
        }
    }
}
