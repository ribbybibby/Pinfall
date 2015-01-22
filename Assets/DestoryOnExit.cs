using UnityEngine;
using System.Collections;

public class DestoryOnExit : MonoBehaviour {
	public GameObject blocker;

	// Use this for initialization
	void OnTriggerExit2D (Collider2D col)
	{
		Destroy (blocker);
	}
}
