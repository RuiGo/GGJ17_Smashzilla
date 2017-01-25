using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour {
    ////--------------------------------------------DELETE THIS SCRIPT
    public int hp = 100;
	
	void Update () {
        if (hp <= 0) {
            Destroy(this.gameObject);
        }
	}
}
