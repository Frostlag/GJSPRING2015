using UnityEngine;
using System.Collections;

public class Spash : MonoBehaviour {
	private bool over;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}


	void OnGUI(){
		if (!over) {
			GUI.Label (new Rect (Screen.width / 2 - 50, Screen.height / 3, 100, 20), "GShot");
			if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height / 2, 100, 20), "Play")) {
				Manager.begin = true;
				gameObject.SetActive(false);
			}
		}else{
			GUI.Label (new Rect (Screen.width / 2 - 50, Screen.height / 3, 100, 20), "Game Over");
			if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height / 2, 100, 20), "Restart")) {
				Application.LoadLevel(Application.loadedLevel);
			}
		}

	}

	void GameOver(){

		gameObject.SetActive (true);
		over = true;
	}

	            
}
