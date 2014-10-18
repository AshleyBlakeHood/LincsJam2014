using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMovement : MonoBehaviour {

	public List<Camera> listocameras;
	int currentCamera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			currentCamera = 0;
			setCamera();
		}
		if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			currentCamera = 1;
			setCamera();
		}
		if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			currentCamera = 2;
			setCamera();
		}
		if(Input.GetKeyDown(KeyCode.Alpha4))
		{
			currentCamera = 3;
			setCamera();
		}
	}

	void setCamera()
	{
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
