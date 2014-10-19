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

	}

	public void MakeAllClap()
	{
		for (int i = 0; i < crowd.Count; i++)
		{
			crowd[i].transform.FindChild ("Static").gameObject.SetActive (false);
			crowd[i].transform.FindChild ("Clapping").gameObject.SetActive (true);
		}

		StartCoroutine(StopAfterTime (1.23f));
	}

	public void MakeAllStop()
	{
		for (int i = 0; i < crowd.Count; i++)
		{
			crowd[i].transform.FindChild ("Static").gameObject.SetActive (true);
			crowd[i].transform.FindChild ("Clapping").gameObject.SetActive (false);
		}
	}

	IEnumerator StopAfterTime(float time)
	{
		yield return new WaitForSeconds(time);
		MakeAllStop ();
	}
}
