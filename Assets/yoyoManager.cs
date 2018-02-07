using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yoyoManager : MonoBehaviour {

	private static GameObject __yoyoFront;
	private static GameObject __yoyoMiddle;
	private static GameObject __yoyoBack;

	private static Animator __yoyoFrontAnim;
	private static Animator __yoyoMiddleAnim;
	private static Animator __yoyoBackAnim;

	public static int action;

	// Use this for initialization

	void Awake(){
		yoyoManager.action = 0;
		yoyoManager.__yoyoFront = GameObject.Find("yoyoFront");
		yoyoManager.__yoyoMiddle = GameObject.Find("yoyoMiddle");
		yoyoManager.__yoyoBack = GameObject.Find("yoyoBack");

		yoyoManager.__yoyoFrontAnim = yoyoManager.__yoyoFront.transform.GetComponentInChildren<Animator>();
		yoyoManager.__yoyoMiddleAnim = yoyoManager.__yoyoMiddle.transform.GetComponentInChildren<Animator>();
		yoyoManager.__yoyoBackAnim = yoyoManager.__yoyoBack.transform.GetComponentInChildren<Animator>();
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (yoyoManager.action == 1 || yoyoManager.action == 2 || yoyoManager.action == 3) {

			yoyoManager.__yoyoFrontAnim.SetInteger ("yoyoAction", yoyoManager.action);
			yoyoManager.__yoyoMiddleAnim.SetInteger ("yoyoAction", yoyoManager.action);
			yoyoManager.__yoyoBackAnim.SetInteger ("yoyoAction", yoyoManager.action);
			yoyoManager.action = 0;
		}
		
	}
}
