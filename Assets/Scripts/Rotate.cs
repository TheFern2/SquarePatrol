using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
	public float rotateSpeed;
	protected bool paused;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (!paused) {
			// Rotate object on its Y axis
			gameObject.transform.Rotate (0.0f, gameObject.transform.position.y * rotateSpeed, 0.0f);
		}
	}

	void OnPauseGame ()
	{
		paused = true;
	}
	
	
	void OnResumeGame ()
	{
		paused = false;
	}
}
