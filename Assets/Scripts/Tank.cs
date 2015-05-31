using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tank : MonoBehaviour {
	public GameObject shot;
	public float shotpower = 0;
	public float maxshotpower;
	public float movespeed = 2;
	public float thrustforce;
	public float aimspeed = 1;
	public float chargerate = 1;
	public float Health;
	public Texture2D powerBar;
	string[] shots = {"Shot","Cluster","Salvo"};
	int shotIndex = 0;
	Sprite shotpic;

	Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = this.GetComponent<Rigidbody2D> ();
		shot = Resources.Load (shots [shotIndex]) as GameObject;

	}
	
	// Update is called once per frame
	void Update () {
		if (Health < 0) Explode ();
	}

	void Move(string direction){
		if (direction == "left") {
			this.transform.position += transform.right.normalized * -movespeed;
		} else if (direction == "right"){
			this.transform.position += transform.right.normalized * movespeed;
		}else if (direction == "up"){
			this.rigidBody.AddForce(transform.up.normalized * thrustforce);
		}else if (direction == "down"){
			this.rigidBody.AddForce(transform.up.normalized * -thrustforce);
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

	void Switch(int n){
		shotIndex += n;
		if (shotIndex < 0) {
			shotIndex = shots.Length + shotIndex;
		}
		if (shotIndex >= shots.Length) {
			shotIndex -= shots.Length;
		}
		shot = Resources.Load (shots [shotIndex]) as GameObject;
	}

	void Charge(){
		shotpower = Mathf.Clamp (shotpower+chargerate, 0, maxshotpower);

	}
	void Fire(){

		Transform child = this.transform.GetChild (0);
		Vector3 temp = child.position;
		temp = temp + child.transform.up/3*2;
		GameObject newShot = Instantiate (shot,temp,child.rotation) as GameObject;
		newShot.GetComponent<Rigidbody2D> ().AddForce(child.transform.up.normalized * shotpower,ForceMode2D.Impulse);
		shotpower = 0;
	}	

	void OnGUI()
	{
		if (!Manager.begin)
			return;
		var point = Camera.main.WorldToScreenPoint(transform.position);
	
		GUI.Label(new Rect(point.x, Screen.height - point.y+10, 200, 20),	 Health.ToString("0.00"));
		GUI.DrawTexture (new Rect (10, Screen.height - 50, 290/maxshotpower*shotpower, 20), powerBar);
		
		GUI.Label(new Rect(point.x, Screen.height - point.y, 200, 20),	 shots[shotIndex]);
		if (Manager.instance.getTurn() == this) {
			GUI.Label(new Rect(point.x, Screen.height - point.y-25, 200, 20),"Turn");
		}
	}
	
	void Damage(float number){
		Health -= number;
	}

	void Explode(){
		GameObject go = Instantiate (Resources.Load("Explosion"),this.transform.position,transform.rotation) as GameObject;
		Explosion explosion = go.GetComponent<Explosion> ();
		explosion.explodeRate = 0.005f;
		explosion.explodeTime = 2;
		explosion.damage = 1f;
		Destroy (gameObject);
		
	}
}
