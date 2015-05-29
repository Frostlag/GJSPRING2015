using UnityEngine;
using System.Collections;

public class Tank : MonoBehaviour {
	public Shot shot;
	public int power = 100;

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
			Debug.Log("left");
			rigidBody.AddForce (transform.right.normalized * -1);
		} else {
			Debug.Log("right");
			rigidBody.AddForce (transform.right.normalized * 1);
		}
	}

	void Fire(){
		Shot newShot = (Shot) Instantiate (shot,transform.position,Quaternion.identity);
		newShot.GetComponent<Rigidbody2D> ().AddForce(transform.up.normalized * power);
	}
}
