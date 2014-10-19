using UnityEngine;
using System.Collections;

public class SeatFinder : MonoBehaviour {
	public GameObject maleCrowdMember;
	public GameObject femaleCrowdMember;
	// Use this for initialization
	void Start () {
	
		Animator[] seats3D = GameObject.FindObjectsOfType<Animator>();

		for (int i = 0; i < seats3D.Length; i++)
		{
			if (seats3D[i].name == "seat10")
			{
				Vector3 seatPos = seats3D[i].transform.position;

				float offset = -15;

				for(int j=0; j<10; j++)
				{
					//Instantiate(maleCrowdMember, new Vector3(seatPos.x -56,seatPos.y,seatPos.z +40),Quaternion.identity);
					if (Random.Range (0, 2) == 0)
						Instantiate(maleCrowdMember, new Vector3(seatPos.x + 1,seatPos.y - 20,seatPos.z),Quaternion.identity);
					else
						Instantiate(femaleCrowdMember, new Vector3(seatPos.x + (j*20),seatPos.y - 20,seatPos.z),Quaternion.identity);

					offset += 3;
					Debug.Log(seatPos.x);

				}

				Debug.Log (seatPos);

			}
		}
	}
}
