using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	private float startTime;
	public float explodeTime;
	public float explodeRate;
	public float damage;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale += new Vector3(explodeRate,explodeRate);
		if (Time.time > startTime + explodeTime){
			Destroy(gameObject);
		}
	}

	void OnTriggerStay2D(Collider2D other){
		
		if (other.sharedMaterial.name == "tank") {
			other.gameObject.SendMessage ("Damage", damage);
		}
	}
}
