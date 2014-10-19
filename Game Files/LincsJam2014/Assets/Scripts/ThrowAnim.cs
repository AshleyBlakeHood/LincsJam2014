using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ThrowAnim : MonoBehaviour {
	
	private List<GameObject> startPoints;
	private Transform startMarker;
	private Transform endMarker;

	public float speed = 30.0F;
	private float startTime;
	private float journeylength;
	public Transform target;
	public float smooth = 5.0F;

	void Start () {
		int x = Random.Range (0, GameObject.FindGameObjectWithTag ("Crowd Manager").GetComponent<CrowdManager> ().crowd.Count);
		startMarker = GameObject.FindGameObjectWithTag ("Crowd Manager").GetComponent<CrowdManager> ().crowd [x].transform;
		endMarker = GameObject.Find ("EndPoint").gameObject.transform;
		Debug.Log (x);
		startTime = Time.time;
		journeylength = Vector3.Distance (startMarker.position, endMarker.position);
	}
	void Update () {
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeylength;
		transform.position = Vector3.Lerp (startMarker.position, endMarker.position, fracJourney);
	}
}
