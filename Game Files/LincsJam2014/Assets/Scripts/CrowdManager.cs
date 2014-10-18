using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CrowdManager : MonoBehaviour {
	public List<GameObject> crowd;

	float lastTime = 0;
	bool clap = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (lastTime + 5 < Time.time)
		{
			lastTime = Time.time;
			
			if (clap)
			{
				MakeAllStop ();
				clap = false;
			}
			else
			{
				MakeAllClap ();
				clap = true;
			}
		}
	}

	public void MakeAllClap()
	{
		for (int i = 0; i < crowd.Count; i++)
		{
			crowd[i].transform.FindChild ("Static").gameObject.SetActive (false);
			crowd[i].transform.FindChild ("Clapping").gameObject.SetActive (true);
		}
	}

	public void MakeAllStop()
	{
		for (int i = 0; i < crowd.Count; i++)
		{
			crowd[i].transform.FindChild ("Static").gameObject.SetActive (true);
			crowd[i].transform.FindChild ("Clapping").gameObject.SetActive (false);
		}
	}
}
