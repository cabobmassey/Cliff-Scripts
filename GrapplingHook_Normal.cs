using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook_Normal : MonoBehaviour {
	public Camera cam;
	RaycastHit2D hit;

	public LayerMask cullingMask;
	public float maxDistance;

	public bool isFlying;
	public Vector3 loc;

	public float speed = 10;

	public float moveSpeed;

	public LineRenderer LR;

	Player playerScript;

	// Use this for initialization
	void Start () {

		playerScript = GetComponent<Player> ();

	}

	// Update is called once per frame
	void Update () {

		if (isFlying) {
			playerScript.enabled = false;
			Flying ();
		}

		if (!isFlying) {
			playerScript.enabled = true;
		}

	}

	public void FindSpot(){

		hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay (Input.mousePosition), cullingMask);

		if (hit.collider.tag == "GrapplePoint") {
			isFlying = true;
			LR.enabled = true;
		}

	}

	public void Flying(){

		loc = hit.transform.position;

		transform.position = Vector3.Lerp (transform.position, loc, speed * Time.deltaTime / Vector3.Distance (transform.position, loc));

		LR.SetPosition (0, gameObject.transform.position);

		LR.SetPosition (1, loc);

		if(Vector3.Distance(transform.position, loc) < 0.5){

			isFlying = false;
			LR.enabled = false;

		}

	}
}
