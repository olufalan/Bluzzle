using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour {

	public Text livesText;
	public Text scoreText;

	private int livesleft;
	private static int score; 
	public float timep;
	public List<float> scoreList = new List<float>();
	static public List<float> timeList = new List<float>();
	public float scoreConv;
	// Use this for initialization

	void Start() {
		//DontDestroyOnLoad (GameObject);
		livesleft = 5; //number of starting lives
		livesText.text="";
		scoreText.text = "";
		score = 500;

	}
	
	// Update is called once per frame
	void Update () {

		if (livesleft == 0) {
			gameOver();//failed level scene
		}


		if (timep >= 5f)
		{
			score = 500;
			updateScore ();
			timep = 0.0f;
		}

		timep += UnityEngine.Time.deltaTime;
	}

	void updateText(){
		livesText.text="Lives Remaining: " + livesleft.ToString();
	}

	public void getScore(){

		if (livesleft > 0) {
			switch (livesleft) {
			case 1://4 lives lost
				break;
			case 2://3 lives lost
				score = (int)(score * 1.2);
				break;
			case 3://2 lives lost
				score = (int)(score * 1.4);
				break;
			case 4://1 life lost
				score = (int)(score * 1.6);
				break;
			case 5://0 lives lost
				score = score * 2;
				break;
			}

		} else {
			score = 0; //score is 0 if number of lives 
		}
	}

	void gameOver(){
		//Tim - Attempt at gathering the score for the graph
		//T - Attempt at rearanging the score to fit on the graph. 
		int bscore = score/100; 
		scoreConv = (float)bscore;
		scoreList.Add (scoreConv);
		timeList.Add (timep);
		//unload previous level scene
		SceneManager.LoadScene ("EndLevel");
	}

	void updateScore(){
		getScore ();
		scoreText.text = "Score: "+ score.ToString();
	}
		
	public void up(){
		TileType.Player.is_Walked();
		if (TileType.Player.get_neighbor(0).TileTypeNum() == 2)
		{
			TileType.Player.get_neighbor(0).is_Walked();
			livesleft--;
			updateText ();
		}
		else if (TileType.Player.get_neighbor(0).TileTypeNum() == 1)
		{
			TileType.Player.is_Walked();
		}
		else if (TileType.Player.get_neighbor(0).TileTypeNum() == 6)
		{
			TileType.Player.is_Walked();
		}
		else if (TileType.Player.get_neighbor(0).TileTypeNum() == 5)
		{
			//pauseControl = true;
			TileType.Player = TileType.Player.get_neighbor(0);
			TileType.Player.is_Walked();
		}
		else
		{
			TileType.Player = TileType.Player.get_neighbor(0);
			TileType.Player.is_Walked();
		}
	}

	public void down(){
		TileType.Player.is_Walked();
		if (TileType.Player.get_neighbor(1).TileTypeNum() == 2)
		{
			TileType.Player.get_neighbor(1).is_Walked();
			livesleft--;
			updateText ();
		}
		else if (TileType.Player.get_neighbor(1).TileTypeNum() == 1)
		{
			TileType.Player.is_Walked();
		}
		else if (TileType.Player.get_neighbor(1).TileTypeNum() == 6)
		{
			TileType.Player.is_Walked();
		}
		else if (TileType.Player.get_neighbor(1).TileTypeNum() == 5)
		{
			//pauseControl = true;
			TileType.Player = TileType.Player.get_neighbor(1);
			TileType.Player.is_Walked();
		}
		else
		{
			TileType.Player = TileType.Player.get_neighbor(1);
			TileType.Player.is_Walked();
		}
	}

	public void left(){
		TileType.Player.is_Walked();
		if (TileType.Player.get_neighbor(3).TileTypeNum() == 2)
		{
			TileType.Player.get_neighbor(3).is_Walked();
			livesleft--;
			updateText ();
		}
		else if (TileType.Player.get_neighbor(3).TileTypeNum() == 1)
		{
			TileType.Player.is_Walked();
		}
		else if (TileType.Player.get_neighbor(3).TileTypeNum() == 6)
		{
			TileType.Player.is_Walked();
		}
		else if (TileType.Player.get_neighbor(3).TileTypeNum() == 5)
		{
			//pauseControl = true;
			TileType.Player = TileType.Player.get_neighbor(3);
			TileType.Player.is_Walked();
		}
		else
		{
			TileType.Player = TileType.Player.get_neighbor(3);
			TileType.Player.is_Walked();
		}
	}

	public void right(){
		TileType.Player.is_Walked();
		if (TileType.Player.get_neighbor(2).TileTypeNum() == 2)
		{
			TileType.Player.get_neighbor(2).is_Walked();
			livesleft--;
			updateText ();
		}
		else if (TileType.Player.get_neighbor(2).TileTypeNum() == 1)
		{
			TileType.Player.is_Walked();
		}
		else if (TileType.Player.get_neighbor(2).TileTypeNum() == 6)
		{
			TileType.Player.is_Walked();
		}
		else if (TileType.Player.get_neighbor(2).TileTypeNum() == 5)
		{
			//pauseControl = true;
			TileType.Player = TileType.Player.get_neighbor(2);
			TileType.Player.is_Walked();
		}
		else
		{
			TileType.Player = TileType.Player.get_neighbor(2);
			TileType.Player.is_Walked();
		}
	}
}
