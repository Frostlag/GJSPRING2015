using UnityEngine;
using System.Collections;

public class Tank : MonoBehaviour {
	public Shot shot;
	public float shotpower = 0;
	public float maxshotpower;
	public float thrustpower = 2;
	public float aimspeed = 1;
	public float chargerate = 1;

	Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	void Move(string direction){
		if (direction == "left") {
			rigidBody.AddForce (transform.right.normalized * -thrustpower);
		} else {
			rigidBody.AddForce (transform.right.normalized * thrustpower);
		}
	}
	void Aim(string direction){
		Transform child = this.transform.GetChild (0);

		if (direction == "left") {
			child.transform.Rotate (0, 0, aimspeed);

		} else {
			child.transform.Rotate (0, 0, -aimspeed);

		}
	}

	void Charge(){
		shotpower += chargerate;
	}
	void Fire(){
		Transform child = this.transform.GetChild (0);
		Vector3 temp = child.position;
		temp = temp + child.transform.up/5;
		Shot newShot = (Shot) Instantiate (shot,temp,Quaternion.identity);
		newShot.GetComponent<Rigidbody2D> ().AddForce(child.transform.up.normalized * shotpower);
		shotpower = 0;
	}

	void OnGUI()
	{
		var point = Camera.main.WorldToScreenPoint(transform.position);
		GUI.Label(new Rect(point.x, Screen.currentResolution.height - point.y - 200, 200, 200), name);
	}
}
