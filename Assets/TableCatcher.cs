using UnityEngine;
using System.Collections;

public class TableCatcher : MonoBehaviour {

	public bool onTable;

	public GameObject enemy;
		
	void OnTriggerStay2D (Collider2D col)
	{
		if (col.tag == "Enemy" && col.GetComponentInParent<EnemyMovement>().onTable == true)
		{
			onTable = true;
			enemy = col.gameObject;
		}
	}
}	
