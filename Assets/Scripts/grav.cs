using UnityEngine;
using System.Collections;

public class grav : MonoBehaviour {

	public float radius;
	public float density;

	// Use this for initialization
	void Start () {
		setScale ();
		foreach (CircleCollider2D c in GetComponents<CircleCollider2D>()) {
			if (c.isTrigger){
				c.radius = Mathf.Sqrt(getMass())*3;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		setScale ();
		setMass ();
	}

	void setScale(){
		GetComponent<Transform> ().localScale = new Vector2 (radius / GetComponent<SpriteRenderer> ().sprite.bounds.size.x * 10, radius / GetComponent<SpriteRenderer> ().sprite.bounds.size.y * 10);
	}

	float getMass(){
		return density * getVolume ();
	}
	void setMass(){
		if (this.GetComponent<Rigidbody2D>())
			GetComponent<Rigidbody2D> ().mass = getMass ();
	}

	float getVolume(){
		return 4 / 3 * Mathf.Pow (radius, 3) * Mathf.PI;
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.GetComponent<grav>() && this.GetComponent<CircleCollider2D>().sharedMaterial.name == "Gfield") {
			Vector2 force = new Vector2(this.GetComponent<Transform>().localPosition.x, this.GetComponent<Transform>().localPosition.y) - new Vector2(other.GetComponent<Transform>().localPosition.x, other.GetComponent<Transform>().localPosition.y);
			float G = Manager.instance.G;
			float mag = G*this.getMass()*other.gameObject.GetComponent<grav>().getMass()/Mathf.Pow (force.magnitude,2);
			if (other.attachedRigidbody)
				other.attachedRigidbody.AddForce (force * mag);
		}
	}
}
