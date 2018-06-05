using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour {

	public static GameManager2 instance;

	public bool gameOver = false;

	public float stamina;
	public float maxStamina;
	public float refreshRate;

	public Image stamBar;

	public float addAmount;

	public GameObject dark;
	public float darkSpeed;
	float darkTimer;

	public AudioClip[] sounds;
	AudioClip soundClip;
	public AudioSource source;

	int soundCount = 0;
	int soundCountMax = 12;

	public float scrollSpeed;

	int distance = 0;
	public Text disText;

	float timer;

	public Animator canvasAnim;
	public GameObject gameOverMenu;

	public Animator pauseAnim;
	bool isPaused = false;
	public GameObject pauseMenu;

	public DataManager datamanager;

	public Text highScoreDisplay;

	void Awake(){
		
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {

		gameOverMenu.SetActive(false);

		pauseMenu.SetActive(false);

		Time.timeScale = 1;

		source.GetComponent<AudioSource> ();

	}

	// Update is called once per frame
	void Update () {

		darkTimer += Time.deltaTime;

		timer = Time.timeSinceLevelLoad;

		CountDistance ();

		dark.transform.position += transform.up * Time.deltaTime * darkSpeed;

		//background.transform.position += transform.up * Time.deltaTime * backSpeed;

		if (stamina < 10) {
			stamina += refreshRate * Time.deltaTime;
		}

		if (stamina <= 0) {
			stamina = 0;
		}

		if (stamina > maxStamina) {
			stamina = maxStamina;
		}
			
		stamBar.fillAmount = (stamina / maxStamina);

		if (darkTimer >= 20) {

			StartCoroutine (MoveDark());

			darkTimer = 0;
		}
			

	}

	public void GameOver(){
		Time.timeScale = 0;
		gameOverMenu.SetActive(true);
		canvasAnim.SetTrigger ("FadeIn");

		datamanager.SubmitNewPlayerScore (distance);
		highScoreDisplay.text = "HIGH SCORE: " + datamanager.GetHighestPlayerScore ().ToString ();
	}

	public void LoadNextScene(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		Debug.Log ("Load Next Scene");
	}

	void CountDistance(){

		distance = Mathf.RoundToInt(timer);

		disText.text = "" + distance;

	}

	IEnumerator MoveDark(){

		float curDarkSpeed = darkSpeed;
		float curScrollSpeed = scrollSpeed;

		darkSpeed = -1;
		yield return new WaitForSeconds (3f);
		darkSpeed = curDarkSpeed;
		scrollSpeed = curScrollSpeed - .2f;

	}

	public void PauseTrigger(){

		isPaused = !isPaused;

		if (isPaused) {
			Time.timeScale = 0;
			pauseMenu.SetActive(true);
			pauseAnim.SetTrigger ("PauseOn");
		}

		if (!isPaused) {
			Time.timeScale = 1;
			pauseAnim.SetTrigger ("PauseOff");
			pauseMenu.SetActive(false);
		}

	}

	public void UpdateSound(){

		if (soundCount == 2) {
			int rand = Random.Range (0, sounds.Length);
			soundClip = sounds [rand];
			source.clip = soundClip;
			source.Play ();
		}

		if (soundCount < soundCountMax) {
			soundCount ++;
		}

		if (soundCount >= soundCountMax) {
			soundCount = 0;
		}

	}

}
