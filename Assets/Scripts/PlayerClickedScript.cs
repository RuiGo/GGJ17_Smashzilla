using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClickedScript : MonoBehaviour {

    private Camera cam;
    public bool pawAttacking = false; //temp public
    public bool hitNoZone = false;  //temp public
    public bool pawGoingUp = false; //temp public
    private PlayerStatistics playerStats;
    private float smashCooldownTimer;
    private Vector3 pawStartFallingPosition;

    [SerializeField]
    protected Transform paw;

    // Attack configuration
    public float smashCooldown = 1.5f;
    public float pawFallingSpeed = 5f;
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
            float fallLimit = hitNoZone ? 3f : 0.8f;
            pawFallingSpeed = hitNoZone ? 3f : 7.5f;
            Vector3 fallPosition = new Vector3(paw.position.x, fallLimit, paw.position.z);
            Vector3 fallDirection = pawStartFallingPosition - fallPosition;
            if (!pawGoingUp) {                
                if (paw.position.y > fallLimit) {
                    Vector3 fallVector = fallDirection * pawFallingSpeed * Time.deltaTime;
                    paw.position -= fallVector;
                } else {
                    paw.position = fallPosition;
                    if (hitNoZone) 
                        pawGoingUp = true;                        
                    else
                        pawAttacking = false;
                }
            } else {
                if (paw.position.y < pawStartFallingPosition.y) {
                    Vector3 riseVector = fallDirection * (pawFallingSpeed / 5f) * Time.deltaTime;
                    paw.position += riseVector;
                } else {
                    paw.position = pawStartFallingPosition;
                    pawGoingUp = false;
                    hitNoZone = false;
                    pawAttacking = false;
                }
            }
        }        
    }

    // Call player attack
    public void PlayerClicked() {
        pawAttacking = false;
        hitNoZone = false;
        pawGoingUp = false;

        playerStats.UseStrike();

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit)) {

            Transform objectHit = hit.transform;
            //Debug.Log("Clicado" + objectHit.gameObject.name);
            pawStartFallingPosition = new Vector3(hit.point.x, 30, hit.point.z);
            paw.position = pawStartFallingPosition;

            if (hit.collider.gameObject.CompareTag("Enemy")) {
                if (objectHit.GetComponent<AudioSource>()) {
                    if (objectHit.GetComponent<AudioSource>().isPlaying)
                        objectHit.GetComponent<AudioSource>().Stop();
                    objectHit.GetComponent<AudioSource>().Play();
                }
                pawAttacking = true;
                hitNoZone = true;
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
