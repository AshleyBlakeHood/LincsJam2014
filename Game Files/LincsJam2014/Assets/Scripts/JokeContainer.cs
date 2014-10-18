using UnityEngine;
using System.Collections;

[System.Serializable]
public class JokeContainer
{
	public string jokeTitle = "";
	public Joke[] jokes;

	public JokeContainer(string iJokeTitle, Joke[] iJokes)
	{
		jokeTitle = iJokeTitle;
		jokes = iJokes;
	}
}
