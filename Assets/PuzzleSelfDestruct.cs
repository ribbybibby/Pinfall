using UnityEngine;
using System.Collections;

public class PuzzleSelfDestruct : MonoBehaviour {
	// Set in Unity
	public float distanceAbove;

	// Private
	private GameObject chaser;

	// NEED TO FIX THIS
	void Start () 
	{
		chaser = GameObject.FindGameObjectWithTag ("Chaser");
	}

	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.y - chaser.transform.position.y > distanceAbove)
		{
			Destroy(gameObject);
		}
	}
}
