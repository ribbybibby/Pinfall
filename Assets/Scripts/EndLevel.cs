using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour {
	public string Level;
	public int PlayerNum;


	void OnTriggerEnter2D(Collider2D col)
	{
		if (PlayerNum == 1)
		{
			Application.LoadLevel (Level);
	
		}
	}
}