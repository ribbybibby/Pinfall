using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ChangeGUITextTrigger : MonoBehaviour {
	public string textSamples;
	public Text txt;


	void OnTriggerEnter2D (Collider2D col)
	{
		txt.text = textSamples;
	}
}
