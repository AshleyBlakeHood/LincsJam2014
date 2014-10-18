using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class JokeManager : MonoBehaviour
{
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

	// Use this for initialization
	void Start ()
	{
		LoadHahaFiles ();

		JokeScraper js = new JokeScraper ();

		//jokes.AddRange (js.GetJokes ("http://jokes.cc.com/funny-police---military"));
		currentJoke = Random.Range (0, jokes[chosenCategory].jokes.Length);
		GiveWrongJokesNewValues ();
		UpdateChoiceGUI ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (timeLimitImage.rectTransform.rect.width <= 0)
		{
			//Lose Points
			
			//Reset Jokes
			currentJoke = Random.Range (0, jokes [chosenCategory].jokes.Length);
			currentLineInJoke = 1;

			timeLimitImage.rectTransform.sizeDelta = new Vector2(tickerStartWidth, timeLimitImage.rectTransform.rect.height);

			if (currentLineInJoke >= jokes[chosenCategory].jokes[currentJoke].lines.Length)
			{
				currentJoke = Random.Range (0, jokes [chosenCategory].jokes.Length);
				currentLineInJoke = 1;
			}
			
			GiveWrongJokesNewValues ();
			UpdateChoiceGUI ();
		}
	}

	void FixedUpdate()
	{
		//rectTransform.sizeDelta = new Vector2( yourWidth, yourHeight);
		timeLimitImage.rectTransform.sizeDelta = new Vector2(timeLimitImage.rectTransform.rect.width - Time.deltaTime * 50, timeLimitImage.rectTransform.rect.height);
		//timeLimitImage.rectTransform.rect.width = timeLimitImage.rectTransform.rect.width + Time.deltaTime * 5;
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
		}

		chosenCategory = Random.Range (0, jokes.Length);
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

	public void GiveWrongJokesNewValues()
	{
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

	public void UpdateChoiceGUI()
	{
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

		guiManager.lastSentence.text = jokes [chosenCategory].jokes [currentJoke].lines [currentLineInJoke - 1];
	}

	public void ButtonPress(int choice)
	{
		currentLineInJoke++;

		if (choice != correctChoice)
		{
			//Lose Points

			//Reset Jokes
			currentJoke = Random.Range (0, jokes [chosenCategory].jokes.Length);
			currentLineInJoke = 1;
		}

		if (currentLineInJoke >= jokes[chosenCategory].jokes[currentJoke].lines.Length)
		{
			currentJoke = Random.Range (0, jokes [chosenCategory].jokes.Length);
			currentLineInJoke = 1;
		}
		
		GiveWrongJokesNewValues ();
		UpdateChoiceGUI ();
	}
}
