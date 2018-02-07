using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinFlap : MonoBehaviour
{
	
	public Vector3 gravity;
	public Vector3 velocity;
	public Vector3 flapVelocity;
	public float maxSpeed;
	public float gravitySpeed;

	private float maxSpeedStart;
	private Vector3 flapVelocityStart;
	private Vector3 gravityStart;

	public static bool startFlap;
	public static bool didFlap;
	public static bool hint;
	public static int animTransform;

	public AudioSource goblinAudio;
	public AudioClip taptap;
	public AudioClip crash;
	public AudioClip pointIncrease;
	public AudioClip laugh;

	Animator rotarAnimator;

	void Awake ()
	{
		this.maxSpeedStart = 5f;
		this.gravitySpeed = 0f;
		this.maxSpeed = this.maxSpeedStart;

		this.velocity = Vector3.zero;

		this.gravityStart = new Vector3 (0f, -10f, 0f);
		this.gravity = this.gravityStart;
		this.flapVelocityStart = new Vector3 (0f, 20f, 0f);
		this.flapVelocity = this.flapVelocityStart;

		GoblinFlap.didFlap = false;
		GoblinFlap.startFlap = false;
		GoblinFlap.hint = false;
		GoblinFlap.animTransform = 1;

		this.rotarAnimator = transform.GetComponentInChildren<Animator> ();
		goblinAudio = GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GoblinFlap.animTransform == 1) {
			transform.position = new Vector3 (1.3f, -2f, -2f);
			this.rotarAnimator.SetInteger ("doAnim", GoblinFlap.animTransform);
			transform.rotation = Quaternion.Euler (0, 0, 0);
			GoblinFlap.animTransform = 0;
		}
	}

	void FixedUpdate ()
	{
		if (Score.coin == true && transform.position.y >= Area.sky_limit) {
			goblinAudio.PlayOneShot (pointIncrease);
			Score.increasePoint ();
			this.gravitySpeed += 1;
			//if (this.maxSpeed > 5) {
			//	this.maxSpeed += 1f;
			//}
			float yt = (this.gravity.y - 1f);
			this.gravity = new Vector3 (0f, yt, 0f);
			//float fp = (this.flapVelocity.y + 1f);
			//this.flapVelocity = new Vector3 (0f, fp, 0f);
		}
		if (GoblinFlap.startFlap == true) {
			this.velocity += this.gravity * Time.deltaTime;

			if (GoblinFlap.didFlap == true) {
				GoblinFlap.didFlap = false;
				if (transform.position.y < Area.sky_limit) {
					goblinAudio.PlayOneShot (taptap);
					this.velocity += this.flapVelocity;
				}
			}

			if (GoblinFlap.animTransform == 2) {
				goblinAudio.PlayOneShot (crash);
				this.rotarAnimator.SetInteger ("doAnim", GoblinFlap.animTransform);
				GoblinFlap.animTransform = 0;
			}

			if (transform.position.y >= Area.land) {
				this.velocity = Vector3.ClampMagnitude (this.velocity, this.maxSpeed);
				if (GoblinFlap.hint == false) {
					transform.position += this.velocity * Time.deltaTime;
				} else {
					if (this.velocity.y > -3) {
						transform.position += this.velocity * Time.deltaTime;
					}
				}
				float angle = 20;
				if (this.velocity.y < 0) {
					angle = Mathf.Lerp (0, -20, -this.velocity.y / maxSpeed);
				}
				transform.rotation = Quaternion.Euler (0, 0, angle);
			}

			if (transform.position.y < Area.land) {
				yoyoManager.action = 3;
				goblinAudio.PlayOneShot (laugh);
				if (BGLoop.running == true) {
					BGLoop.running = false;
					ads.bannerView.Show ();
					goblinAudio.PlayOneShot (crash);
					this.rotarAnimator.SetInteger ("doAnim", 2);
					yoyoManager.action = 3;
					controller.setQuestions (false);
					Control.sourceAudio.Stop ();
					Score.gameOver ();
				}
				transform.rotation = Quaternion.Euler (0, 0, 0);
				this.maxSpeed = this.maxSpeedStart;
				this.gravity = this.gravityStart;
				this.flapVelocity = this.flapVelocityStart;
				this.gravitySpeed = 0f;

				GoblinFlap.startFlap = false;
				Control.play.SetActive (true);
				Control.hint.SetActive (true);
			}
	
		}
	}
}
