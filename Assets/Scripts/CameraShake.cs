using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
    [SerializeField]
    protected Camera cam;
	
	// How long the object should shake for.
	public float shakeDuration = 0f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;
    private bool isShaking;
	
	Vector3 originalPos;

	void Update()
	{
        if (isShaking)
        {
            if (shakeDuration > 0)
            {
                cam.transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shakeDuration = 0f;
                cam.transform.localPosition = originalPos;
                isShaking = false;
            }
        }
	}

    public void ShakeCamera()
    {
        isShaking = true;
        originalPos = cam.transform.localPosition;
    }
}
