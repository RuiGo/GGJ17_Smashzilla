using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour {

    public static PathManager pathManager = null;
    public Transform[] availableNodes;
    
    void Awake()
    {
        if (pathManager == null)
            pathManager = this;
        else
            Destroy(this.gameObject);
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
