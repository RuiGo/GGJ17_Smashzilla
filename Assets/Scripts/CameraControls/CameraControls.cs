using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour {
    private Camera cam;
    private Vector2 initClickPosition;

    [SerializeField]
    protected float minZoomDistance;
    [SerializeField]
    protected float maxZoomDistance;
    [SerializeField]
    protected float xMinCameraBorder;
    [SerializeField]
    protected float xMaxCameraBorder;
    [SerializeField]
    protected float yMinCameraBorder;
    [SerializeField]
    protected float yMaxCameraBorder;
    [SerializeField]
    protected float zoomSpeed = 10f;
    [SerializeField]
    protected float dragSpeed = 0.07f;

    void Start () {
        cam = GetComponent<Camera>();
	}
	
	void Update () {
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && cam.orthographicSize < maxZoomDistance)
            ZoomOut();
        else if (Input.GetAxis("Mouse ScrollWheel") > 0 && cam.orthographicSize > minZoomDistance)
            ZoomIn();
        
        //TODO: por o movimento de acordo com os eixos da camara
        if (Input.GetKey(KeyCode.LeftArrow)) {
            Vector3 newPosition = cam.transform.localPosition;
            newPosition -= new Vector3(0.5f, 0, 0);
            print(newPosition);
            if (newPosition.x >= xMinCameraBorder)
                cam.transform.localPosition = newPosition;
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            Vector3 newPosition = cam.transform.localPosition;
            newPosition += new Vector3(0.5f, 0, 0);
            print(newPosition);
            if (newPosition.x <= xMaxCameraBorder)
                cam.transform.localPosition = newPosition;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            Vector3 newPosition = cam.transform.position;
            newPosition -= new Vector3(0, 0.5f, 0);
            print(newPosition);
            if (newPosition.y >= yMinCameraBorder)
                cam.transform.localPosition = newPosition;
        } else if (Input.GetKey(KeyCode.UpArrow)) {
            Vector3 newPosition = cam.transform.localPosition;
            newPosition += new Vector3(0, 0.5f, 0);
            print(newPosition);
            if (newPosition.y <= yMaxCameraBorder)
                cam.transform.localPosition = newPosition;
        }

        if (Input.GetButtonDown("Fire2")) {
            initClickPosition = Input.mousePosition;
        }

        if (Input.GetButton("Fire2")) {
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 movementDirection = mousePosition - initClickPosition;
            Vector3 tempVector = new Vector3(movementDirection.x, movementDirection.y, 0);
            Vector3 newPosition = cam.transform.localPosition;
            newPosition += -(tempVector.normalized * dragSpeed * Time.deltaTime);
            if (newPosition.x >= xMinCameraBorder && newPosition.x <= xMaxCameraBorder
                && newPosition.y >= yMinCameraBorder && newPosition.y <= yMaxCameraBorder) {
                cam.transform.localPosition = newPosition;
            }
        }
    }

    void ZoomIn()  {
        cam.orthographicSize -= zoomSpeed * Time.deltaTime;
        if (cam.orthographicSize < minZoomDistance)
            cam.orthographicSize = minZoomDistance;
    }

    void ZoomOut() {
        cam.orthographicSize += zoomSpeed * Time.deltaTime;
        if (cam.orthographicSize > maxZoomDistance)
            cam.orthographicSize = maxZoomDistance;
    }
}
