using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {
	private float startTime;

	public float armtime;
	public float explodetime;
	public float life;
	public float exploderate;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > startTime + armtime) {
			GetComponent<SpriteRenderer>().color = Color.red;
		}
		if (Time.time > startTime + life) {
			Explode();
		}

	}

	void OnCollisionEnter2D(Collision2D other){
		if ( Time.time > startTime + armtime) {
			Explode();
		}
	}

	void Explode(){
		Manager.instance.SendMessage ("ShotEnd");
		GameObject go = Instantiate (Resources.Load("Explosion"),this.transform.position,Quaternion.identity) as GameObject;
		Explosion explosion = go.GetComponent<Explosion> ();
		explosion.explodeRate = 0.005f;
		explosion.explodeTime = 0.5f;
		explosion.damage = 1f;
		Destroy (gameObject);

	}

	
}
