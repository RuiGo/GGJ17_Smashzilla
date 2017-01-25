using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    private Camera cam;
	private bool isShaking;
    private float shakeTimer = 0;
	private Vector3 originalPos;

	// How long the object should shake for.
	public float shakeDuration = 0.5f;
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;

    void Start() {
        cam = GetComponent<Camera>();
    }

	void Update() {
        if (isShaking) {
            if (shakeTimer > 0) {
                cam.transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
                shakeTimer -= Time.deltaTime * decreaseFactor;
            } else {
                shakeTimer = 0f;
                cam.transform.localPosition = originalPos;
                isShaking = false;
            }
        }
	}

    public void ShakeCamera() {
        isShaking = true;
        shakeTimer = shakeDuration;
        originalPos = cam.transform.localPosition;
    }
}
