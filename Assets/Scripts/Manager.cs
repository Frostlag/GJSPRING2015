﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Manager : MonoBehaviour {
	public Tank[] tanks;
	private List<Tank> tankturns = new List<Tank> ();
	private Tank tank;
	public static Manager instance = null;
	public float G;
	Camera c;

	public List<GameObject> waitFor = new List<GameObject> ();

	private bool wait;
	private GameObject tankResource ;
	private GameObject planetResource ;

	// Use this for initialization
	void Start () {
		tankResource = Resources.Load ("Tank") as GameObject;
		planetResource = Resources.Load ("Planet") as GameObject;
		tanks = new Tank[2];
		GameObject newTank = Instantiate (tankResource, new Vector2(Random.Range (-10,10),Random.Range(-10,10)), Quaternion.identity) as GameObject;
		tanks [0] = newTank.GetComponent<Tank>();
		newTank = Instantiate (tankResource, new Vector2(Random.Range (-10,10),Random.Range(-10,10)), Quaternion.identity) as GameObject;
		tanks [1] = newTank.GetComponent<Tank>();

		int planets = Random.Range(2, 9);

		for (int i = 0; i < planets; i++) {
			Vector2 pos = new Vector2(Random.Range (-10,10),Random.Range(-10,10));
			GameObject planet = Instantiate(planetResource,pos,Quaternion.AngleAxis(Random.Range (0,360),Vector3.back)) as GameObject;
		}

		c = Camera.main;
		if (!instance) {
			instance = this;
		}
		foreach (Tank t in tanks){
			tankturns.Add(t);
		}
		tank = tankturns[0];
		tankturns.Remove (tank);
	}

	// Update is called once per frame
	void Update () {
		if (wait) {
			if (waitFor.Count != 0) {
				if (waitFor[0] != null){
					c.transform.position = new Vector3 (waitFor[0].transform.position.x, waitFor[0].transform.position.y, -10);
				}
				List<GameObject> tbr = new List<GameObject> ();
				foreach (GameObject go in waitFor) {
					if (go == null)
						tbr.Add (go);
				}
				foreach (GameObject go in tbr) {
					waitFor.Remove (go);
				}
				return;
			} else {
				NextTurn ();
				wait = false;
				c.transform.position = new Vector3 (0, 0, -10);
			}
		}
		if (tank == null)
			return;
		if (Input.GetKey ("left")) {
			tank.SendMessage("Move","left");
		}
		if (Input.GetKey ("right")) {
			tank.SendMessage("Move","right");
		}
		if (Input.GetKey ("a")) {
			tank.SendMessage("Aim","left");
		}
		if (Input.GetKey ("d")) {
			tank.SendMessage("Aim","right");
		}
		if (Input.GetKey ("space")) {
			tank.SendMessage("Charge");
		}
		if (Input.GetKeyDown ("up")) {
			tank.SendMessage("Move","up");
		} 
		if (Input.GetKeyDown ("down")) {
			tank.SendMessage("Move","down");
		} 
		if (Input.GetKeyUp ("space")) {
			tank.SendMessage("Fire");
			tankturns.Add(tank);
			tank = null;
			wait = true;
		}
		if (Input.GetKeyDown ("q")) {
			tank.SendMessage("Switch",-1);
		}
		if (Input.GetKeyDown ("e")) {
			tank.SendMessage("Switch",1);
		}
	}
	void ShotEnd(){
		NextTurn ();
	}
	void NextTurn(){
		tank = tankturns [0];
		tankturns.Remove (tank);
	}

	public Tank getTurn(){ return tank; }
}
