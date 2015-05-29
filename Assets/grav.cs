using UnityEngine;
using System.Collections;

public class grav : MonoBehaviour {

	public float radius;
	public float density;	

	// Use this for initialization
	void Start () {

		GetComponent<Transform> ().localScale = new Vector2 (radius, radius);
		GetComponent<CircleCollider2D> ().radius = 9999;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	float getMass(){
		return density * getVolume ();
	}

	float getVolume(){
		return 4 / 3 * Mathf.Pow (radius, 3) * Mathf.PI;
	}
}
