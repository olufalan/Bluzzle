using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenuButtons : MonoBehaviour {
	public Score go;
	public LineGraphManager so;
	public bool ispause;
	// Use this for initialization

	void Start () {

			
		
	}
	
	// Update is called once per frame
	void Update () {
		
			
		if (Input.GetKeyDown("p")) {
			Time.timeScale = 0;
			SceneManager.LoadScene ("PauseMenu");
		}
	}
	public void Resume(){
			Time.timeScale = 1;
			SceneManager.LoadScene ("level");
			Time.timeScale = 1;
		////Load Stats
	}
	public void TimeSt(){
		so = GetComponent<LineGraphManager>();
		go = GetComponent<Score>();
		go.scoreList.Add (go.scoreConv);
		Score.timeList.Add(go.timep);
		so.deci = 2;
		SceneManager.LoadScene ("STAT-BuildFrom");
	}
	public void Quit_to_ScoreSt(){
		so = GetComponent<LineGraphManager>();
		so.deci = 1;
		go = GetComponent<Score>();
		go.scoreList.Add (go.scoreConv);
		Score.timeList.Add(go.timep);
		SceneManager.LoadScene("STAT-BuildFrom");
	}
	public void Quit_To_Menu()
	{
		go = GetComponent<Score>();
		go.scoreList.Add (go.scoreConv);
		Score.timeList.Add(go.timep);
		//go back to main menu
		SceneManager.LoadScene("MainMenu");
	}
}

