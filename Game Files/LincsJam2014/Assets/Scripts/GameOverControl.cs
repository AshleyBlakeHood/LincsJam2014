using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverControl : MonoBehaviour
{
	public Text heading;
	public Text description;

	public Button replay;
	public Button mainMenu;
	public Button quit;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void GameLost()
	{
		heading.text = "You Lost!";
		description.text = "Apparently you weren't funny enough to keep the crowds at The Flying Foot entertained..\n\n Would you like to try again?";
	}

	public void GameWon()
	{
		heading.text = "You Won!";
		description.text = "The crowd at The Flying Foot loved you so much they kicked you off the stage!";
	}

	public void Replay()
	{
		Application.LoadLevel (Application.loadedLevelName);
	}

	public void Quit()
	{
		Application.Quit ();
	}

	public void LoadMainMenu()
	{
		Application.LoadLevel ("MainMenu");
	}
}
