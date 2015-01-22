using UnityEngine;
using System.Collections;

public class TableCatcher : MonoBehaviour {

	private bool onTable;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D (Collider2D col)
	{
		Debug.Log ("WHAT THE HELL");
		if (col.tag == "Enemy")
		{
			col.transform.position = new Vector3 (transform.position.x, (transform.position.y+1), transform.position.z);
			col.rigidbody2D.isKinematic = true;
			col.rigidbody2D.GetComponentInParent<EnemyMovement>().onTable = true;
			onTable = true;
		}
	}
}	
