using UnityEngine;
using System.Collections;

public class RunningAnimationManager : MonoBehaviour {
	// Set in Unity Editor
	public KeyCode UpKey; // Up
	public KeyCode LeftKey; // Left
	public KeyCode DownKey; // Down
	public KeyCode RightKey; // Right
	
	public Sprite resting;
	private SpriteRenderer sprtrndr;
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator> ();
		sprtrndr = gameObject.GetComponent<SpriteRenderer> ();
		//anim.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (LeftKey) || Input.GetKey (RightKey))
		{
			anim.enabled = true;
		}
		else
		{
			anim.enabled = false;
			sprtrndr.sprite = resting;
		}
	}
}
