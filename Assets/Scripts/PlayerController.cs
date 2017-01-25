using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    
    public int strikesAvailable;
    public int startingStrikes;
    public Text strikesAvailableText;

    void Start () {
        strikesAvailable = startingStrikes;
    }
	
	void Update () {}

    public void useStrike() {
        strikesAvailable--;
        strikesAvailableText.text = "x " + strikesAvailable;
    }

    public void reloadStrike() {
        strikesAvailable = 4;
    }
}
