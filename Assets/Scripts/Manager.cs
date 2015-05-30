using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager : MonoBehaviour {
	public Tank[] tanks;
	private List<Tank> tankturns = new List<Tank> ();
	private Tank tank;
	public static Manager instance = null;
	public float G;
	// Use this for initialization
	void Start () {
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
}
