using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{

	public static GameObject play;
	public static GameObject hint;
	public static GameObject hintDesc;
	public static int playGame;
	public static int hintGame;

	public static AudioSource sourceAudio;

	// Use this for initialization
	void Start ()
	{
		playGame = 0;
		hintGame = 0;
		Control.play = GameObject.Find ("play");
		Control.hint = GameObject.Find ("hint");
		Control.sourceAudio = GetComponent<AudioSource> ();
		Control.sourceAudio.Play ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//clickEvent ();
		tapEvent ();
	}

	void tapEvent ()
	{
		for (var i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch (i).phase == TouchPhase.Began) {
				RaycastHit2D hitInfo = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.GetTouch (i).position), Vector2.zero);
				// RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
				tap(hitInfo);
			}
		}
	}

//	void clickEvent(){
//		if (Input.GetMouseButtonDown (0)) {
//			Vector2 pos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
//			RaycastHit2D hitInfo = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (pos), Vector2.zero);
//			tap (hitInfo);
//		}
//	}

	public static void startGame ()
	{
		ads.hideAdss ();
		Control.play.SetActive (false);
		Control.hint.SetActive (false);
		Score.gameStart ();
		GoblinFlap.startFlap = false;
		GoblinFlap.animTransform = 1;
		BGLoop.running = true;
		yoyoManager.action = 2;
		controller.setQuestions (true);
		controller.createQuestions ();
		if (!sourceAudio.isPlaying) {
			sourceAudio.Play ();
		}
		Control.playGame += 1;
	}

	public static void gameOver ()
	{
		BGLoop.running = false;
		sourceAudio.Stop ();
		controller.setQuestions (false);
		GoblinFlap.animTransform = 2;
		yoyoManager.action = 1;
		Score.gameOver ();
		ads.bannerView.Show ();
	}

	void tap(RaycastHit2D hitInfo){
		if (hitInfo) {
			if (Control.hintGame == 0) {
				if (hitInfo.collider.name == "button_a1" || hitInfo.collider.name == "button_a2") {     

					GoblinFlap.startFlap = true;
					if (hitInfo.collider.name == controller.correct_answer) {
						GoblinFlap.didFlap = true;
						controller.setQuestions (true);
						controller.createQuestions ();
						Score.increaseScore ();
					} else {
						Control.gameOver ();
					}

				}

				if (hitInfo.collider.name == "play") {
					Control.startGame ();
				}

				if (hitInfo.collider.name == "hint") {
					Control.hintGame = 1;
					controller.hintObject.SetActive (true);
					Control.startGame ();
				}

				if (hitInfo.collider.name == "hintDesc") {
					Control.hintDesc.SetActive (false);
					Control.play.SetActive (true);
					Control.hint.SetActive (true);
					if (Control.playGame > 0) {
						Score.scoreActive (true);
					}
				}
			}
			// Here you can check hitInfo to see which collider has been hit, and act appropriately.
		}
		if (Control.hintGame == 1) {
			controller.createHintQuestion (1);
			Control.hintGame = 3;
		} else if (Control.hintGame == 3) {
			controller.createHintQuestion (3);
			Control.hintGame = 4;
		} else if (Control.hintGame == 4) {
			if (hitInfo && hitInfo.collider.name == "button_a1") {
				controller.createHintQuestion (4);
				Control.hintGame = 6;
				GoblinFlap.didFlap = true;
				GoblinFlap.startFlap = true;
				GoblinFlap.hint = true;
			}
		} else if (Control.hintGame == 6) {
			controller.createHintQuestion (6);
			Control.hintGame = 7;
		} else if (Control.hintGame == 7) {
			if (hitInfo && hitInfo.collider.name == "button_a2") {
				controller.createHintQuestion (7);
				Control.hintGame = 8;
				GoblinFlap.didFlap = true;
			}
		} else if (Control.hintGame == 8) {
			controller.createHintQuestion (8);
			Control.play.SetActive (true);
			Control.hint.SetActive (true);
			Control.hintGame = 0;
			GoblinFlap.startFlap = false;
			GoblinFlap.hint = false;
			GoblinFlap.animTransform = 1;
		}
	}
}
