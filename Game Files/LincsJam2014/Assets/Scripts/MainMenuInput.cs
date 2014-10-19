using UnityEngine;
using System.Collections;

public class MainMenuInput : MonoBehaviour {


	public GameObject play, quit;
	public Light spot1, spot2, playSpot, quitSpot;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		RaycastHit rayHit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast (ray.origin, ray.direction, out rayHit, 100)) 
		{
			if(rayHit.collider == play.GetComponent<MeshCollider>())
			{
				playSpot.enabled = true;
				quitSpot.enabled = false;
				spot1.enabled = false;
				spot1.enabled = false;
				if(Input.GetMouseButtonDown(0))
				{
					Application.LoadLevel("CharacterBuilding");
				}
			}
			else if(rayHit.collider == quit.GetComponent<MeshCollider>())
			{
				playSpot.enabled = false;
				quitSpot.enabled = true;
				spot1.enabled = false;
				spot1.enabled = false;
				if(Input.GetMouseButtonDown(0))
				{
					Application.Quit();
				}
			}
			else
			{
				playSpot.enabled = false;
				quitSpot.enabled = false;
				spot1.enabled = true;
				spot1.enabled = true;
			}
		}

	}
}
