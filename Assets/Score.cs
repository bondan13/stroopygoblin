using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Score : MonoBehaviour {

	public static long score;
	public static long highScore;
	public static long point;
	public static bool coin;
	private static float coinLeft = 3.0f;

	public static GameObject scoreText;
	public static GameObject scoreIntText;
	public static GameObject scoreSmallText;
	public static GameObject pointText;
	public static GameObject highScoreText;
	public static GameObject coinObject;
	public static GameObject quoteObject;

	private static GUIText scoreTextGui;
	private static GUIText scoreIntTextGui;
	private static GUIText scoreSmallTextGui;
	private static GUIText pointTextGui;
	private static GUIText highScoreTextGui;
	private static GUIText quoteTextGui;
	private static string[] quoteText;

	void Awake() {
		score = 0;
		highScore = Score.getHighScore();
		point = 1;
		Score.highScoreText = GameObject.Find("highScore");
		Score.scoreText = GameObject.Find("score");
		Score.scoreIntText = GameObject.Find("scoreBig");
		Score.scoreSmallText = GameObject.Find("scoreSmall");
		Score.pointText = GameObject.Find("point");
		Score.coinObject = GameObject.Find("coin");
		Score.quoteObject = GameObject.Find("quote");
		Score.highScoreTextGui = Score.highScoreText.GetComponent<GUIText> ();
		Score.scoreIntTextGui = Score.scoreIntText.GetComponent<GUIText> ();
		Score.scoreSmallTextGui = Score.scoreSmallText.GetComponent<GUIText> ();
		Score.pointTextGui = Score.pointText.GetComponent<GUIText> ();
		Score.quoteTextGui = Score.quoteObject.GetComponent<GUIText> ();
		Score.quoteText = new String[] {"When you give up, \nthat's when the game ends","","",
			"If you use your head, \nyou won't get fat even if you eat sweets.","","",
			"Actually, if you're smart, \nyou can eat sweets without gaining weight.","","",
			"Sometimes, the questions are complicated \nand the answers are simple.","","",
			"Remember, this just a game.","","",
			"You are human, are you not? \nYou are allowed to make mistakes.","","",
			"If you use your head, \nyou won't get fat even if you eat sweets.","","",
			"Actually, if you're smart, \nyou can eat sweets without gaining weight.","",""};
	}

	// Use this for initialization
	void Start () {
		Score.scoreIntTextGui.text = score.ToString();
		Score.highScoreTextGui.text = "HIGH SCORE: " + highScore.ToString("N0");
		Score.pointTextGui.text = "POINT: " + point.ToString("N0")+ "x";
		Score.scoreSmallTextGui.text = "SCORE: " + score.ToString("N0");
		Score.gameOver ();
		Score.coinObject.SetActive (false);
		Score.scoreIntText.SetActive (false);
		Score.scoreText.SetActive (false);
		Score.quoteObject.SetActive (false);
		Score.coin = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Score.coin == false) {
			Score.coinLeft -= Time.deltaTime;
			if (Score.coinLeft <= 0f) {
				Score.coin = true;
				Score.coinObject.SetActive (true);
			}
		}
	}

	public static void increaseScore(){
		Score.score = Score.score + Score.point;
		Score.scoreSmallTextGui.text = "SCORE :" +score.ToString("N0");
	}

	public static void increasePoint(){
		Score.coinLeft = 3f;
		Score.coin = false;
		Score.coinObject.SetActive (false);
		Score.point = Score.point * 10;	
		Score.pointTextGui.text = "POINT: " + point.ToString("N0") + "x";
	}

	public static void gameOver(){
		if (Score.score > Score.highScore) {
			Score.saveHighScore ();
		}
		Score.scoreText.SetActive (true);
		Score.scoreIntText.SetActive (true);
		Score.scoreIntTextGui.text = score.ToString("N0");
		Score.quoteTextGui.text = Score.quoteText[UnityEngine.Random.Range(0, 18)];
		Score.quoteObject.SetActive (true);
	}
	public static void gameStart(){
		Score.score = 0;
		Score.point = 1;
		Score.scoreText.SetActive (false);
		Score.scoreIntText.SetActive (false);
		Score.quoteObject.SetActive (false);
		Score.highScoreTextGui.text = "HIGH SCORE: " + highScore.ToString("N0");
		Score.pointTextGui.text = "POINT: " + point.ToString("N0") + "x";
		Score.scoreSmallTextGui.text = "SCORE: " + score.ToString("N0");
		Score.coinObject.SetActive (true);
		Score.coin = true;
	}

	public static void saveHighScore(){
		PlayerPrefs.SetString ("hs", Score.score.ToString());
		Score.highScore = Score.score;
	}

	public static void scoreActive(bool active){
		Score.scoreText.SetActive (active);
		Score.scoreIntText.SetActive (active);
	}

	private static int getHighScore(){
		if (PlayerPrefs.HasKey ("hs")) {
			return (int)Convert.ToInt64(PlayerPrefs.GetString ("hs"));
		} else {
			string hs = "10";
			PlayerPrefs.SetString ("hs", hs);
			return (int)Convert.ToInt64(hs);
		}
			
	}
}
