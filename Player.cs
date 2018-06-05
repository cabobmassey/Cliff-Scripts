using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float jumpHeight;

	GameManager gameManager;

	private bool right = true;

	Animator anim;

	public Material Climb1;
	public Material Climb2;

	GrapplingHook_Normal grap;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();

		gameManager = FindObjectOfType<GameManager> ();

		grap = FindObjectOfType<GrapplingHook_Normal> ();

	}

	// Update is called once per frame
	void Update () {

		Vector3 screenPos = Camera.main.WorldToScreenPoint (gameObject.transform.position);

		/*if (Input.GetKeyDown (KeyCode.W)) {
			transform.position += transform.up / 6;
			gameManager.stamina -= .1f;
			right = !right;
			if (right) {
				gameObject.GetComponent<Renderer> ().material = Climb1;
			}
			if (!right) {
				gameObject.GetComponent<Renderer> ().material = Climb2;
			}
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			transform.Translate (0, jumpHeight * gameManager.stamina, 0);
			gameManager.stamina /= 3;
		}*/

		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			GetComponent<GrapplingHook_Normal> ().FindSpot ();
		}

		if (screenPos.y >= Screen.height - 10) {
			screenPos.y = Screen.height - 10;
			gameObject.transform.position = Camera.main.ScreenToWorldPoint(screenPos);
		}
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "SpeedUp") {
			gameManager.IncreaseSpeed ();
			Destroy (other);
		}

		if (other.tag == "Hole" && grap.isFlying == false || other.tag == "Darkness" && grap.isFlying == false) {
			StartCoroutine(Death ());
		}

		if (other.tag == "Finish") {
			gameManager.LoadNextScene ();
		}
	}

	IEnumerator Death(){
		anim.SetTrigger ("Die");
		yield return new WaitForSeconds (2.0f);
		gameManager.GameOver();
	}

	public void PlayerClimb(){

		if (grap.isFlying == false) {
			transform.position += transform.up / 6;
			GameManager2.instance.stamina -= .1f;
			right = !right;
			if (right) {
				gameObject.GetComponent<Renderer> ().material = Climb1;
			}
			if (!right) {
				gameObject.GetComponent<Renderer> ().material = Climb2;
			}

			GameManager2.instance.UpdateSound ();

		}

	}

	public void PlayerJump(){

		if (grap.isFlying == false) {
			transform.Translate (0, jumpHeight * GameManager2.instance.stamina, 0);
			GameManager2.instance.stamina /= 3;
		}

	}

}

