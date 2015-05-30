using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cluster : MonoBehaviour {
	private float startTime;
	
	public float armtime;
	public float explodetime;
	public float life;
	public float exploderate;
	bool dead = false;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > startTime + armtime) {
			GetComponent<SpriteRenderer>().color = Color.yellow;
		}
		if (Time.time > startTime + life) {
			Explode();
		}
		
	}
	
	void OnCollisionEnter2D(Collision2D other){
		if (Time.time > startTime + armtime) {
			Explode();
		}
	}
	
	void Explode(){
		Manager.instance.SendMessage ("ShotEnd");
		if (!dead) {
			dead = true;
			for (int i=0; i<5; i++) {
				GameObject newShot = Instantiate (Resources.Load("Shot"),this.transform.position,Quaternion.identity) as GameObject;
				Vector2 force = Quaternion.AngleAxis (45 * i, Vector3.right) * (this.transform.InverseTransformDirection (GetComponent<Rigidbody2D> ().velocity) / GetComponent<Rigidbody2D>().velocity.magnitude);
				newShot.GetComponent<Rigidbody2D> ().AddForce (force * 10, ForceMode2D.Impulse);
			}
		}
		Destroy (this.gameObject);
	}
	
	
}
