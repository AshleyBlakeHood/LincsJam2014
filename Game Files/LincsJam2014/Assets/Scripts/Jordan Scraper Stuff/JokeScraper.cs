using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;

public class JokeScraper
{
	WebClient wc = new WebClient();
	string page = "";

	public List<string> jokes = new List<string>();

	// Use this for initialization
	void Start ()
	{

	}
	
	public Joke[] GetJokes(string webAddress)
	{
		//Download page of jokes.
		page = wc.DownloadString (webAddress);
		
		//Get all the joke urls.
		string[] rawJokesplit = page.Split (new string[] {"<div class=\"module_content\">", "<div class=\"header\"><h2>Joke Categories</h2></div>"}, System.StringSplitOptions.None)[1].Split (new string[] {"<a href=\"http://jokes.cc.com/"}, System.StringSplitOptions.None);
		//File.WriteAllText (@"C:\Users\computing\Desktop\Split.txt", rawJokesplit[0]);
		
		
		string[] jokeUrls = new string[rawJokesplit.Length];
		
		for (int i = 1; i < rawJokesplit.Length; i++)
		{
			jokeUrls[i] = rawJokesplit[i].Split ('"')[0];
			string s = wc.DownloadString ("http://jokes.cc.com/" + jokeUrls[i]);
			
			s = s.Split(new string[] {"<meta name=\"description\" content="}, System.StringSplitOptions.None)[1].Split (new string[] {"<meta name"}, System.StringSplitOptions.None)[0];
			string jokeContent = s.Split (new string[] {"Comedy Central Jokes - "}, System.StringSplitOptions.None)[1];
			
			//first - 
			int index = jokeContent.IndexOf(" - ");
			jokeContent = jokeContent.Remove(0, index + 3);
			
			jokes.Add (jokeContent);
		}

		Joke[] jokeArray = new Joke[jokes.Count];

		for (int i = 0; i < jokeArray.Length; i++)
		{
			string[] jokeLines = jokes[i].Split (new string[] {". ", ".&lt", "! ", "!&lt", "? ", "?&lt", "&amp;br&amp;", "\n&lt;br&gt;\n", "./pp", "\n"}, System.StringSplitOptions.RemoveEmptyEntries);

			for (int d = 0; d < jokeLines.Length; d++)
			{
				string cleanLine = CleanLine(jokeLines[d]);
				jokeLines[d] = cleanLine += ".";
			}

			jokeArray[i] = new Joke(jokeLines);
		}

		return jokeArray;
	}

	public Joke[] GetJokes(TextAsset csvFile)
	{
		//Split Lines
		string[] lineSplit = csvFile.text.Split (new string[] {System.Environment.NewLine}, System.StringSplitOptions.RemoveEmptyEntries);

		Joke[] jokeArray = new Joke[lineSplit.Length];
		
		for (int i = 0; i < jokeArray.Length; i++)
		{
			string[] jokeLines = lineSplit[i].Split (new string[] {","}, System.StringSplitOptions.RemoveEmptyEntries);

			for (int l = 0; l < jokeLines.Length; l++)
			{
				jokeLines[l] = jokeLines[l].Replace ("[COMMA]", ",").Replace ("[QUOTE]", "\"");
			}

			jokeArray[i] = new Joke(jokeLines);
		}
		
		return jokeArray;
	}

	public string CleanLine(string s)
	{
		s = s.Replace ("&#039;", "'");
		s = s.Replace ("&lt;", "");
		s = s.Replace ("p align=&amp;", "");
		s = s.Replace ("quot;left", "");
		s = s.Replace ("&gt;", "");
		s = s.Replace ("quot;", "");
		s = s.Replace (";BRBR&amp;", "");
		s = s.Replace ("\" />", "");
		s = s.Replace ("&amp;", "");
		s = s.Replace (";BRBR", "");
		return s;
	}
}
