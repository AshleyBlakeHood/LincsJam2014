using UnityEngine;
using System.Collections;

public class EndGameScript : MonoBehaviour {

	public GameObject theFoot, kickingFoot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RunEndGame(int endtype)
	{
		if (endtype == 1) 
		{
			GameObject temp = Instantiate(theFoot, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
		} 
		else 
		{
			GameObject temp = Instantiate(kickingFoot, gameObject.transform.position, gameObject.transform.rotation) as GameObject;

		}
	}
}
