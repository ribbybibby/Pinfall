using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	public float speed;

	private bool facingright;
	public bool inFloor;
	public bool inHold;
	public bool onTable;
	public bool thrown;

	// Use this for initialization
	void Start () {
		int rndNo = Random.Range (0, 1);
		if (rndNo == 0)
		{
			facingright = true;
		}
		if (rndNo == 1)
		{
			facingright = false;
		}
		inFloor = true;
		inHold = false;
		onTable = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (inFloor == true && inHold == false && onTable == false)
		{
			if (facingright == true)
			{
				transform.Translate(Vector2.right * speed * Time.deltaTime);
			}

			if (facingright == false)
			{
				transform.Translate(-Vector2.right * speed * Time.deltaTime);
			}
		}

	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "EnemyTable" && inFloor == false)
		{
			transform.position = new Vector3 (col.transform.position.x, (col.transform.position.y+1), col.transform.position.z);
			rigidbody2D.isKinematic = true;
			onTable = true;
		}
		if (col.name == "LeftWall")
		{
			facingright = true;
		}

		if (col.name == "RightWall")
		{
			facingright = false;
		}

		if (col.tag == "Floor" && inHold == false)
		{
			gameObject.transform.eulerAngles = new Vector3 (0,0,0);
			inFloor = true;
		}
	}

	void OnTriggerStay2D (Collider2D col)
	{
		if (col.tag == "Floor" && inHold == false)
		{
			inFloor = true;
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if (col.tag == "Floor")
		{
			inFloor = false;
		}

		if (col.tag == "Player")
		{
			inHold = false;
		}
	}
}
