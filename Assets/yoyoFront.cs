using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yoyoFront : MonoBehaviour {

	public AudioSource goblinAudio;
	public AudioClip yoyowalk;

	// Use this for initialization
	void Start () {
		goblinAudio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PrintEvent(int i) {
		goblinAudio.PlayOneShot(yoyowalk);
	}
}
