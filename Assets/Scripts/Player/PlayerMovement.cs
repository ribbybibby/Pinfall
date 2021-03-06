﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	// Set in Unity Editor
	public KeyCode UpKey; // Up
	public KeyCode LeftKey; // Left
	public KeyCode DownKey; // Down
	public KeyCode RightKey; // Right
	public KeyCode GrabKey; // Grab enemy
	public KeyCode ThrowKey; // Throw enemy
	public float jumpForce; // How hard to jump
	public float throwForce; // How hard to throw
	public float smashForce; // How hard to smash downwards
	public int jumpLimit; // How many jumps (single, double, triple, etc)
	public float airMoveIncrement; // How much to decrease mobility in the air by each update
	public int speed; // Move speed
	public float sightDistance; // Distance for downwards raycast
	public bool smashDown;

	// Bools that track state changes, for use in conditionals
	private bool facingRight; // Facing right?
	private bool inFloor; // Touching the floor?
	private bool inWall; // Touching the wall?
	private bool inLadder; // Touching a ladder?
	private bool inHold; // Holding an enemy?
	private bool movingInLadder; // Moving up a ladder?
	private bool midJump; // Mid-jump?


	private float airMoves; // A variable to increment by airMoveIncrement
	private int startJumps; // A variable to store the jumpLimit value in
	private GameObject enemy; // The enemy object we're touching

	// Use this for initialization
	void Start () {
		facingRight = true;
		startJumps = jumpLimit;
		inWall = false;
		inFloor = false;
		inHold = false;
		movingInLadder = false;
		airMoves = 1;
		midJump = false;
	}
	
	// Update is called once per frame
	void Update () {
		// Movement while on a ladder, not midjump and not holding an enemy
		/* 1. Up Key:
		 * Climbs ladder at half speed, ignore gravity.
		 * 
		 * 2. Down Key:
		 * Fall down ladder. Re-enables gravity.
		 * 
		 */
		if (inLadder == true && midJump == false && inHold == false) 
		{
			if (Input.GetKey (UpKey))
			{
				movingInLadder = true;
				gameObject.rigidbody2D.isKinematic = true;
				transform.Translate(Vector2.up * (speed/2) * Time.deltaTime);
			}
			if (Input.GetKey (DownKey))
			{
				movingInLadder = true;
				gameObject.rigidbody2D.isKinematic = false;
			}
		}

		// Movement while on the floor
		if (inFloor == true) {
			if (Input.GetKeyDown (UpKey) && inHold == false)
			{
				midJump = true;
				rigidbody2D.AddForce(Vector3.up * jumpForce);
				--jumpLimit;
			}
			if (Input.GetKey (RightKey))
			{
				facingRight = true;
				gameObject.transform.eulerAngles = new Vector3 (0,180,-1);
				transform.Translate(-Vector2.right * (speed) * Time.deltaTime);
			}
			if (Input.GetKey (LeftKey))
			{
				facingRight = false;
				gameObject.transform.eulerAngles = new Vector3 (0,0,-1);
				transform.Translate(-Vector2.right * (speed) * Time.deltaTime);
			}
		}

		// Movement while not on the floor or on a Ladder
		if (inFloor == false && movingInLadder == false) 
		{
			// Aerial movement Right
			gameObject.rigidbody2D.gravityScale += ((airMoves-1)/10);
			if (Input.GetKey (RightKey) && smashDown == false)
			{
				facingRight = true;
				gameObject.transform.eulerAngles = new Vector3 (0,180,-1);
				transform.Translate(-Vector2.right * (speed) * Time.deltaTime);
				airMoves += airMoveIncrement;
			}
			// Aerial movement Left
			if (Input.GetKey (LeftKey) && smashDown == false)
			{
				facingRight = false;
				gameObject.transform.eulerAngles = new Vector3 (0,0,-1);
				transform.Translate(-Vector2.right * (speed/airMoves) * Time.deltaTime);
				airMoves += airMoveIncrement;
			}

			// Smash downwards if above a table
			if (Input.GetKey (DownKey))
			{
				RaycastHit2D[] hitdown = Physics2D.RaycastAll (transform.position, -Vector2.up, sightDistance);
				for (int i = 0; i < hitdown.Length; i++)
				{
					if (hitdown[i].collider.tag == "EnemyTable" && hitdown[i].collider.GetComponent<TableCatcher>().onTable == true)
					{
						smashDown = true;
						rigidbody2D.AddForce(Vector3.down * smashForce);
					}
				}
			}
		}

		// Throw
		if (Input.GetKeyDown (ThrowKey) && ! Input.GetKeyDown (LeftKey) && ! Input.GetKeyDown (RightKey) && inHold == true)
		{
			

			if (facingRight == true)
			{
				enemy.transform.parent.rigidbody2D.isKinematic = false;
				enemy.transform.parent.rigidbody2D.AddForce(Vector3.right * (throwForce));
				enemy.transform.parent.rigidbody2D.AddForce(Vector3.up * throwForce);

			}
			if (facingRight == false)
			{
				enemy.transform.parent.rigidbody2D.isKinematic = false;
				enemy.transform.parent.rigidbody2D.AddForce(-Vector3.right * (throwForce));
				enemy.transform.parent.rigidbody2D.AddForce(Vector3.up * throwForce);
			}
			inHold = false;
			enemy.GetComponentInParent<EnemyMovement>().thrown = true;
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{

		// Destroy table and prone enemy with smash downwards
		if (col.tag == "Floor" && col.gameObject.layer == 14 
		    && col.gameObject.transform.parent.GetComponentInChildren<TableCatcher>().onTable == true
		    && smashDown == true)
		{
			Destroy (col.gameObject.transform.parent.GetComponentInChildren<TableCatcher>().enemy);
			Destroy (col.transform.parent.gameObject);
		}

		// Grab enemy
		if (col.tag == "Enemy")
		{
			GrabEnemy(col);
		}

		// Re-enable ladder collisions at the top, do a little jump if coming up the ladder
		if (col.tag == "LadderTop")
		{
			inLadder = false;
			Physics2D.IgnoreLayerCollision (9, 10, false);
			if (gameObject.rigidbody2D.isKinematic == true)
			{
				gameObject.rigidbody2D.isKinematic = false;
				gameObject.rigidbody2D.AddForce(Vector3.up * jumpForce);
			}

			Physics2D.IgnoreLayerCollision (9, 10, false);
		}

		// Ignore ladder collisions once you reach the bottom
		if (col.tag == "LadderBottom")
		{
			Physics2D.IgnoreLayerCollision (9, 10, true);
		}

		// Ignore collisions while climbing a ladder
		if (col.tag == "LadderBody")
		{
			Physics2D.IgnoreLayerCollision (9, 10, true);
			inLadder = true;
		}


		if (col.tag == "Wall")
		{
			inWall = true;
		}

		// If you hit the floor, reset a lot of different things
		if (col.tag == "Floor")
		{
			smashDown = false;
			gameObject.rigidbody2D.gravityScale = 1;
			inFloor = true;
			midJump = false;
			movingInLadder = false;
			jumpLimit = startJumps;
			airMoves = 1;
		}
	}
	
	void OnTriggerStay2D (Collider2D col)
	{

		// Grab enemy
		if (col.tag == "Enemy")
		{
			GrabEnemy(col);
		}

		// If you press down at the top of a ladder, disable collisions and fall down
		if (col.tag == "LadderTop")
		{
			if (Input.GetKey (DownKey) && inHold == false)
			{
				Physics2D.IgnoreLayerCollision (9, 10, true);
				transform.Translate(-Vector2.up * speed/10 * Time.deltaTime);
			}
		}

		if (col.tag == "Wall")
		{
			inWall = true;
		}

		// Make sure certain things remain true or false while on the floor
		if (col.tag == "Floor")
		{
			inFloor = true;
			midJump = false;
			movingInLadder = false;
		}
	}


	void OnTriggerExit2D (Collider2D col)
	{
		// If you leave a ladder, make that clear
		if (col.gameObject.tag == "LadderBody")
		{
			inLadder = false;
		}

		if (col.tag == "Wall")
		{
			inWall = false;
		}
		// If you leave the floor, set jumping variables
		if (col.tag == "Floor")
		{
			if (movingInLadder == false)
			{
				midJump = true;
			}
			inFloor = false;
		}
		
	}

	// Grab Enemy method
	void GrabEnemy(Collider2D col)
	{
		// If held, keep above the player's head
		if (inHold == true)
		{
			col.transform.parent.position = new Vector3 (transform.position.x, (transform.position.y+1), transform.position.z);
		}

		//Grab by removing gravity from the enemy, placing them above the characters head 
		// and rotating them by 90 degrees
		if (Input.GetKeyDown (GrabKey) && ! Input.GetKey (LeftKey) && ! Input.GetKey (RightKey) && inHold == false)
		{
			enemy = col.gameObject;
			col.transform.parent.position = new Vector3 (transform.position.x, (transform.position.y+1), transform.position.z);
			col.transform.parent.rigidbody2D.isKinematic = true;
			col.transform.parent.eulerAngles = new Vector3(0,0,90);
			inHold = true;
			col.GetComponentInParent<EnemyMovement>().inHold = true;
			col.GetComponentInParent<EnemyMovement>().inFloor = false;

			
		}
	}
}
