using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour
{

	public float delayTime;

	// Use this for initialization
	void Start ()
	{

		StartCoroutine (Go ());
	
	}
	
	IEnumerator Go ()
	{
		while (true) {
			GetComponent<Animation> ().Play ();
			yield return new WaitForSeconds (delayTime);
		}
	}
}
