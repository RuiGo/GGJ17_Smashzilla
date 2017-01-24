using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Touch1 : MonoBehaviour {

    [SerializeField]
    private GameObject monsterPaw;

    [SerializeField]
    private GameObject building;

    [SerializeField]
    private AudioClip buildingCrumble;


    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

	}


    protected void OnTriggerEnter(Collider coll)
    {
        if (coll == monsterPaw.gameObject.GetComponent<Collider>())
        {
            building.GetComponent<AudioSource>().PlayOneShot(buildingCrumble);
        }
    }
}
