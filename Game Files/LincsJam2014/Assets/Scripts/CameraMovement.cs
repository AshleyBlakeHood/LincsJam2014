using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMovement : MonoBehaviour {

	JokeManager jm;
	public List<Camera> listocameras;
	public int currentCamera;
	public bool canMoveCamera;

	// Use this for initialization
	void Start () {
		canMoveCamera = true;
		jm = GetComponent<JokeManager> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (jm.gameEnded)
			return;

		if (canMoveCamera) {
						if (Input.GetKeyDown (KeyCode.Alpha1)) {
								setCamera (0);
						}
						if (Input.GetKeyDown (KeyCode.Alpha2)) {
								setCamera (1);
						}
						if (Input.GetKeyDown (KeyCode.Alpha3)) {
								setCamera (2);
						}
						if (Input.GetKeyDown (KeyCode.Alpha4)) {
								setCamera (3);
						}
				}
	}

	public void setCamera(int camera)
	{
		currentCamera = camera;

		for (int i = 0; i < listocameras.Count; i++) 
		{
			if(i == currentCamera)
			{
				listocameras[i].depth = 0;
			}
			else
			{
				listocameras[i].depth = -3;
			}
		}
	}


}
