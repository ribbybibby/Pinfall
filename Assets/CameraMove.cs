using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	//Set in Unity
	public float downSpeed;
	public float distanceBeforeMove;
	public float chaseDistance;
	public float cameraChaseDistance;
	public GameObject player;
	public GameObject chaser;


	// Just for me
	private float playerDistance;
	private float distanceBetween;
	private Vector3 downVector;
	private Vector3 downVectorSlow;
	private float positiveDistance;
	private float distanceBetweenChaser;
	private float chaserDistanceCamera;

	// Use this for initialization
	void Start () {
		gameObject.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, gameObject.transform.position.z);
		Physics2D.IgnoreLayerCollision (8, 14, true);
		Physics2D.IgnoreLayerCollision (9, 13, true);
		Physics2D.IgnoreLayerCollision (9, 12);
		Physics2D.IgnoreLayerCollision (10, 12, true);
		Physics2D.IgnoreLayerCollision (12, 12, true);
		Physics2D.IgnoreLayerCollision (12, 14, true);

	}
	
	// Update is called once per frame
	void Update () {
		distanceBetween = player.transform.position.y - gameObject.transform.position.y;
		positiveDistance = Vector2.Distance (player.transform.position, gameObject.transform.position);
		downVector = new Vector3 (0, downSpeed/*+(positiveDistance/1.5f)*/, 0);
		downVectorSlow = new Vector3 (0, downSpeed, 0);
		distanceBetweenChaser = chaser.transform.position.y - player.transform.position.y;
		chaserDistanceCamera = chaser.transform.position.y - gameObject.transform.position.y;


		if (distanceBetweenChaser < chaseDistance && chaserDistanceCamera < cameraChaseDistance)
		{
			gameObject.transform.position -= downVectorSlow*Time.deltaTime;
		}

		if (distanceBetween < -distanceBeforeMove)
		{
			gameObject.transform.position -= downVector*Time.deltaTime;
		}
		else if (distanceBetween > distanceBeforeMove)
		{
			gameObject.transform.position += downVector*Time.deltaTime;
		}
		if (distanceBetween < 5)
		{
			gameObject.transform.position -= (downVector)*Time.deltaTime;
		}

		//playerDistance = Vector2.Distance(gameObject.transform.position

	}
}
