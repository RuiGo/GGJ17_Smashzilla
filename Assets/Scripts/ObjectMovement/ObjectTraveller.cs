using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTraveller : MonoBehaviour {

    private int currentPathIndex;
    private float distanceToNode;
    private float maxDistanceToNode = 0.1f;

    [SerializeField]
    protected bool isSummonable = false;
    [SerializeField]
    protected float movementSpeed = 1f;
    [SerializeField]
    protected Transform[] path;
    [SerializeField]
    protected Transform currentPath;
    [SerializeField]
    protected Transform summonPosition;

    public bool isMoving = true;

    void Start () {
        currentPathIndex = 0;
        currentPath = path[currentPathIndex];
        distanceToNode = Vector3.Distance(transform.position, currentPath.position);
    }
	
	void Update () {
        if (isMoving) {
            if (path.Length > 0 && currentPath != null) {
                distanceToNode = Vector3.Distance(transform.position, currentPath.position);
                if (distanceToNode > maxDistanceToNode) {
                    MoveToPathNode();
                } else {
                    RequestNewPath();
                }
            }
        }
	}

    void MoveToPathNode() {
        Vector3 movementDirection = currentPath.position - transform.position;
        transform.position += movementDirection.normalized * movementSpeed * Time.deltaTime;
        transform.forward = movementDirection.normalized;
    }

    void RequestNewPath() {
        currentPathIndex++;
        if (currentPathIndex >= path.Length) {
            if (isSummonable && summonPosition != null) {
                transform.position = summonPosition.position;
            }
            currentPathIndex = 0;
        }
        currentPath = path[currentPathIndex];
    }
}
