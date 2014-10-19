using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharBuildScript : MonoBehaviour {

	public List<Sprite> headList, torsoList, legList, eyeList, mouthList;
	public GameObject headImage, torsoImage, legImage, eyeImage, mouthImage;

	public int headIndex, torsoIndex, legIndex, eyeIndex, mouthIndex;

	public Text category;
	public Button leftCategory;
	public Button rightCategory;

	public JokeContainer[] jokes;
	int position = 0;

	public int jokeCount = 0;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("Category", 0);

		headIndex = PlayerPrefs.GetInt("headIndex");
		torsoIndex = PlayerPrefs.GetInt("torsoIndex");
		legIndex = PlayerPrefs.GetInt("legIndex");
		eyeIndex = PlayerPrefs.GetInt ("eyeIndex");
		mouthIndex = PlayerPrefs.GetInt ("mouthIndex");
		updateHead ();
		updateLeg ();
		updateTorso ();
		updateEye ();
		updateMouth ();

		LoadHahaFiles ();
		category.text = jokes [0].jokeTitle;
	}

	public void headLeft()
	{
		Debug.Log ("Head Left");
		if (headIndex != 0) 
		{
			headIndex--;
		} 
		else 
		{
			headIndex = headList.Count-1;
		}
		updateHead ();
	}

	public void headRight()
	{
		Debug.Log ("Head Right");
		if (headIndex < headList.Count-1) 
		{
			headIndex++;
		} 
		else 
		{
			headIndex = 0;
		}
		updateHead ();
	}

	public void torsoLeft()
	{
		Debug.Log ("Torso Left");
		if (torsoIndex != 0) 
		{
			torsoIndex--;
		} 
		else 
		{
			torsoIndex = torsoList.Count-1;
		}
		updateTorso ();
	}

	public void torsoRight()
	{
		Debug.Log ("Torso Right");
		if (torsoIndex < torsoList.Count-1) 
		{
			torsoIndex++;
		} 
		else 
		{
			torsoIndex = 0;
		}
		updateTorso ();
	}

	public void legLeft()
	{
		Debug.Log ("Leg Left");
		if (legIndex != 0) 
		{
			legIndex--;
		} 
		else 
		{
			legIndex = legList.Count-1;
		}
		updateLeg ();
	}

	public void legRight()
	{
		Debug.Log ("Leg Right");
		if (legIndex < legList.Count-1) 
		{
			legIndex++;
		} 
		else 
		{
			legIndex = 0;
		}
		updateLeg ();
	}

	public void eyeLeft()
	{
		Debug.Log ("eye Left");
		if (eyeIndex != 0) 
		{
			eyeIndex--;
		} 
		else 
		{
			eyeIndex = eyeList.Count-1;
		}
		updateEye ();
	}
	
	public void eyeRight()
	{
		Debug.Log ("eye Right");
		if (eyeIndex < eyeList.Count-1) 
		{
			eyeIndex++;
		} 
		else 
		{
			eyeIndex = 0;
		}
		updateEye ();
	}

	public void mouthLeft()
	{
		Debug.Log ("mouth Left");
		if (mouthIndex != 0) 
		{
			mouthIndex--;
		} 
		else 
		{
			mouthIndex = mouthList.Count-1;
		}
		updateMouth ();
	}
	
	public void mouthRight()
	{
		Debug.Log ("mouth Right");
		if (mouthIndex < mouthList.Count-1) 
		{
			mouthIndex++;
		} 
		else 
		{
			mouthIndex = 0;
		}
		updateMouth ();
	}

	void updateHead()
	{
		headImage.GetComponent<Image> ().sprite = headList [headIndex];
	}
	void updateTorso()
	{
		torsoImage.GetComponent<Image> ().sprite = torsoList [torsoIndex];
	}
	void updateLeg()
	{
		legImage.GetComponent<Image> ().sprite = legList [legIndex];
	}
	void updateEye()
	{
		//eyeImage.GetComponent<Image> ().sprite = eyeList [eyeIndex];
	}
	void updateMouth()
	{
		//mouthImage.GetComponent<Image> ().sprite = mouthList [mouthIndex];
	}

	public void saveCharacter()
	{
		PlayerPrefs.SetInt ("headIndex", headIndex);
		PlayerPrefs.SetInt ("torsoIndex", torsoIndex);
		PlayerPrefs.SetInt ("legIndex", legIndex);
		PlayerPrefs.SetInt ("mouthIndex", mouthIndex);
		PlayerPrefs.SetInt ("eyeIndex", eyeIndex);
		goToGame ();
	}

	void goToGame()
	{
		PlayerPrefs.SetString ("selectedLevel", "TheatreClub");
		if (PlayerPrefs.GetString ("selectedLevel") == "")
						PlayerPrefs.SetString ("selectedLevel", "TheatreClub");
		Debug.Log (PlayerPrefs.GetString ("selectedLevel"));
		Application.LoadLevel(PlayerPrefs.GetString("selectedLevel"));
	}

	private void LoadHahaFiles()
	{
		TextAsset[] hahaFiles = Resources.LoadAll<TextAsset> ("Haha Files");
		Debug.Log (hahaFiles.Length);
		
		JokeScraper js = new JokeScraper ();
		
		jokes = new JokeContainer[hahaFiles.Length];
		
		for (int i = 0; i < jokes.Length; i++)
		{
			jokes[i] = new JokeContainer(hahaFiles[i].name, js.GetJokes (hahaFiles[i]));
			jokeCount += jokes[i].jokes.Length;
		}
	}

	public void NextCategory()
	{
		position++;

		leftCategory.GetComponent<Button>().interactable = true;

		if (position >= jokes.Length)
		{
			position = jokes.Length - 1;
			rightCategory.GetComponent<Button>().interactable = false;
		}

		UpdateCategoryLabel ();
	}

	public void PreviousCategory()
	{
		position--;

		rightCategory.GetComponent<Button>().interactable = true;

		if (position < 0)
		{
			position = 0;
			leftCategory.GetComponent<Button>().interactable = false;
		}

		UpdateCategoryLabel ();
	}

	public void UpdateCategoryLabel()
	{
		category.text = jokes [position].jokeTitle;
		PlayerPrefs.SetInt ("Category", position);
	}
}
