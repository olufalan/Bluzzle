using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour {
	//Add your scene to scene name to LevelS
	public GameObject mainmenu;
	public GameObject optionsmenu;

	void Awake(){
		
		Destroy (GameObject.Find("GameManager(Clone)"));//destroy gamemanager if already existing
	}
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}


	public void ClassicStart()
	{//Load Game
		SceneManager.LoadScene("level");
		//Application.LoadLevel(1);

	}
	public void StatsStart(){
		//PlaceHolder for actual graph scene
		////Load Stats
		SceneManager.LoadScene("STAT-BuildFrom");
	}
	public void OptionsStart(){
		mainmenu.SetActive (false);
		optionsmenu.SetActive (true);
	
		//Load Screens
	}

	public void Quit()
	{//Leave alone for now
		//Application.Quit();
	}

	///For Options in the Main Menu
	public void Back(){
		mainmenu.SetActive (true);
		optionsmenu.SetActive (false);
	}


	public void SlideLight (float rbgValue){
		RenderSettings.ambientLight = new Color (rbgValue, rbgValue, rbgValue, 1);
	}

	public void SlideVolume (float volume){
		AudioListener.volume = volume;
	}


}
