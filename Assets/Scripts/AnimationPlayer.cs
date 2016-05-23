using UnityEngine;
using System.Collections;

public class AnimationPlayer : MonoBehaviour
{

	// HACK Need to come up with a way for animations to start at different times
	// Using simple InvokeRepeating!

	public float startDelay;
	public float delayTime;

	public bool playAnimation;
	
	// Use this for initialization
	void Start ()
	{
		InvokeRepeating ("PlayAnimation", startDelay, delayTime);
	}

	void PlayAnimation ()
	{
		GetComponent<Animation> ().Play ();
	}

}