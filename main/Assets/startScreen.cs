using UnityEngine;
using System.Collections;

public class startScreen : MonoBehaviour 
{
	public void startGame()
	{
		Application.LoadLevel("main");
	}
	public void exitGame()
	{
		Application.Quit();
	}
}
