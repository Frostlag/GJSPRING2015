﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cluster : MonoBehaviour {
	private float startTime;
	
	public float armtime;
	public float explodetime;
	public float life;
	public float exploderate;
	public float spread;
	public float concentration;
	public float damage;
	public int shots;
	bool dead = false;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
		Manager.instance.waitFor.Add (gameObject);
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
	
	void OnCollisionStay2D(Collision2D other){
		if ( Time.time > startTime + armtime) {
			Explode();
		}
	}
	
	void OnCollisionEnter2D(Collision2D other){
		if ( Time.time > startTime + armtime) {
			Explode();
		}
	}
	
	void Explode(){
		if (!dead) {
			dead = true;
			for (int i=0; i<shots; i++) {
				GameObject newShot = Instantiate (Resources.Load("Shot"),this.transform.position,Quaternion.identity) as GameObject;
				newShot.GetComponent<Shot>().real = false;
				newShot.GetComponent<Shot>().explodetime = explodetime;
				newShot.GetComponent<Shot>().exploderate = exploderate;
				newShot.GetComponent<Shot>().armtime = armtime;
				newShot.GetComponent<Shot>().life = life;
				newShot.GetComponent<Shot>().damage = damage;
				newShot.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity;
				Vector2 force = Quaternion.AngleAxis (spread * i, Vector3.right) * (this.transform.InverseTransformDirection (GetComponent<Rigidbody2D> ().velocity) / GetComponent<Rigidbody2D>().velocity.magnitude);
				newShot.GetComponent<Rigidbody2D> ().AddForce (force * concentration, ForceMode2D.Impulse);
			}
		}
		Destroy (this.gameObject);
	}
	
	
}
