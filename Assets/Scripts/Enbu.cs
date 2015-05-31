using UnityEngine;
using System.Collections;

public class Enbu : MonoBehaviour {
	private float startTime;
	public float armtime;
	public float explodetime;
	public float life;
	public float exploderate;
	public float damage;
	public bool real = true;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
		Manager.instance.waitFor.Add(gameObject);
		GetComponent<grav> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > startTime + armtime) {
			GetComponent<SpriteRenderer>().color = Color.green;
			GetComponent<grav> ().enabled = true;
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
		GameObject go = Instantiate (Resources.Load("Explosion"),this.transform.position,Quaternion.identity) as GameObject;
		Explosion explosion = go.GetComponent<Explosion> ();
		explosion.explodeRate = exploderate;
		explosion.explodeTime = explodetime;
		explosion.damage = damage;
		Destroy (gameObject);
	}
	
	
}
