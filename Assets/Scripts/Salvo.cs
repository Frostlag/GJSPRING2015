using UnityEngine;
using System.Collections;

public class Salvo : MonoBehaviour {
	private float startTime;
	public float volley;
	public float armtime;
	public float explodetime;
	public float life;
	public float exploderate;
	public float damage;
	// Use this for initialization
	void Start () {
		for (int i = 0; i< volley; i++) {
			Vector3 temp = this.transform.position + this.transform.right * (i-volley/2)/10;
			GameObject newShot = Instantiate (Resources.Load("Shot"), temp, Quaternion.identity) as GameObject;
			newShot.GetComponent<Shot>().armtime = armtime;
			newShot.GetComponent<Shot>().exploderate = exploderate;
			newShot.GetComponent<Shot>().explodetime = explodetime;
			newShot.GetComponent<Shot>().life = life;
			newShot.GetComponent<Shot>().damage = damage;
			newShot.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity;
		}
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
