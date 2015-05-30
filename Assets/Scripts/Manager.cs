using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {
	public Tank tank;
	// Use this for initialization
	void Start () {
		
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
