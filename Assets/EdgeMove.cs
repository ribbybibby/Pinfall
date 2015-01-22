using UnityEngine;
using System.Collections;

public class EdgeMove : MonoBehaviour {
	//Set in Unity
	public float downSpeed;
	public float distanceBeforeMove;
	public GameObject player;
	
	
	// Just for me
	private float playerDistance;
	private float distanceBetween;
	private Vector3 downVector;
	private float positiveDistance;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		distanceBetween = player.transform.position.y - 15 - gameObject.transform.position.y;
		positiveDistance = Vector2.Distance (player.transform.position, gameObject.transform.position);
		downVector = new Vector3 (0, downSpeed+(positiveDistance/1.5f), 0);
		
		if (distanceBetween < -distanceBeforeMove) //&& positiveDistance > (distanceBeforeMove + 10))
		{
			gameObject.transform.position += -downVector*Time.deltaTime;
		}
		
		if (distanceBetween > distanceBeforeMove) //&& positiveDistance > (distanceBeforeMove + 10))
		{
			gameObject.transform.position += downVector*Time.deltaTime;
		}
		
		//playerDistance = Vector2.Distance(gameObject.transform.position
		
	}
}
