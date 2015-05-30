using UnityEngine;
using System.Collections;

public class Tank : MonoBehaviour {
	public Shot shot;
	public float shotpower = 0;
	public float maxshotpower;
	public float movespeed = 2;
	public float aimspeed = 1;
	public float chargerate = 1;
	public float Health;
	public Texture2D powerBar;

	Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Health < 0)
			Explode ();
	}

	void Move(string direction){
		if (direction == "left") {
			this.transform.position += transform.right.normalized * -movespeed;
		} else {
			this.transform.position += transform.right.normalized * movespeed;
		}
	}
	void Aim(string direction){
		Transform child = this.transform.GetChild (0);

		if (direction == "left") {
			child.transform.Rotate (0, 0, aimspeed);

		} else {
			child.transform.Rotate (0, 0, -aimspeed);

		}
	}

	void Charge(){
		shotpower = Mathf.Clamp (shotpower+chargerate, 0, maxshotpower);
	}
	void Fire(){

		Transform child = this.transform.GetChild (0);
		Vector3 temp = child.position;
		temp = temp + child.transform.up/3*2;
		Shot newShot = (Shot) Instantiate (shot,temp,Quaternion.identity);
		newShot.GetComponent<Rigidbody2D> ().AddForce(child.transform.up.normalized * shotpower,ForceMode2D.Impulse);
		shotpower = 0;
	}

	void OnGUI()
	{
		var point = Camera.main.WorldToScreenPoint(transform.position);
	
		GUI.Label(new Rect(point.x, Screen.height - point.y+10, 200, 20),	 Health.ToString("0.00"));
		GUI.DrawTexture (new Rect (10, Screen.height - 50, 290/maxshotpower*shotpower, 20), powerBar);
	}

	void Damage(float number){
		Health -= number;
	}

	void Explode(){
		GameObject go = Instantiate (Resources.Load("Explosion"),this.transform.position,Quaternion.identity) as GameObject;
		Explosion explosion = go.GetComponent<Explosion> ();
		explosion.explodeRate = 0.005f;
		explosion.explodeTime = 2;
		explosion.damage = 0.5f;
		Destroy (gameObject);
		
	}
}
