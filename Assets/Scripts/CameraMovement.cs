using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
	public Vector3 mainCamPosition;
	private GameObject player;
	private GameObject mainCam;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindWithTag ("Player");
		mainCam = GameObject.FindWithTag ("MainCamera");
		//DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Camera position should be following the player
		// based on x -5 or so, y (same) of player position, z -15 or so
		mainCam.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y * mainCamPosition.y,
		                                         player.transform.position.z * mainCamPosition.z);

		if (player.transform.position.z > -4.5f) {
			mainCam.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y * mainCamPosition.y,
			                                          player.transform.position.z * mainCamPosition.z - 1.5f);
		}
	
	}
}
