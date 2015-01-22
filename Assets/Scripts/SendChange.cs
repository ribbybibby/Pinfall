using UnityEngine;
using System.Collections;

public class SendChange : MonoBehaviour {
	public Vector3 moveto;

	private Vector3 startpos;

	void Start () {
		startpos = transform.position;
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (transform.position == startpos) 
		{
			transform.position = moveto;		
		}
		else if (transform.position == moveto)
		{
			transform.position = startpos;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		other.transform.SendMessage ("ChangeSize", SendMessageOptions.DontRequireReceiver);
	}
}
