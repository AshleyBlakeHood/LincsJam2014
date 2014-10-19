using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			EndGame();
				}
	
	}

	void EndGame()
	{
		gameObject.GetComponent<CameraMovement> ().currentCamera = 1;
		gameObject.GetComponent<CameraMovement> ().setCamera (1);
		gameObject.GetComponent<CameraMovement> ().canMoveCamera = false;
		GameObject.FindGameObjectWithTag ("EndGameTag").GetComponent<EndGameScript> ().RunEndGame (1);
	}
}
