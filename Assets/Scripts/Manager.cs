using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager : MonoBehaviour {
	public Tank[] tanks;
	private List<Tank> tankturns = new List<Tank> ();
	private Tank tank;
	public static Manager instance = null;
	public float G;

	public List<GameObject> waitFor = new List<GameObject> ();

	private bool wait;
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
		if (wait) {
			if (waitFor.Count != 0){
				List<GameObject> tbr = new List<GameObject>();
				foreach (GameObject go in waitFor){
					if (go == null) tbr.Add(go);
				}
				foreach (GameObject go in tbr){
					waitFor.Remove(go);
				}
				return;
			}else{
				NextTurn();
				wait = false;
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
}
