using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGLoop : MonoBehaviour {
	
	public static bool running = false;
	private Vector3 __forwardGrassPos;
	private Vector3 __forwardCloudPos;
	private float __grassSpeed;
	private float __cloudSpeed;
	
	private GameObject __grass_1;
	private GameObject __grass_2;
	private GameObject __grass_3;
	private GameObject __grass_4;
	private GameObject __grass_5;
	private GameObject __grass_6;
	private GameObject __grass_7;
	private GameObject __grass_8;
	private GameObject __grass_9;
	private GameObject __grass_0;
	private GameObject __cloud_1;
	private GameObject __cloud_2;
	private GameObject __cloud_3;
	private GameObject __cloud_4;
	private GameObject __cloud_5;
	private GameObject __cloud_6;
	private GameObject __cloud_7;
	private GameObject __cloud_8;
	private GameObject __cloud_9;
	private GameObject __cloud_0;
	private List<GameObject> __grass;
	private List<GameObject> __cloud;
	
	private Vector3 __pos;
	
	void Awake(){
		this.__grass = new List<GameObject>();
		this.__cloud = new List<GameObject>();
		this.__forwardGrassPos = new Vector3(4f, -3.0f, -1f);
		this.__forwardCloudPos = new Vector3(4f, -0.87f, 0f);
		this.__grassSpeed = 1f;
		this.__cloudSpeed = 0.1f;
	}

	// Use this for initialization
	void Start () {
		BGLoop.running = true;
		this.__loadObjectGrass();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void FixedUpdate(){
		if (BGLoop.running == true) {
			this.__move ();
		}
	}
	
	private void __loadObjectGrass(){
		this.__grass_1 = GameObject.Find("background_02");
		this.__grass_2 = GameObject.Find("background_03");
		this.__grass_3 = GameObject.Find("background_04");
		this.__grass_4 = GameObject.Find("background_05");
		this.__grass_5 = GameObject.Find("background_06");
		this.__grass_6 = GameObject.Find("background_07");
		this.__grass_7 = GameObject.Find("background_08");
		this.__grass_8 = GameObject.Find("background_09");
		this.__grass_9 = GameObject.Find("background_10");
		this.__grass_0 = GameObject.Find("background_11");
		this.__cloud_1 = GameObject.Find("cloud_01");
		this.__cloud_2 = GameObject.Find("cloud_02");
		this.__cloud_3 = GameObject.Find("cloud_03");
		this.__cloud_4 = GameObject.Find("cloud_04");
		this.__cloud_5 = GameObject.Find("cloud_05");
		this.__cloud_6 = GameObject.Find("cloud_06");
		this.__cloud_7 = GameObject.Find("cloud_07");
		this.__cloud_8 = GameObject.Find("cloud_08");
		this.__cloud_9 = GameObject.Find("cloud_09");
		this.__cloud_0 = GameObject.Find("cloud_10");
		
		
		this.__grass.Add(this.__grass_1);
		this.__grass.Add(this.__grass_2);
		this.__grass.Add(this.__grass_3); 
		this.__grass.Add(this.__grass_4);
		this.__grass.Add(this.__grass_5);
		this.__grass.Add(this.__grass_6);
		this.__grass.Add(this.__grass_7);
		this.__grass.Add(this.__grass_8);
		this.__grass.Add(this.__grass_9);
		this.__grass.Add(this.__grass_0);
		
		this. __cloud.Add(this.__cloud_1);
		this. __cloud.Add(this.__cloud_2);
		this. __cloud.Add(this.__cloud_3);
		this. __cloud.Add(this.__cloud_4);
		this. __cloud.Add(this.__cloud_5);
		this. __cloud.Add(this.__cloud_6);
		this. __cloud.Add(this.__cloud_7);
		this. __cloud.Add(this.__cloud_8);
		this. __cloud.Add(this.__cloud_9);
		this. __cloud.Add(this.__cloud_0);
		
	}
	
	private void __move(){
		foreach (GameObject gra in this.__grass){
			this.__pos = gra.transform.position;
			this.__pos.x = this.__pos.x - this.__grassSpeed * Time.deltaTime;
			gra.transform.position = this.__pos;
			if (gra.transform.position.x <= -4.0f){
				gra.transform.position = this.__forwardGrassPos;
			}
		}
		
		foreach (GameObject clo in this.__cloud){
			this.__pos = clo.transform.position;
			this.__pos.x = this.__pos.x - this.__cloudSpeed * Time.deltaTime;
			clo.transform.position = this.__pos;
			if (clo.transform.position.x <= -4.0f){
				clo.transform.position = this.__forwardCloudPos;
			}
		}
		
	}
}
