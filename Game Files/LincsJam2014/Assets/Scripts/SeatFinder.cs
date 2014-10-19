using UnityEngine;
using System.Collections;

public class SeatFinder : MonoBehaviour {
	public GameObject maleCrowdMember;
	public GameObject femaleCrowdMember;
	// Use this for initialization
	void Start () {
	
		Animator[] seats3D = GameObject.FindObjectsOfType<Animator>();
		int blah = 0;
		for (int i = 0; i < seats3D.Length; i++)
		{
			if (seats3D[i].name == "seat10")
			{
				Vector3 seatPos = seats3D[i].transform.position;

				float offset = 6;

				for(int j=0; j<10; j++)
				{
					blah = Random.Range (0, 4);
					//Instantiate(maleCrowdMember, new Vector3(seatPos.x -56,seatPos.y,seatPos.z +40),Quaternion.identity);
					if (blah == 0)
						Instantiate(maleCrowdMember, new Vector3(seatPos.x -offset,seatPos.y - 20,seatPos.z),Quaternion.identity);
					else if(blah == 1)
						Instantiate(femaleCrowdMember, new Vector3(seatPos.x - offset,seatPos.y - 20,seatPos.z),Quaternion.identity);

					offset += 6;
					Debug.Log(seatPos.x);

				}

				Debug.Log (seatPos);

			}
		}
	}
}
