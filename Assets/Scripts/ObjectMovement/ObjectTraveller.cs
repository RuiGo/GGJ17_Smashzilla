using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTraveller : MonoBehaviour {

    private bool isMoving = false;
    public bool isSummonable = false;
    public float movementSpeed = 1f;
    public int[] path;
    public Transform currentPath;
    int currentPathIndex;
    public Transform summonPosition;
    float nodeInteractionCooldown = 2f;
    float nodeInteractionTimer;


    // Use this for initialization
    void Start () {
        nodeInteractionTimer = 0;
        currentPathIndex = 0;
        currentPath = PathManager.pathManager.availableNodes[path[currentPathIndex]];
        print("Starting path index: " + path[currentPathIndex]);
    }
	
	// Update is called once per frame
	void Update () {
        if(nodeInteractionTimer > 0) {
            nodeInteractionTimer -= Time.deltaTime;
        }
        if (path.Length > 0 && currentPath != null)
        {
            if (!isMoving)
                isMoving = true;

            MoveToPathNode();
        }
	}

    void OnTriggerEnter(Collider collider)
    {
        //if object of type node request new path
        if(collider.gameObject.CompareTag("Node") && nodeInteractionTimer <= 0) {
            nodeInteractionTimer = nodeInteractionCooldown;
            RequestNewPath();
        }
        else
        {
            //print("invalid collider or current path is null");
        }
    }

    void MoveToPathNode()
    {
        Vector3 movementDirection = currentPath.position - transform.position;
        transform.position += movementDirection.normalized * movementSpeed * Time.deltaTime;
        transform.forward = movementDirection.normalized;
    }
    //usar o smooth turning do AI

    void StopMovement()
    {
        isMoving = false;
    }

    void RequestNewPath()
    {
        currentPathIndex++;
        if (currentPathIndex >= path.Length)
        {
            if (isSummonable && summonPosition != null)
            {
                transform.position = summonPosition.position;
            }
            currentPathIndex = 0;
            //print("Reset path");
        }
        currentPath = PathManager.pathManager.availableNodes[path[currentPathIndex]];
    }
}
