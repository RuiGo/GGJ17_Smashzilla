﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatistics : MonoBehaviour {

    public static PlayerStatistics playerStatistics;

    public int strikesAvailable;
    public int startingStrikes;
    public Text strikesAvailableText;

    void Awake () {
        if (playerStatistics == null)
            playerStatistics = null;
        else
            Destroy(gameObject);
    }

    void Start () {
        strikesAvailable = startingStrikes;
        strikesAvailableText.text = "x " + strikesAvailable;
    }
	
	void Update () {}

    public void UseStrike() {
        strikesAvailable--;
        strikesAvailableText.text = "x " + strikesAvailable;
    }

    public void ReloadStrike() {
        strikesAvailable = 4;
    }
}
