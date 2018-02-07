using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
	
	public static GameObject hintObject;
	public static GameObject hintObject1;
	public static GameObject tapObject;
	public static GameObject hintObject2;
	public static GUIText hintText;
	public static GUIText hintText2;

	private static List<Sprite> __button_sprite = new List<Sprite> ();
	private static Dictionary<string, Sprite> __button_sprite_d = new Dictionary<string, Sprite> ();

	private static Vector3 __button_a_size = new Vector3 (0.6f, 0.6f, 0f);
	private static Vector3 __button_q_size = new Vector3 (0.7f, 0.7f, 0f);
	private static Vector3 __button_bg_size = new Vector3 (1f, 1f, 0f);
	private static Vector3 __button_q_pos = new Vector3 (0f, -1f, -2f);
	private static Vector3 __button_bg_pos = new Vector3 (0f, -1f, -1f);
	private static Vector3 __button_left_pos = new Vector3 (-0.5f, -4f, -1f);
	private static Vector3 __button_right_pos = new Vector3 (0.5f, -4f, -1f);
	private static string[] __button_id = {
		"blue-circle",
		"blue-square",
		"blue-triangle",
		"green-circle",
		"green-square",
		"green-triangle",
		"red-circle",
		"red-square",
		"red-triangle",
		"yellow-circle",
		"yellow-square",
		"yellow-triangle",
		"black-circle",
		"white-circle"
	};

	private static string[] __button_color = { "blue", "green", "red", "yellow" };
	private static string[] __button_pattern = { "circle", "square", "triangle" };
	private static string[] __bg_color = { "white-circle", "black-circle" };

	private static int[] __question = { 0, 0 };
	private static int __bg_question;
	private static string __answer;

	private static Vector2 __button_collider = new Vector2 (1.5f, 1.5f);
	
	public static GameObject button_a1;
	public static GameObject button_a2;
	public static GameObject button_q;
	public static GameObject button_bg;

	public static string correct_answer;
	
	private static int __button_a1_id;
	private static int __button_a2_id;
	private static int __button_bg_id;
	private static int __button_q_id;
	
	// Use this for initialization

	void Awake ()
	{
		awakeQuestions ();
		foreach (string i in controller.__button_id) {
			Sprite s = (Sprite)Resources.Load ("button/" + i, typeof(Sprite));
			controller.__button_sprite.Add (s);
			controller.__button_sprite_d.Add (i, s);
		}
		controller.hintObject = GameObject.Find ("hintText");
		controller.hintObject1 = GameObject.Find ("hintText1");
		controller.hintObject2 = GameObject.Find ("hintText2");
		controller.tapObject = GameObject.Find ("tap");
		controller.tapObject.SetActive(false);
		controller.hintObject1.SetActive(false);
		controller.hintObject2.SetActive(false);
		controller.hintText = controller.hintObject.GetComponent<GUIText> ();
		controller.hintText2 = controller.hintObject1.GetComponent<GUIText> ();
	}

	void Start ()
	{
		
		//createQuestions ();
		//controller.createQuestions();
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	int RandomArr (int[] arr_id, int? except)
	{
		int length = arr_id.Length - 1;
		int result = Random.Range (0, length);
		do {
			result = Random.Range (0, length);
		} while (except.HasValue && (result == except));
		return result;
	}

	static int RandomRange (int start, int stop, int? except)
	{
		int result;
		do {
			result = Random.Range (start, stop);
		} while (except.HasValue && (result == except));
		return result;
	}

	public static void createQuestions ()
	{
		controller.__question [0] = controller.RandomRange (0, 4, controller.__question [0]);
		controller.__question [1] = controller.RandomRange (0, 3, controller.__question [1]);
		controller.__bg_question = controller.RandomRange (0, 2, null);
		int answer_pos = controller.RandomRange (0, 2, null);

		string answer_1 = controller.__button_color [controller.RandomRange (0, 4, controller.__question [0])] + "-" + controller.__button_pattern [controller.__question [1]];
		string answer_2 = controller.__button_color [controller.__question [0]] + "-" + controller.__button_pattern [controller.RandomRange (0, 3, controller.__question [1])];
		Sprite answer_true;
		Sprite answer_false;

		if (controller.__bg_question == 1) {
			answer_true = controller.__button_sprite_d [answer_1];
			answer_false = controller.__button_sprite_d [answer_2];
		} else {
			answer_true = controller.__button_sprite_d [answer_2];
			answer_false = controller.__button_sprite_d [answer_1];
		}

		if (answer_pos == 1) {
			controller.button_a1.GetComponent<SpriteRenderer> ().sprite = answer_false;
			controller.button_a2.GetComponent<SpriteRenderer> ().sprite = answer_true;
			controller.correct_answer = "button_a2";
		} else {
			controller.button_a1.GetComponent<SpriteRenderer> ().sprite = answer_true;
			controller.button_a2.GetComponent<SpriteRenderer> ().sprite = answer_false;
			controller.correct_answer = "button_a1";
		}

		controller.button_q.GetComponent<SpriteRenderer> ().sprite = controller.__button_sprite_d [controller.__button_color [controller.__question [0]] + "-" + controller.__button_pattern [controller.__question [1]]];
		controller.button_bg.GetComponent<SpriteRenderer> ().sprite = controller.__button_sprite_d [controller.__bg_color [controller.__bg_question]];
	}

	void awakeQuestions ()
	{
		controller.button_a1 = new GameObject ("button_a1");
		controller.button_a1.AddComponent<SpriteRenderer> ();
		controller.button_a1.AddComponent<BoxCollider2D> ();
		BoxCollider2D cola1 = controller.button_a1.GetComponent<BoxCollider2D> ();
		cola1.size = controller.__button_collider;
		cola1.enabled = false;
		controller.button_a1.transform.position = controller.__button_left_pos;
		controller.button_a1.transform.localScale = controller.__button_a_size;

		controller.button_a2 = new GameObject ("button_a2");
		controller.button_a2.AddComponent<SpriteRenderer> ();
		controller.button_a2.AddComponent<BoxCollider2D> ();
		BoxCollider2D col = controller.button_a2.GetComponent<BoxCollider2D> ();
		col.size = controller.__button_collider;
		col.enabled = false;
		controller.button_a2.transform.position = controller.__button_right_pos;
		controller.button_a2.transform.localScale = controller.__button_a_size;

		controller.button_q = new GameObject ("button_q");
		controller.button_q.AddComponent<SpriteRenderer> ();
		controller.button_q.transform.position = controller.__button_q_pos;
		controller.button_q.transform.localScale = controller.__button_q_size;

		controller.button_bg = new GameObject ("button_bg");
		controller.button_bg.AddComponent<SpriteRenderer> ();
		controller.button_bg.transform.position = controller.__button_bg_pos;
		controller.button_bg.transform.localScale = controller.__button_bg_size;
	}

	public static void setQuestions (bool active)
	{
		controller.button_a1.SetActive (active);
		controller.button_a1.GetComponent<BoxCollider2D> ().enabled = active;
		controller.button_a2.SetActive (active);
		controller.button_a2.GetComponent<BoxCollider2D> ().enabled = active;
		controller.button_q.SetActive (active);
		controller.button_bg.SetActive (active);
	}

	public static void createHintQuestion (int step)
	{
		if (step == 1) {
			string steptest = "Question showing up\nred triangle button\nwith background\nWHITE";
			controller.hintText.text = steptest;
			controller.hintObject.transform.position = new Vector3 (0.5f, 0.58f, 0f);
			controller.hintObject.transform.localScale = new Vector3 (0.15f, 0.15f, 0f);
			controller.button_q.GetComponent<SpriteRenderer> ().sprite = controller.__button_sprite_d ["red-triangle"];
			controller.button_a1.GetComponent<SpriteRenderer> ().sprite = null;
			controller.button_a2.GetComponent<SpriteRenderer> ().sprite = null;
			controller.button_bg.GetComponent<SpriteRenderer> ().sprite = controller.__button_sprite_d ["white-circle"];
		} else if (step == 3) {
			string steptest = "lets tap button \naccording the\nCOLOR ";
			controller.hintText2.text = steptest;
			controller.hintObject1.transform.position = new Vector3 (0.19f, 0.1f, 0f);
			controller.hintObject1.transform.localScale = new Vector3 (0.1f, 0.1f, 0f);
			controller.button_a1.GetComponent<SpriteRenderer> ().sprite = controller.__button_sprite_d ["red-circle"];
			controller.button_a2.GetComponent<SpriteRenderer> ().sprite = controller.__button_sprite_d ["green-triangle"];
			controller.hintObject1.SetActive(true);
			controller.tapObject.SetActive (true);
			controller.tapObject.transform.position = new Vector3 (-0.75f, -4.25f, -5f);
			controller.tapObject.transform.localScale = new Vector3 (-0.6f, 0.6f, 0f);
		} else if (step == 4) {
			controller.hintObject1.SetActive(false);
			string steptest = "Question showing up\nred triangle button\nwith background\nBLACK";
			controller.hintText.text = steptest;
			controller.hintObject.transform.position = new Vector3 (0.5f, 0.58f, 0f);
			controller.hintObject.transform.localScale = new Vector3 (0.15f, 0.15f, 0f);
			controller.button_q.GetComponent<SpriteRenderer> ().sprite = controller.__button_sprite_d ["red-triangle"];
			controller.button_a1.GetComponent<SpriteRenderer> ().sprite = null;
			controller.button_a2.GetComponent<SpriteRenderer> ().sprite = null;
			controller.button_bg.GetComponent<SpriteRenderer> ().sprite = controller.__button_sprite_d ["black-circle"];
			controller.tapObject.SetActive (false);
		} else if (step == 6) {
			controller.hintObject1.SetActive(true);
			string steptest = "lets tap button\naccording the\nSHAPE ";
			controller.hintText2.text = steptest;
			controller.hintObject1.transform.position = new Vector3 (0.82f, 0.1f, 0f);
			controller.hintObject1.transform.localScale = new Vector3 (0.1f, 0.1f, 0f);
			controller.button_a1.GetComponent<SpriteRenderer> ().sprite = controller.__button_sprite_d ["red-circle"];
			controller.button_a2.GetComponent<SpriteRenderer> ().sprite = controller.__button_sprite_d ["green-triangle"];
			controller.tapObject.SetActive (true);
			controller.tapObject.transform.position = new Vector3 (0.75f, -4.25f, -5f);
			controller.tapObject.transform.localScale = new Vector3 (0.6f, 0.6f, 0f);
		} else if (step == 7) {
			controller.hintObject1.SetActive(false);
			controller.hintObject2.SetActive (true);
			string steptest = "each correct answer will increasing score \nbased on point, get multiple point 10x \nby collecting coin";
			controller.hintText.text = steptest;
			controller.hintObject.transform.position = new Vector3 (0.5f, 0.6f, 0f);
			controller.hintObject.transform.localScale = new Vector3 (0.1f, 0.07f, 0f);
			controller.hintObject2.transform.position = new Vector3 (0.5f, 0.7f, 0f);
			controller.hintObject2.transform.localScale = new Vector3 (0.2f, 0.15f, 0f);
			controller.setQuestions (false);
			controller.tapObject.SetActive (false);
		} else if (step == 8) {
			controller.hintObject2.SetActive (false);
			controller.hintObject.SetActive (false);
		}
	}
}
