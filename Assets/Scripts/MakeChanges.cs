using UnityEngine;
using System.Collections;

public class MakeChanges : MonoBehaviour {

	//Set in Unity
	public float bigScale;
	public float smallScale;


	// Use this for initialization
	public void ChangeSize ()
	{
		gameObject.rigidbody2D.isKinematic = true;
		gameObject.rigidbody2D.isKinematic = false;

		if (gameObject.tag == "Big")
		{
			transform.localScale = new Vector3 (smallScale, smallScale, 0);
			gameObject.rigidbody2D.gravityScale = 1;
			gameObject.tag = "Small";
		}
		else if (gameObject.tag == "Small")
		{
			gameObject.rigidbody2D.gravityScale = 0;
			transform.localScale = new Vector3 (bigScale, bigScale, 0);
			gameObject.tag = "Big";
		}
	}

}
