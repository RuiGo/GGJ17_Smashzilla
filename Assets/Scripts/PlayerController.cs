using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    public int strikesAvailable;

    [SerializeField]
    public int startingStrikes;


    [SerializeField]
    public Text strikesAvailableText;

    // Use this for initialization
    void Start () {
        strikesAvailable = startingStrikes;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void useStrike() {
        strikesAvailable--;
        Debug.Log("STRIKE USED--------REMAINING: " + strikesAvailable);
        strikesAvailableText.text = "x " + strikesAvailable;
    }

    public void reloadStrike()
    {
        strikesAvailable = 4;
    }
}
