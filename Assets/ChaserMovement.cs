using UnityEngine;
using System.Collections;

public class ChaserMovement : MonoBehaviour {

	public Vector3 downSpeed;
	public float flashTimer;

	private float storeTimer;
	private bool isRed;

	// Use this for initialization
	void Start () {
		storeTimer = Time.time + flashTimer;
		gameObject.GetComponent<MeshRenderer> ().material.color = Color.red;
		isRed = true;
	}
	
	// Update is called once per frame
	void Update () {
		//Physics2D.IgnoreLayerCollision (0, 0);
		gameObject.transform.position += downSpeed*Time.deltaTime;

		if (Time.time >= storeTimer)
		{
			if (isRed == false)
			{
				gameObject.GetComponent<MeshRenderer> ().material.color = Color.red;
				isRed = true;
			}
			else if (isRed == true)
			{
				gameObject.GetComponent<MeshRenderer> ().material.color = Color.yellow;
				isRed = false;
			}
			storeTimer = Time.time + flashTimer;
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player")
		{
			Application.LoadLevel(0);
		}
	}
}
