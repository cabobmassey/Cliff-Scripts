using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {

	PlayerProgress playerProgress;

	// Use this for initialization
	void Start () {

		DontDestroyOnLoad (gameObject);

		LoadPlayerProgress ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SubmitNewPlayerScore(int newScore){

		if (newScore > playerProgress.highestScore) {

			playerProgress.highestScore = newScore;

			SavePlayerProgress ();

		}

	}

	public void LoadPlayerProgress(){

		playerProgress = new PlayerProgress ();

		if(PlayerPrefs.HasKey("highestScore")){

			playerProgress.highestScore = PlayerPrefs.GetInt("highestScore");

		}

	}

	public int GetHighestPlayerScore(){

		return playerProgress.highestScore;

	}

	private void SavePlayerProgress(){

		PlayerPrefs.SetInt ("highestScore", playerProgress.highestScore);

	}
}
