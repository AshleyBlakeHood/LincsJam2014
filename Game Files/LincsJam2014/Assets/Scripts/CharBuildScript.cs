using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharBuildScript : MonoBehaviour
{
	public GameObject maleTemplate;
	public GameObject femaleTemplate;

	//public List<Sprite> headList, torsoList, legList, eyeList, mouthList;
	public GameObject headImage, torsoImage, legImage, eyeImage, mouthImage;

	public int mHeadIndex, mTorsoIndex, mLegIndex;
	public int fHeadIndex, fTorsoIndex, fLegIndex;

	public Text category;
	public Button leftCategory;
	public Button rightCategory;

	public JokeContainer[] jokes;
	int position = 0;

	public int jokeCount = 0;

	public Sprite[] maleHeads;
	public Sprite[] maleNecks;
	public Sprite[] maleTorsos;
	public Sprite[] maleLegs;
	public Sprite[] maleFeets;

	public Sprite[] femaleHeads;
	public Sprite[] femaleNecks;
	public Sprite[] femaleTorsos;
	public Sprite[] femaleLegs;
	public Sprite[] femaleFeets;

	public int gender = 0;

	public Button genderButton;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("Category", 0);

		mHeadIndex = 0;
		mTorsoIndex = 0;
		mLegIndex = 0;

		fHeadIndex = 0;
		fTorsoIndex = 0;
		fLegIndex = 0;

		LoadHahaFiles ();
		category.text = jokes [0].jokeTitle;

		Sprite[] tempMaleHeads = Resources.LoadAll<Sprite>(@"Characters/Male/Heads");
		maleHeads = new Sprite[tempMaleHeads.Length / 4];

		int counter = 0;
		for (int i = 0; i < tempMaleHeads.Length; i++)
		{
			if (i % 4 == 0)
			{
				maleHeads[counter] = tempMaleHeads[i];
				counter++;
			}
		}

		maleNecks = Resources.LoadAll<Sprite>(@"Characters/Male/Neck");
		maleTorsos = Resources.LoadAll<Sprite>(@"Characters/Male/Torso");

		maleLegs = Resources.LoadAll<Sprite>(@"Characters/Male/Legs");

		maleFeets = Resources.LoadAll<Sprite>(@"Characters/Male/Feet");

		//Female
		Sprite[] tempFemaleHeads = Resources.LoadAll<Sprite>(@"Characters/Female/Heads");
		femaleHeads = new Sprite[tempFemaleHeads.Length / 4];

		int fCounter = 0;
		for (int i = 0; i < tempFemaleHeads.Length; i++)
		{
			if (i % 4 == 0)
			{
				femaleHeads[fCounter] = tempFemaleHeads[i];
				fCounter++;
			}
		}
		
		femaleNecks = Resources.LoadAll<Sprite>(@"Characters/Female/Neck");
		femaleTorsos = Resources.LoadAll<Sprite>(@"Characters/Female/Torso");
		
		femaleLegs = Resources.LoadAll<Sprite>(@"Characters/Female/Legs");
		
		femaleFeets = Resources.LoadAll<Sprite>(@"Characters/Female/Feet");
		
		updateHead ();
		updateLeg ();
		updateTorso ();
		updateEye ();
		updateMouth ();
	}

	public void headLeft()
	{
		switch (gender)
		{
		case 0:
			mHeadIndex--;

			if (mHeadIndex <= 0)
			{
				mHeadIndex = 0;
			}
			break;
		case 1:
			fHeadIndex--;
			
			if (fHeadIndex <= 0)
			{
				fHeadIndex = 0;
			}
			break;
		}
		updateHead ();
	}

	public void headRight()
	{
		switch (gender)
		{
		case 0:
			mHeadIndex++;
			
			if (mHeadIndex > maleHeads.Length - 1)
			{
				mHeadIndex = maleHeads.Length - 1;
			}
			break;
		case 1:
			fHeadIndex++;
			
			if (fHeadIndex > femaleHeads.Length - 1)
			{
				fHeadIndex = femaleHeads.Length - 1;
			}
			break;
		}
		updateHead ();
	}

	public void torsoLeft()
	{
		switch (gender)
		{
		case 0:
			mTorsoIndex--;
			
			if (mTorsoIndex <= 0)
			{
				mTorsoIndex = 0;
			}
			break;
		case 1:
			fTorsoIndex--;
			
			if (fTorsoIndex <= 0)
			{
				fTorsoIndex = 0;
			}
			break;
		}
		updateTorso ();
	}

	public void torsoRight()
	{
		switch (gender)
		{
		case 0:
			mTorsoIndex++;
			
			if (mTorsoIndex > maleTorsos.Length - 1)
			{
				mTorsoIndex = maleTorsos.Length - 1;
			}
			break;
		case 1:
			fTorsoIndex++;
			
			if (fTorsoIndex > femaleTorsos.Length - 1)
			{
				fTorsoIndex = femaleTorsos.Length - 1;
			}
			break;
		}
		updateTorso ();
	}

	public void legLeft()
	{
		switch (gender)
		{
		case 0:
			mLegIndex--;
			
			if (mLegIndex <= 0)
			{
				mLegIndex = 0;
			}
			break;
		case 1:
			fLegIndex--;
			
			if (fLegIndex <= 0)
			{
				fLegIndex = 0;
			}
			break;
		}
		updateLeg ();
	}

	public void legRight()
	{
		switch (gender)
		{
		case 0:
			mLegIndex++;
			
			if (mTorsoIndex > maleLegs.Length - 1)
			{
				mTorsoIndex = maleLegs.Length - 1;
			}
			break;
		case 1:
			fLegIndex++;
			
			if (fTorsoIndex > femaleLegs.Length - 1)
			{
				fTorsoIndex = femaleLegs.Length - 1;
			}
			break;
		}
		updateLeg ();
	}

//	public void eyeLeft()
//	{
//		Debug.Log ("eye Left");
//		if (eyeIndex != 0) 
//		{
//			eyeIndex--;
//		} 
//		else 
//		{
//			eyeIndex = eyeList.Count-1;
//		}
//		updateEye ();
//	}
//	
//	public void eyeRight()
//	{
//		Debug.Log ("eye Right");
//		if (eyeIndex < eyeList.Count-1) 
//		{
//			eyeIndex++;
//		} 
//		else 
//		{
//			eyeIndex = 0;
//		}
//		updateEye ();
//	}
//
//	public void mouthLeft()
//	{
//		Debug.Log ("mouth Left");
//		if (mouthIndex != 0) 
//		{
//			mouthIndex--;
//		} 
//		else 
//		{
//			mouthIndex = mouthList.Count-1;
//		}
//		updateMouth ();
//	}
//	
//	public void mouthRight()
//	{
//		Debug.Log ("mouth Right");
//		if (mouthIndex < mouthList.Count-1) 
//		{
//			mouthIndex++;
//		} 
//		else 
//		{
//			mouthIndex = 0;
//		}
//		updateMouth ();
//	}

	void updateHead()
	{
		switch (gender)
		{
		case 0:
			maleTemplate.transform.FindChild ("Head").GetComponent<SpriteRenderer>().sprite = maleHeads[mHeadIndex];
			break;
		case 1:
			femaleTemplate.transform.FindChild ("Head").GetComponent<SpriteRenderer>().sprite = femaleHeads[fHeadIndex];
			break;
		}
	}
	void updateTorso()
	{
		switch (gender)
		{
		case 0:
			maleTemplate.transform.FindChild ("Torso").GetComponent<SpriteRenderer>().sprite = maleTorsos[mTorsoIndex];
			break;
		case 1:
			femaleTemplate.transform.FindChild ("Torso").GetComponent<SpriteRenderer>().sprite = femaleTorsos[fTorsoIndex];
			break;
		}
	}
	void updateLeg()
	{
		switch (gender)
		{
		case 0:
			maleTemplate.transform.FindChild ("Legs").GetComponent<SpriteRenderer>().sprite = maleLegs[mLegIndex];
			break;
		case 1:
			femaleTemplate.transform.FindChild ("Legs").GetComponent<SpriteRenderer>().sprite = femaleLegs[fLegIndex];
			break;
		}
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
		PlayerPrefs.SetInt ("Gender", gender);

		switch (gender)
		{
		case 0:
			PlayerPrefs.SetInt ("HeadIndex", mHeadIndex);
			PlayerPrefs.SetInt ("TorsoIndex", mTorsoIndex);
			PlayerPrefs.SetInt ("LegIndex", mLegIndex);
			break;
		case 1:
			PlayerPrefs.SetInt ("HeadIndex", fHeadIndex);
			PlayerPrefs.SetInt ("TorsoIndex", fTorsoIndex);
			PlayerPrefs.SetInt ("LegIndex", fLegIndex);
			break;
		}

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

	public void ChangeGender()
	{
		if (gender == 0)
		{
			mHeadIndex = 0;
			mTorsoIndex = 0;
			mLegIndex = 0;
			
			fHeadIndex = 0;
			fTorsoIndex = 0;
			fLegIndex = 0;

			genderButton.GetComponentInChildren<Text>().text = "Female";
			maleTemplate.SetActive (false);
			femaleTemplate.SetActive (true);
			gender = 1;
		}
		else
		{
			mHeadIndex = 0;
			mTorsoIndex = 0;
			mLegIndex = 0;
			
			fHeadIndex = 0;
			fTorsoIndex = 0;
			fLegIndex = 0;

			genderButton.GetComponentInChildren<Text>().text = "Male";
			maleTemplate.SetActive (true);
			femaleTemplate.SetActive (false);
			gender = 0;
		}
	}
}
