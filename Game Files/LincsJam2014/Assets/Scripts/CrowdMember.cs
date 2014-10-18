using UnityEngine;
using System.Collections;

public class CrowdMember : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		GameObject.FindGameObjectWithTag ("Crowd Manager").GetComponent<CrowdManager> ().crowd.Add (gameObject);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
