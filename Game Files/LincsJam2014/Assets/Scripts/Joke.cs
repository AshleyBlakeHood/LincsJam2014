using UnityEngine;
using System.Collections;

[System.Serializable]
public class Joke
{
	public string[] lines;

	public Joke(string[] iLines)
	{
		lines = iLines;
	}
}
