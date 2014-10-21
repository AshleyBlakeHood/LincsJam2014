using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class JokeManager : MonoBehaviour
{
	CameraMovement cm;

	public CrowdManager crowdManager;

	public GameObject maleTemplate;
	public GameObject femaleTemplate;

	public TextAsset[] textAssetJokes;

	public JokeContainer[] jokes;

	public GUIManager guiManager;
	//public List<Joke> jokes = new List<Joke>();

	int currentJoke = 0;
	int currentLineInJoke = 1;

	public string[] wrongJokes = new string[4];
	public int correctChoice = 0;

	int chosenCategory = 0;
	float tickerStartWidth = 500;

	public Image timeLimitImage;

	public int jokeCount = 0;
	public bool selectingJoke = true;

	private int[] currentIndexes = new int[4];

	private int succesfulJokes = 0;
	private int unsuccesfulJokes = 0;

	private int weighting = 0;

	public bool gameEnded = false;

	public GameObject throwingFoot;

	public AudioClip[] hecklerAudio;

	// Use this for initialization
	void Start ()
	{
		cm = GetComponent<CameraMovement> ();

		LoadHahaFiles ();

		JokeScraper js = new JokeScraper ();

		//jokes.AddRange (js.GetJokes ("http://jokes.cc.com/funny-police---military"));
		currentJoke = Random.Range (0, jokes[chosenCategory].jokes.Length);

		GetAllCorrectChoices ();

		hecklerAudio = Resources.LoadAll<AudioClip>("");

		if (PlayerPrefs.GetInt ("Gender") == 0)
		{
			maleTemplate.SetActive (true);

			maleTemplate.transform.FindChild ("Head").GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>(@"Characters/Male/Heads")[PlayerPrefs.GetInt ("HeadIndex")];

			maleTemplate.transform.FindChild ("Neck").GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>(@"Characters/Male/Neck")[0];
			maleTemplate.transform.FindChild ("Torso").GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>(@"Characters/Male/Torso")[PlayerPrefs.GetInt ("TorsoIndex")];
			
			maleTemplate.transform.FindChild ("Legs").GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>(@"Characters/Male/Legs")[PlayerPrefs.GetInt ("LegsIndex")];
			
			maleTemplate.transform.FindChild ("Feet").GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>(@"Characters/Male/Feet")[0];
		}
		else
		{
			femaleTemplate.SetActive (true);

			//Female
			femaleTemplate.transform.FindChild ("Head").GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>(@"Characters/Female/Heads")[PlayerPrefs.GetInt ("HeadIndex")];
			
			femaleTemplate.transform.FindChild ("Neck").GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>(@"Characters/Female/Neck")[0];
			femaleTemplate.transform.FindChild ("Torso").GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>(@"Characters/Female/Torso")[PlayerPrefs.GetInt ("TorsoIndex")];
			
			femaleTemplate.transform.FindChild ("Legs").GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>(@"Characters/Female/Legs")[PlayerPrefs.GetInt ("LegsIndex")];
			
			femaleTemplate.transform.FindChild ("Feet").GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>(@"Characters/Female/Feet")[0];
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (timeLimitImage.rectTransform.rect.width <= 0)
		{
			//Lose Points
			
			//Reset Jokes
			//ResetJoke ();

			timeLimitImage.rectTransform.sizeDelta = new Vector2(tickerStartWidth, timeLimitImage.rectTransform.rect.height);

			GetAllCorrectChoices ();

			unsuccesfulJokes++;
			weighting--;
			guiManager.unsuccessfulJokes.text = "Unsuccessful Jokes: " + unsuccesfulJokes;
			SpawnFoot();
			PlayHeckle();
			CheckEndGame ();


//			if (currentLineInJoke >= jokes[chosenCategory].jokes[currentJoke].lines.Length)
//			{
//				ResetJoke ();
//			}
//
//			UpdateChoices ();
		}

		if(Input.GetKeyDown(KeyCode.A))
		{
			PlayHeckle();
		}
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

		chosenCategory = PlayerPrefs.GetInt ("Category");
		Debug.Log (jokes [chosenCategory].jokeTitle);
	}

	void OnGUI()
	{
//		string correctJoke = jokes [currentJoke].lines [currentLineInJoke];
//		string lastJoke = jokes [currentJoke].lines [currentLineInJoke - 1];
//		string output = string.Format ("Line {5} of {6}\nLine just Spoken: {4}\n\nCorrect Joke: {0}\nWrong Joke 1: {1}\nWrong Joke 2: {2}\nWrong Joke 3: {3}", correctJoke, wrongJokes [0], wrongJokes [1], wrongJokes [2], lastJoke, currentLineInJoke - 1, jokes [currentJoke].lines.Length - 1);
//
//		GUI.Label (new Rect (0, 0, Screen.width, Screen.height), output);
	}

	public void GetAllCorrectChoices()
	{
		if (gameEnded)
			return;

		cm.setCamera (Random.Range (0, 4));

		selectingJoke = true;
		currentLineInJoke = 0;

		int jokeLine = Random.Range (0, jokes[chosenCategory].jokes.Length);
		wrongJokes [0] = jokes [chosenCategory].jokes[jokeLine].lines [0];
		currentIndexes [0] = jokeLine;

		jokeLine = Random.Range (0, jokes[chosenCategory].jokes.Length);
		wrongJokes [1] = jokes [chosenCategory].jokes[jokeLine].lines [0];
		currentIndexes [1] = jokeLine;
		
		jokeLine = Random.Range (0, jokes[chosenCategory].jokes.Length);
		wrongJokes [2] = jokes [chosenCategory].jokes[jokeLine].lines [0];
		currentIndexes [2] = jokeLine;
		
		jokeLine = Random.Range (0, jokes[chosenCategory].jokes.Length);
		wrongJokes [3] = jokes [chosenCategory].jokes[jokeLine].lines [0];
		currentIndexes [3] = jokeLine;

		guiManager.topLeftPanel.GetComponentInChildren<Button>().gameObject.GetComponentInChildren<Text>().text = wrongJokes[0];
		guiManager.topRightPanel.GetComponentInChildren<Button>().gameObject.GetComponentInChildren<Text>().text = wrongJokes[1];
		guiManager.bottomLeftPanel.GetComponentInChildren<Button>().gameObject.GetComponentInChildren<Text>().text = wrongJokes[2];
		guiManager.bottomRightPanel.GetComponentInChildren<Button>().gameObject.GetComponentInChildren<Text>().text = wrongJokes[3];

		guiManager.lastSentence.text = "";

		timeLimitImage.rectTransform.sizeDelta = new Vector2(tickerStartWidth, timeLimitImage.rectTransform.rect.height);
	}

	public void GiveWrongJokesNewValues()
	{
		if (gameEnded)
			return;

		int jokeLine = Random.Range (0, jokes[chosenCategory].jokes.Length);
		int sentencePos = Random.Range (0, jokes [chosenCategory].jokes [jokeLine].lines.Length);

		wrongJokes [0] = jokes [chosenCategory].jokes[jokeLine].lines [sentencePos];

		jokeLine = Random.Range (0, jokes[chosenCategory].jokes.Length);
		sentencePos = Random.Range (0, jokes [chosenCategory].jokes [jokeLine].lines.Length);
		
		wrongJokes [1] = jokes [chosenCategory].jokes[jokeLine].lines [sentencePos];

		jokeLine = Random.Range (0, jokes[chosenCategory].jokes.Length);
		sentencePos = Random.Range (0, jokes [chosenCategory].jokes [jokeLine].lines.Length);
		
		wrongJokes [2] = jokes [chosenCategory].jokes[jokeLine].lines [sentencePos];

		jokeLine = Random.Range (0, jokes[chosenCategory].jokes.Length);
		sentencePos = Random.Range (0, jokes [chosenCategory].jokes [jokeLine].lines.Length);
		
		wrongJokes [3] = jokes [chosenCategory].jokes[jokeLine].lines [sentencePos];
	}

	public void UpdateChoices()
	{
		if (gameEnded)
			return;

		//Update Wrong Answers
		GiveWrongJokesNewValues ();

		string correctJoke = jokes [chosenCategory].jokes [currentJoke].lines [currentLineInJoke];
		correctChoice = Random.Range (0, 4);

		guiManager.topLeftPanel.GetComponentInChildren<Button>().gameObject.GetComponentInChildren<Text>().text = wrongJokes[0];
		guiManager.topRightPanel.GetComponentInChildren<Button>().gameObject.GetComponentInChildren<Text>().text = wrongJokes[1];
		guiManager.bottomLeftPanel.GetComponentInChildren<Button>().gameObject.GetComponentInChildren<Text>().text = wrongJokes[2];
		guiManager.bottomRightPanel.GetComponentInChildren<Button>().gameObject.GetComponentInChildren<Text>().text = wrongJokes[3];

		switch (correctChoice)
		{
		case 0:
			guiManager.topLeftPanel.GetComponentInChildren<Button>().gameObject.GetComponentInChildren<Text>().text = correctJoke;
			break;
		case 1:
			guiManager.topRightPanel.GetComponentInChildren<Button>().gameObject.GetComponentInChildren<Text>().text = correctJoke;
			break;
		case 2:
			guiManager.bottomLeftPanel.GetComponentInChildren<Button>().gameObject.GetComponentInChildren<Text>().text = correctJoke;
			break;
		case 3:
			guiManager.bottomRightPanel.GetComponentInChildren<Button>().gameObject.GetComponentInChildren<Text>().text = correctJoke;
			break;
		}

		if (currentLineInJoke > 0)
			guiManager.lastSentence.text = jokes [chosenCategory].jokes [currentJoke].lines [currentLineInJoke - 1];
		else
			guiManager.lastSentence.text = "";

		timeLimitImage.rectTransform.sizeDelta = new Vector2(tickerStartWidth, timeLimitImage.rectTransform.rect.height);
	}

	public void ButtonPress(int choice)
	{
		currentLineInJoke++;

		if (selectingJoke)
		{
			currentJoke = currentIndexes[choice];
			selectingJoke = false;
		}
		else
		{
			if (choice != correctChoice)
			{
				//Lose Points
				unsuccesfulJokes++;
				weighting--;
				guiManager.unsuccessfulJokes.text = "Unsuccessful Jokes: " + unsuccesfulJokes;
				CheckEndGame ();
				//Reset Jokes
				GetAllCorrectChoices ();
				SpawnFoot();
				PlayHeckle();
			}
		}

		if (currentLineInJoke >= jokes[chosenCategory].jokes[currentJoke].lines.Length)
		{
			succesfulJokes++;
			weighting++;
			guiManager.successfulJokes.text = "Successful Jokes: " + succesfulJokes;
			CheckEndGame ();
			crowdManager.MakeAllClap ();

			GetAllCorrectChoices ();
		}
		else
			UpdateChoices ();
	}

	public void ResetJoke()
	{
		timeLimitImage.rectTransform.sizeDelta = new Vector2(tickerStartWidth, timeLimitImage.rectTransform.rect.height);

		currentJoke = Random.Range (0, jokes [chosenCategory].jokes.Length);
		currentLineInJoke = 1;
	}

	public void CheckEndGame()
	{
		if (weighting >= 3)
		{
			WinGame ();
		}
		else if (weighting <= -3)
		{
			EndGame ();
		}
	}

	void EndGame()
	{
		Debug.Log ("GAME LOSS");
		gameEnded = true;

		timeLimitImage.transform.parent.GetComponent<Canvas> ().gameObject.SetActive (false);

		gameObject.GetComponent<CameraMovement> ().currentCamera = 1;
		gameObject.GetComponent<CameraMovement> ().setCamera (1);
		gameObject.GetComponent<CameraMovement> ().canMoveCamera = false;
		GameObject.FindGameObjectWithTag ("EndGameTag").GetComponent<EndGameScript> ().RunEndGame (1);

		
		guiManager.gameOverObject.SetActive (true);
		guiManager.gameOverObject.GetComponent<GameOverControl> ().GameLost ();
	}

	void WinGame()
	{
		Debug.Log ("GAME WIN");
		gameEnded = true;

		timeLimitImage.transform.parent.GetComponent<Canvas> ().gameObject.SetActive (false);
		
		gameObject.GetComponent<CameraMovement> ().currentCamera = 1;
		gameObject.GetComponent<CameraMovement> ().setCamera (1);
		gameObject.GetComponent<CameraMovement> ().canMoveCamera = false;
		GameObject.FindGameObjectWithTag ("EndGameTag").GetComponent<EndGameScript> ().RunEndGame (0);

		switch (PlayerPrefs.GetInt ("Gender"))
		{
		case 0:
			maleTemplate.GetComponent<Animator>().enabled = true;
			break;
		case 1:
			femaleTemplate.GetComponent<Animator>().enabled = true;
			break;
		}

		guiManager.gameOverObject.SetActive (true);
		guiManager.gameOverObject.GetComponent<GameOverControl> ().GameWon ();
	}

	void SpawnFoot()
	{
		int rand = Random.Range(2,20);
		for(int i = 0; i<rand; i++)
		{
			Instantiate(throwingFoot);
		}
	}

	void PlayHeckle()
	{
		Debug.Log ("Playing heckle");
		gameObject.GetComponent<AudioSource> ().clip = hecklerAudio [Random.Range (0, hecklerAudio.Length)];
		gameObject.GetComponent<AudioSource> ().Play();
	}
}
