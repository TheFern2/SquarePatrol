using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
	public GUISkin skin;

	void OnGUI ()
	{
		GUI.skin = skin;

		GUI.Label (new Rect (10, 10, 200, 200), "Square Patrol");

		if (PlayerPrefs.GetInt ("Current Level") > 1) {
			if (GUI.Button (new Rect (10, 55, 200, 50), "Continue")) {
				Application.LoadLevel (PlayerPrefs.GetInt ("Current Level"));

			}
		}

		if (GUI.Button (new Rect (10, 120, 200, 50), "Start New Game")) {
			Application.LoadLevel (1);
			PlayerPrefs.SetInt ("Current Level", 1);
			PlayerPrefs.SetInt ("Win", 0);
			
		}

		if (GUI.Button (new Rect (10, 185, 200, 50), "Quit")) {
			Application.Quit ();
			
		}

	}

}
