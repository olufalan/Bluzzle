using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {
	

	public GameObject musicPlayer;
	// Use this for initialization
	void Awake ()
	{

		Scene currentScene = SceneManager.GetActiveScene ();
		string sceneName = currentScene.name;

		if (sceneName == "MainMenu") {
			//checks for an object called GameMusic
			musicPlayer = GameObject.Find ("GameMusic");
			if (musicPlayer == null) {//if doesn't exist uses object that is attached to as music player and changes name to GameMusic
				musicPlayer = this.gameObject;
				musicPlayer.name = "GameMusic";
				DontDestroyOnLoad (musicPlayer);
		
			} else {//If not the original audio then just destroy itself
				if (this.gameObject.name != "GameMusic") {
					Destroy (this.gameObject);
				}
			}
		} else if (sceneName == "EndLevel") {
			Destroy (GameObject.Find ("GameMusic"));
		}
	}
}