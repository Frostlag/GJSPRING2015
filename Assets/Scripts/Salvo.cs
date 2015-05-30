using UnityEngine;
using System.Collections;

public class Salvo : MonoBehaviour {
	private float startTime;
	public float volley;
	public float armtime;
	public float explodetime;
	public float life;
	public float exploderate;
	// Use this for initialization
	void Start () {
		for (int i = 0; i< volley; i++) {
			Vector3 temp = this.transform.position + this.transform.right * (i-volley/2)/10;
			GameObject newShot = Instantiate (Resources.Load("Shot"), temp, Quaternion.identity) as GameObject;
			newShot.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity;


		}
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
