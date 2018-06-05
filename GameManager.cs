using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public float stamina;
	public float maxStamina;
	public float refreshRate;

	public Image stamBar;

	public Camera cam;
	public float camSpeed;
	public float addAmount;

	public GameObject dark;
	public float darkSpeed;

	public GameObject background;
	public float backSpeed;

	public AudioClip[] sounds;
	AudioClip soundClip;
	public AudioSource source;

	int soundCount = 0;
	int soundCountMax = 12;

	public Animator pauseAnim;
	bool isPaused = false;
	public GameObject pauseMenu;

	// Use this for initialization
	void Start () {

		source.GetComponent<AudioSource> ();

	}

	// Update is called once per frame
	void Update () {

		cam.transform.position += transform.up * Time.deltaTime * camSpeed;

		dark.transform.position += transform.up * Time.deltaTime * darkSpeed;
	
		background.transform.position += transform.up * Time.deltaTime * backSpeed;

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

		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene ("Menu");
		}

		if (soundCount == 2 && Input.GetKeyDown (KeyCode.W)) {
			int rand = Random.Range (0, sounds.Length);
			soundClip = sounds [rand];
			source.clip = soundClip;
			source.Play ();
		}

		if (Input.GetKeyDown (KeyCode.W) && soundCount < soundCountMax) {
			soundCount ++;
		}

		if (Input.GetKeyDown (KeyCode.W) && soundCount >= soundCountMax) {
			soundCount = 0;
		}

	}

	public void IncreaseSpeed(){
		camSpeed += addAmount;
		darkSpeed += addAmount;
	}

	public void GameOver(){
		camSpeed = 0;
		darkSpeed = 0;
		backSpeed = 0;
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
		Debug.Log ("GameOver");
	}

	public void LoadNextScene(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

}
