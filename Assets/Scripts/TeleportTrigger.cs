using UnityEngine;
using System.Collections;

public class TeleportTrigger : MonoBehaviour {
	public string tag1;
	public string tag2;
	public Vector2 goTo;

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == tag1 || col.gameObject.tag == tag2)
		{
			col.transform.position = goTo;
		}
	}
}
