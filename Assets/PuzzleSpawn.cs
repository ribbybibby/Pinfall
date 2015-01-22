using UnityEngine;
using System.Collections;

public class PuzzleSpawn : MonoBehaviour {

	public GameObject firstPuzzle;
	public GameObject[] puzzles;
	public GameObject player;
	public float spawnDistanceFromPlayer;
	public float distanceBetweenPuzzles;

	// Don't touch in Unity
	public float puzzlePos;

	private int randompuzzle;
	private float firstPuzzleX;
	private float playerPos;

	// Use this for initialization
	void Start () {
		/*Debug.Log ("Puzzle0:" + puzzles [0].name);
		Debug.Log ("Puzzle1:" + puzzles [1].name);
		Debug.Log ("Length:" + puzzles.Length);*/
		playerPos = player.transform.position.y;
		firstPuzzleX = firstPuzzle.transform.position.x;
		puzzlePos = firstPuzzle.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if ((playerPos - player.transform.position.y) > spawnDistanceFromPlayer)
		{
			randompuzzle = RndRange();
			Debug.Log("Random number:" + randompuzzle);
			puzzlePos = puzzlePos - distanceBetweenPuzzles;
			playerPos = player.transform.position.y;
			Instantiate(puzzles[randompuzzle], new Vector3(firstPuzzleX, puzzlePos, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
		}

	}

	public int RndRange ()
	{
		int value = Random.Range (0, puzzles.Length);
		return value;
	}
}
