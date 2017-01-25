using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClickedScript : MonoBehaviour {

    private Camera cam;
    private Transform objectHit;
    private bool pawAttacking = false;
    private PlayerController playerController;
    private float smashCooldownTimer;

    [SerializeField]
    protected AudioClip[] crumble;
    [SerializeField]
    protected GameObject paw;

    


    // Attack configuration
    public float smashCooldown = 1.0f;
    public GameObject impactWaves;


    void Start() {
        cam = Camera.main;
        smashCooldownTimer = 0;
        playerController = GetComponentInParent<PlayerController>();
    }

    void Update() {
        // Shoot Timer
        if (smashCooldownTimer > 0) {
            smashCooldownTimer -= Time.deltaTime;
        }

        // Get player Right Click input
        if (Input.GetButton("Fire1")) {
            if (smashCooldownTimer <= 0) {
                smashCooldownTimer = smashCooldown;
                objectHit = null;

                Debug.Log("Strikes:    " + playerController.strikesAvailable);
                if (playerController.strikesAvailable > 0) {
                    playerClicked();
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
    public void playerClicked() {

        playerController.useStrike();

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit)) {

            objectHit = hit.transform;
            Debug.Log("Clicado" + objectHit.transform);
            paw.transform.position = new Vector3(hit.point.x, 30, hit.point.z);

            if (hit.collider.gameObject.tag == "Enemy") {
                StartCoroutine(tiltBuild(hit.collider.gameObject));
                objectHit.GetComponent<AudioSource>().Play();
                return;
            }

            if (hit.collider.gameObject.tag == "Building" || hit.collider.gameObject.tag == "Floor"
                || hit.collider.gameObject.tag == "ElectricTower") {
                objectHit.GetComponent<AudioSource>().PlayOneShot(crumble[Random.Range(0, crumble.Length)]);
                pawAttacking = true;
                Instantiate(impactWaves, new Vector3(hit.point.x, 0.70f, hit.point.z), Quaternion.identity);
                cam.GetComponent<CameraShake>().shakeDuration = 1;
                cam.GetComponent<CameraShake>().shakeAmount = 0.3f;
                cam.GetComponent<CameraShake>().ShakeCamera();
            }
        }
    }

    public IEnumerator tiltBuild(GameObject build) {
        Debug.Log("entrou na rotina");
        Color originalColor = build.GetComponent<MeshRenderer>().material.color;
        build.GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0, 1);
        yield return new WaitForSeconds(0.10f);
        Debug.Log(".25s depois vai alterar para COR ORIGINAL");
        build.GetComponent<MeshRenderer>().material.color = originalColor;
        yield return new WaitForSeconds(0.10f);
        Debug.Log(".25 depois vai alterar para VERMELHO");
        build.GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0, 1);
        yield return new WaitForSeconds(0.10f);
        Debug.Log(".25 depois vai alterar para COR ORIGINAL");
        build.GetComponent<MeshRenderer>().material.color = originalColor;
    }
}
