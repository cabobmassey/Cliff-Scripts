using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public DataManager dataManager;

	public Text highScoreDisplay;


	void Awake() {
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 30;
	}

	// Use this for initialization
	void Start () {

		dataManager.LoadPlayerProgress ();

		highScoreDisplay.text = "HIGH SCORE: " + dataManager.GetHighestPlayerScore ().ToString ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
