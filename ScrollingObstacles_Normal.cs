using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObstacles_Normal : MonoBehaviour {

	Rigidbody2D rb2d;

	Player player;

	// Use this for initialization
	void Start () {

		player = FindObjectOfType<Player> ();

	}

	// Update is called once per frame
	void Update () { 

		if (gameObject.transform.position.y < player.transform.position.y) {

			GetComponent<Collider2D> ().enabled = false;

		}

	}
}
