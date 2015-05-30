using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {
	public Tank tank;
	public static Manager instance = null;
	public double G;
	// Use this for initialization
	void Start () {
		if (!instance) {
			instance = this;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("left")) {
			tank.SendMessage("Move","left");
		}
		if (Input.GetKey ("right")) {
			tank.SendMessage("Move","right");
		}
		if (Input.GetKeyDown ("space")) {
			tank.SendMessage("Fire");
		} 
	}
}
