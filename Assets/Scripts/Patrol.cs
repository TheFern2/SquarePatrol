// Basic waypoint system

using UnityEngine;
using System.Collections;

public class Patrol : MonoBehaviour
{

	// Variables
	public Transform[] patrolPoints;
	public float moveSpeed;

	private int currentPoint;
	private Transform patrolTf;

	// Use this for initialization
	void Start ()
	{
		patrolTf = GetComponent<Transform> ();
		patrolTf.position = patrolPoints [0].position;

		currentPoint = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// If patrol position equals current point, move to next point
		if (patrolTf.position == patrolPoints [currentPoint].position) {
			currentPoint++;
		}

		if (currentPoint >= patrolPoints.Length) {
			currentPoint = 0;
		}

		// Move patrol to next point
		patrolTf.position = Vector3.MoveTowards (patrolTf.position, patrolPoints [currentPoint].position, moveSpeed * Time.deltaTime);
	}
}
