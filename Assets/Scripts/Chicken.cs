using UnityEngine;
using System.Collections;

public class Chicken : MonoBehaviour {
	private float startTime;
	public float armtime;
	public float explodetime;
	public float life;
	public float exploderate;
	public float damage;
	public int eggs;
	public float laytime;
	float layfreq;
	float startExplode = 0;
	float laynow;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
		layfreq = laytime / eggs;
		Manager.instance.waitFor.Add(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > startTime + armtime) {
			GetComponent<SpriteRenderer>().color = new Color(1,0,1,1);
			if (startExplode == 0){
				startExplode = Time.time;
				laynow = Time.time;
			}
			Lay();
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
		GameObject go = Instantiate (Resources.Load("Explosion"),this.transform.position,Quaternion.identity) as GameObject;
		Explosion explosion = go.GetComponent<Explosion> ();
		explosion.explodeRate = exploderate;
		explosion.explodeTime = explodetime;
		explosion.damage = damage;
		Destroy (gameObject);
	}
	
	void Lay(){
		if (Time.time < startExplode + laytime + layfreq) {
			if (Time.time > laynow + layfreq) {
				laynow = Time.time;
				GetComponent<grav>().radius -= GetComponent<grav>().radius / eggs;
				Vector3 offset = GetComponent<Rigidbody2D>().velocity/GetComponent<Rigidbody2D>().velocity.magnitude;
				offset *= GetComponent<Renderer>().bounds.size.x;
				GameObject newShot = Instantiate (Resources.Load ("Shot"), this.transform.position-offset, Quaternion.identity) as GameObject;
				newShot.GetComponent<Shot> ().real = false;
				newShot.GetComponent<Shot> ().explodetime = explodetime;
				newShot.GetComponent<Shot> ().exploderate = exploderate;
				newShot.GetComponent<Shot> ().armtime = armtime;
				newShot.GetComponent<Shot> ().life = life;
				newShot.GetComponent<Shot> ().damage = damage;
			}
		} else {
			Destroy (this.gameObject);
		}

	}
	
	
}
