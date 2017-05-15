using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

	public GameManager manager;
	//variables
	public float moveSpeed;
	public float maxSpeed;
	public float courseSpeed;
	public GameObject deathParticles;
	private Vector3 input;
	private Rigidbody playerRb;
	private Vector3 spawnPoint;
	public AudioClip[] gameEffects;

	protected bool paused;

	// Use this for initialization
	void Start ()
	{
		playerRb = GetComponent<Rigidbody> ();
		spawnPoint = playerRb.position;
		manager = manager.GetComponent<GameManager> ();
	}

	void FixedUpdate ()
	{
		if (!paused) {
			input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
			
			if (playerRb.velocity.magnitude < maxSpeed) {
				if (Input.GetKey ("space")) {
					playerRb.AddRelativeForce (input * courseSpeed);
				} else
					playerRb.AddRelativeForce (input * moveSpeed);
			}
			
			// Check if player falls below
			if (playerRb.position.y < -3.0) {
				Die ();
			}
		}

	}

	void OnCollisionEnter (Collision other)
	{
		if (other.transform.tag == "Enemy") {

			Die ();
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.transform.tag == "Goal") {
			PlaySound (2);
			manager.CompleteLevel ();
			Destroy (gameObject);
		}

		if (other.transform.tag == "Enemy") {
			Die ();
		}

		if (other.transform.tag == "Extender") {
			PlaySound (1);
			manager.extendTime = 0.3f;
			Destroy (other.gameObject);
			Invoke ("EndTimeExtension", 0.7f);
		}

		if (other.transform.tag == "Token") {
			PlaySound (0);
			manager.AddToken ();
			Destroy (other.gameObject);
		}

	}

	void PlaySound (string soundName)
	{
		// Array order - Coin pickup, timeExtender, Win, explosion(Player death), gameLoop
		AudioSource audio = GetComponent<AudioSource> ();

		string defaultSound = "backgroundMusic";

		switch (soundName) {

		case soundName == "gameOver":
			audio.clip = gameEffects [2];
			audio.Play ();
			break;
		case soundName == "gameWin":
			audio.clip = gameEffects [1];
			audio.Play ();
			break;
		default:
			audio.clip = gameEffects [0];
			audio.Play ();
			break;
		}
			
	}

	void PlaySound (int index)
	{
		// Array order - Coin pickup, timeExtender, Win, explosion(Player death), gameLoop
		AudioSource audio = GetComponent<AudioSource> ();
		audio.clip = gameEffects [index];
		audio.Play ();
	}

	void EndTimeExtension ()
	{
		manager.extendTime = 0.0f;
	}

	void Die ()
	{
		PlaySound (3);
		Instantiate (deathParticles, playerRb.position, Quaternion.identity);
		playerRb.position = spawnPoint;
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
