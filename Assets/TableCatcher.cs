using UnityEngine;
using System.Collections;

public class TableCatcher : MonoBehaviour {

	public bool onTable;

	public GameObject enemy;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	void OnTriggerStay2D (Collider2D col)
	{
		if (col.tag == "Enemy" && col.GetComponentInParent<EnemyMovement>().onTable == true)
		{
			onTable = true;
			enemy = col.gameObject;
		}
	}
}	
