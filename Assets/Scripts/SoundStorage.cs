using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundStorage : MonoBehaviour {

    public static SoundStorage soundStorage;
    public AudioClip[] crumbleArray;
    public AudioClip monsterRoar;
    public AudioClip monsterScreech;
    public AudioClip explosion;
    public AudioMixer mainAudioMixer;

    void Awake () {
        if (soundStorage == null)
            soundStorage = this;
        else
            Destroy(this);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
