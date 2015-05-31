using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

public class Manager : MonoBehaviour {
	public Tank[] tanks;
	public bool random;	
	public static Manager instance = null;
	public float G;


	public List<GameObject> waitFor = new List<GameObject> ();
	public static bool begin = false;

	public Spash splash;
	public float minDistance;
	public int maxplanets;
	public float distScale;
	public float xrange;
	public float yrange;

	private bool wait;

	private GameObject tankResource ;
	private GameObject planetResource ;
	private List<Tank> tankturns = new List<Tank> ();
	private List<GameObject> planets = new List<GameObject> ();
	private Tank tank;

	Camera c;

	// Use this for initialization
	void Start () {
		c = Camera.main;
		Vector3 topright = c.ViewportToWorldPoint(new Vector3(1, 1, c.nearClipPlane));

		tankResource = Resources.Load ("Tank") as GameObject;
		planetResource = Resources.Load ("Planet") as GameObject;
		if (random) {
			int numplanets = Random.Range (2, maxplanets);
			try{
				for (int i = 0; i < numplanets; i++) {
					Vector3 pos = newPlanetPos();
					GameObject planet = Instantiate (planetResource, pos, Quaternion.AngleAxis (Random.Range (0, 360), Vector3.back)) as GameObject;
					planets.Add(planet);
				}
			}catch(System.Exception e){}

			tanks = new Tank[2];
			int landon = Random.Range(0,planets.Count+1);
			GameObject newTank = Instantiate (tankResource, planets[landon].transform.position, Quaternion.identity) as GameObject;
			landon = Random.Range(0,planets.Count+1);
			tanks [0] = newTank.GetComponent<Tank> ();
			newTank = Instantiate (tankResource, planets[landon].transform.position, Quaternion.identity) as GameObject;
			tanks [1] = newTank.GetComponent<Tank> ();


		}


		if (!instance) {
			instance = this;
		}
		foreach (Tank t in tanks){
			tankturns.Add(t);
		}
		tank = tankturns[0];
		tankturns.Remove (tank);
	}

	// Update is called once per frame
	void Update () {
		if (!begin)
			return;
		if (wait) {
			if (waitFor.Count != 0) {
				if (waitFor[0] != null){
					c.transform.position = new Vector3 (waitFor[0].transform.position.x, waitFor[0].transform.position.y, -10);
				}
				List<GameObject> tbr = new List<GameObject> ();
				foreach (GameObject go in waitFor) {
					if (go == null)
						tbr.Add (go);
				}
				foreach (GameObject go in tbr) {
					waitFor.Remove (go);
				}
				return;
			} else {
				NextTurn ();
				wait = false;
				//c.transform.position = new Vector3 (0, 0, -10);
			}
		}
		if (tank == null)
			return;
		if (Input.GetKey ("left")) {
			tank.SendMessage("Move","left");
		}
		if (Input.GetKey ("right")) {
			tank.SendMessage("Move","right");
		}
		if (Input.GetKey ("a")) {
			tank.SendMessage("Aim","left");
		}
		if (Input.GetKey ("d")) {
			tank.SendMessage("Aim","right");
		}
		if (Input.GetKey ("space")) {
			tank.SendMessage("Charge");
		}
		if (Input.GetKeyDown ("up")) {
			tank.SendMessage("Move","up");
		} 
		if (Input.GetKeyDown ("down")) {
			tank.SendMessage("Move","down");
		} 
		if (Input.GetKeyUp ("space")) {
			tank.SendMessage("Fire");
			tankturns.Add(tank);
			tank = null;
			wait = true;
		}
		if (Input.GetKeyDown ("q")) {
			tank.SendMessage("Switch",-1);
		}
		if (Input.GetKeyDown ("e")) {
			tank.SendMessage("Switch",1);
		}
		Transform a = tanks[0].GetComponent<Transform>();
		Transform b = tanks[1].GetComponent<Transform>();
		Vector3 half = (a.position + b.position) * 0.5f;
		float dist = Vector3.Distance(a.position, b.position);

		c.transform.position = half + Vector3.back * ( dist * distScale);
	}
	void ShotEnd(){
		NextTurn ();
	}
	void NextTurn(){
		tank = tankturns [0];
		tankturns.Remove (tank);
	}
	void GameOver(){
		begin = false;
		splash.gameObject.SetActive (true);
		splash.SendMessage ("GameOver");
	}
	public Tank getTurn(){ return tank; }

	bool isFarEnough(Vector3 pos){
		foreach (GameObject go in planets) {
			if (Vector3.Distance(go.transform.position,pos) < minDistance) return false;
		}
	    return true;
	}

	Vector3 newPlanetPos(){
		Stopwatch stopwatch = Stopwatch.StartNew ();
		Vector3 ret = new Vector3 (Random.Range (-xrange, xrange), Random.Range (-yrange, yrange));
		while (!isFarEnough(ret)) {
			if (stopwatch.Elapsed.Seconds > 2) throw new System.Exception("no more space");
			ret = new Vector3 (Random.Range (-xrange, xrange), Random.Range (-yrange, yrange));
		}
		return ret;

	}


}
