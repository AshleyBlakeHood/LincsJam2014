using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class JokeManager : MonoBehaviour
{
	public TextAsset techJokes;

	public GUIManager guiManager;
	public List<Joke> jokes = new List<Joke>();

	int currentJoke = 0;
	int currentLineInJoke = 1;

	public string[] wrongJokes = new string[4];
	public int correctChoice = 0;

	// Use this for initialization
	void Start ()
	{
		JokeScraper js = new JokeScraper ();

		//jokes.AddRange (js.GetJokes ("http://jokes.cc.com/funny-police---military"));
		jokes.AddRange (js.GetJokes (techJokes));
		currentJoke = Random.Range (0, jokes.Count);
		GiveWrongJokesNewValues ();
		UpdateChoiceGUI ();
	}
	
	// Update is called once per frame
	void Update ()
	{
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
		int jokeLine = Random.Range (0, jokes.Count);
		int sentencePos = Random.Range (0, jokes [jokeLine].lines.Length);

		wrongJokes [0] = jokes [jokeLine].lines [sentencePos];

		jokeLine = Random.Range (0, jokes.Count);
		sentencePos = Random.Range (0, jokes [jokeLine].lines.Length);
		
		wrongJokes [1] = jokes [jokeLine].lines [sentencePos];

		jokeLine = Random.Range (0, jokes.Count);
		sentencePos = Random.Range (0, jokes [jokeLine].lines.Length);
		
		wrongJokes [2] = jokes [jokeLine].lines [sentencePos];

		jokeLine = Random.Range (0, jokes.Count);
		sentencePos = Random.Range (0, jokes [jokeLine].lines.Length);
		
		wrongJokes [3] = jokes [jokeLine].lines [sentencePos];
	}

	public void UpdateChoiceGUI()
	{
		string correctJoke = jokes [currentJoke].lines [currentLineInJoke];
		correctChoice = Random.Range (0, 4);

		Debug.Log (wrongJokes [0]);

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

		guiManager.lastSentence.text = jokes [currentJoke].lines [currentLineInJoke - 1];
	}

	public void ButtonPress(int choice)
	{
		currentLineInJoke++;

		if (choice != correctChoice)
		{
			//Lose Points

			//Reset Jokes
			currentJoke = Random.Range (0, jokes.Count);
			currentLineInJoke = 1;
		}

		if (currentLineInJoke >= jokes[currentJoke].lines.Length)
		{
			currentJoke = Random.Range (0, jokes.Count);
			currentLineInJoke = 1;
		}
		
		GiveWrongJokesNewValues ();
		UpdateChoiceGUI ();
	}
}
