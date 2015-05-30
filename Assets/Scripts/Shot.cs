using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {
	private float startTime;
	public float life;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > startTime + life) {
			Destroy(gameObject);
		}
	}
}
