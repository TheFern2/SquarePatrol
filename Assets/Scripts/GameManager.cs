using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	// Put todo for the game here!
	// TODO Destroy player when getting to the goal, but without particles
	// TODO Stars for scoring
	// TODO Fix time and level loading when paused

	// Count
	public  int currentScore;
	public  int highScore;
	public int tokenCount;
	private int totalTokenCount;
	private  int currentLevel;
	private  int lastLevel = 4;
	public  int unlockedLevel;

	// GUI Skin
	public GUISkin skin;
	public Rect rectTimer;
	public Color warningTimer;
	public Color defaultTimer;

	// Time
	public float startTime;
	public float extendTimeInput;
	public  float extendTime = 0.0f;
	private  bool isTimeExtended;
	public string currentTime;

	// References
	public GameObject tokenParent;
	// public AudioClip gameLoopClip;

	// GUI stuff
	private bool showStatsScreen = false;
	private bool showPauseScreen = false;
	public int statsScreenWidth, statsScreenHeight;

	void Update ()
	{
		if (!showStatsScreen) {
			if (!showPauseScreen) {
				startTime -= Time.deltaTime;
			}

			startTime = startTime + extendTime;
			currentTime = string.Format ("{0:0.0}", startTime);
			
			// If time is done destroy game manager, and load menu
			if (startTime <= 0) {
				startTime = 0;
				CleanUp ();
			
			}

			// TODO Add logic to pause game if time is not done - Use escape key
			// Need to create a new GUI Pause Box to show when game is paused
			if ((startTime > 0.0) && Input.GetKey (KeyCode.Escape)) {
				showPauseScreen = true;
			}
		}

	}

	void Start ()
	{
		currentLevel = PlayerPrefs.GetInt ("Current Level");

		totalTokenCount = tokenParent.transform.childCount;

		// print ("Total token count is " + totalTokenCount);

	}

	public void CompleteLevel ()
	{
		showStatsScreen = true;
	}

	void LoadNextLevel ()
	{
		if (currentLevel >= lastLevel) {
			currentLevel = 0;
			Application.LoadLevel (0);
			
			PlayerPrefs.SetInt ("Current Level", currentLevel);
			print ("You've won the game! Cheers");
			
			PlayerPrefs.SetInt ("Win", 1);
			
			Debug.Log ("Next Level");
			
		}
		
		if (currentLevel < lastLevel && PlayerPrefs.GetInt ("Win") == 0) {
			currentLevel++;
			
			PlayerPrefs.SetInt ("Current Level", currentLevel);
			PlayerPrefs.SetInt ("Level" + currentLevel.ToString () + " score", currentScore);
			
			
			Application.LoadLevel (currentLevel);
			
		}
	}

	void SaveGame ()
	{

	}

	public void CleanUp ()
	{
		Application.LoadLevel (0);
	}

	public void AddToken ()
	{
		tokenCount++;
	}

	private int CheckLoadedLevel ()
	{
		int currentLevel = 0;

		for (int i = 0; i < lastLevel; i++) {
			if (Application.loadedLevel == i) {
				currentLevel = i;
				break;
			}
		
		}
		return currentLevel;

	}

	void OnGUI ()
	{
		GUI.skin = skin;

		if (startTime < 5.0f) {
			skin.GetStyle ("Timer").normal.textColor = warningTimer;
		} else {
			skin.GetStyle ("Timer").normal.textColor = defaultTimer;
		}

		GUI.Label (rectTimer, currentTime, skin.GetStyle ("Timer"));
		GUI.Label (new Rect (30, 50, 200, 200), tokenCount.ToString () + "/" + totalTokenCount.ToString ());


		// Show Status Screen
		if (showStatsScreen) {
			Rect statsScreenRec = new Rect (Screen.width / 2 - (Screen.width * 0.5f / 2),
			                                Screen.height / 2 - (Screen.height * 0.5f / 2),
			                                Screen.width * 0.5f, Screen.height * 0.5f);
			 
			GUI.Box (statsScreenRec, "Stats");

			// int gameTime = (int)startTime;
			currentScore = tokenCount * (int)startTime;

			if (GUI.Button (new Rect (statsScreenRec.x + statsScreenRec.width - 170, 
			                         statsScreenRec.y + statsScreenRec.height - 60, 150, 40), "Continue")) {
				LoadNextLevel ();
			}

			if (GUI.Button (new Rect (statsScreenRec.x + 20, 
			                          statsScreenRec.y + statsScreenRec.height - 60, 100, 40), "Quit")) {
				Application.LoadLevel (0);
			}

			GUI.Label (new Rect (statsScreenRec.x + 20, statsScreenRec.y + 40, 300, 50), "Score " + currentScore.ToString ());
			GUI.Label (new Rect (statsScreenRec.x + 20, statsScreenRec.y + 75, 300, 50), "Current Level  " + currentLevel);

		}

		// Show Pause Screen
		if (showPauseScreen) {
			Rect pauseScreenRec = new Rect (Screen.width / 2 - (Screen.width * 0.5f / 2),
			                                Screen.height / 2 - (Screen.height * 0.5f / 2),
			                                Screen.width * 0.5f, Screen.height * 0.5f);
			
			GUI.Box (pauseScreenRec, "Stats");
			
			// int gameTime = (int)startTime;
			currentScore = tokenCount * (int)startTime;
			
			if (GUI.Button (new Rect (pauseScreenRec.x + pauseScreenRec.width - 170, 
			                          pauseScreenRec.y + pauseScreenRec.height - 60, 150, 40), "Continue")) {

				Application.LoadLevel (CheckLoadedLevel ());
			}
			
			if (GUI.Button (new Rect (pauseScreenRec.x + 20, 
			                          pauseScreenRec.y + pauseScreenRec.height - 60, 100, 40), "Quit")) {
				Application.LoadLevel (0);
			}
			
			GUI.Label (new Rect (pauseScreenRec.x + 20, pauseScreenRec.y + 40, 300, 50), "Score " + currentScore.ToString ());
			GUI.Label (new Rect (pauseScreenRec.x + 20, pauseScreenRec.y + 75, 300, 50), "Current Level  " + currentLevel);
			
		}
	}
}
