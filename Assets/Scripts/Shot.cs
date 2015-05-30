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
		Invoke ("NextTurn",2);
		Explosion explosion = Resources.Load("Explosion") as Explosion;
		Debug.Log (explosion);
		Explosion texplosion = Instantiate (explosion);
		Destroy (gameObject);
	}
	void NextTurn(){
		Manager.instance.SendMessage("NextTurn");
	}
	
}
