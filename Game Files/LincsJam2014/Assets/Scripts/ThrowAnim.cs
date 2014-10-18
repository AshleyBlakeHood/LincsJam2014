using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ThrowAnim : MonoBehaviour {
	public List<Transform> startPoints;

	private Transform startMarker;
	public Transform endMarker;

	public float speed = 30.0F;
	private float startTime;
	private float journeylength;
	public Transform target;
	public float smooth = 5.0F;

	void Start () {
		int x = Random.Range (0, startPoints.Count);
		startMarker = startPoints [x];
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
