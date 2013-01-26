using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	public void StartGame()
	{
		Application.LoadLevel ("runner 1");
	}
	
	public void LoadHighscores()
	{
		Debug.Log ("Load Highscore");
	}
	
	public void ShowCredits()
	{
		Debug.Log ("Shoe Credits");
	}

}
