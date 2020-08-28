using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// This is the main script attached to the GameObject Gamecontroller

public class gameScript : ManagerBase {

	// startposition of the game
	Vector3 startPosition;
	Vector3 actualPosition;
	int myRandom;

	// References to the prefabs
	public GameObject tile;
	public GameObject plane;
	public GameObject sphere;

	// references to the ui panels
	public GameObject menuPanel;
	public GameObject rulePanel;
	public GameObject exitPanel;
	public GameObject gameOverPanel;

	// score display on the ui
	public Text scoreText;
	public Text scoreText1;
	public Text hiScoreText1;

	public Text menuBestScore;
	public Text menuGamesPlayed;

	float timer;
	float timerInterval = 0.3f;

	int score;

	public Image fader;
	
	// Game may be in various states
	public enum GameState {
		fadeblackout,
		menu, 
		game,
		over,
		exit,
	};

	public GameState gameState;

	float fadeTimer;
	float fadeInterval;
	
	Color col;

	Color camColor;
	Color camColorBlack;
	float camLerpTimer;
	float camLerpInterval = 1f;
	bool camColorLerp = false;
	int direction = 1;

	Color[] tileMatDay;
	Color tileMatNight;
	int actTileColor;

	public Light dayLight;
	public Material tileMat;

	float daytimeTimer;
	float dayTimeInterval = 15f;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake() {

		camColor = new Color (0.75f, 0.75f, 0.75f);
		camColorBlack = new Color (0, 0, 0);

		actTileColor = 0;
		tileMatDay = new Color[3];
		tileMatDay[0] = new Color (10/256f, 139/256f, 203/256f);
		tileMatDay[1] = new Color (10/256f, 200/256f, 20/256f);
		tileMatDay[2] = new Color (220/256f, 170/256f, 45/256f);
		tileMatNight = new Color (0, 8/256f, 11/256f);
		tileMat.color = tileMatDay [0];

		// set up the game elements
		setupGame ();
		// set the game state
		gameState = GameState.fadeblackout;
		// hide all panels except for menu panel
		menuPanel.SetActive (true);
		rulePanel.SetActive (false);
		exitPanel.SetActive (false);
		gameOverPanel.SetActive (false);
	}

	// Use this for initialization
	void Start () {
		// set the hiscore value
		if (!PlayerPrefs.HasKey ("HiScore"))
			PlayerPrefs.SetInt ("HiScore", 0);
		if (!PlayerPrefs.HasKey ("GamesPlayed"))
			PlayerPrefs.SetInt ("GamesPlayed", 0);
	}


	// Update is called once per frame
	void Update () {
		// Escape key for android smartphones
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (gameState == GameState.game)
				Time.timeScale = 0;
			gameState = GameState.exit;
			exitGame();
		}
		switch (gameState) {
		case GameState.fadeblackout:
			col = fader.color;
			col.a -= 0.01f;
			if (col.a <= 0) {
				col.a = 0;
				fader.gameObject.SetActive(false);
				gameState = GameState.menu;
			}
			fader.color = col;
			break;

		case GameState.menu:
			break;

		case GameState.game:
			// generate further tiles teh player may roll on
			timer += Time.deltaTime;
			if (timer >= timerInterval) {
				timer -= timerInterval;
				buildTile ();
			}

			daytimeTimer += Time.deltaTime;
			if (daytimeTimer > dayTimeInterval) {
				daytimeTimer -= daytimeTimer;
				camColorLerp = true;
				camLerpTimer = 0;
			}

			if (camColorLerp) {
				camLerpTimer += Time.deltaTime;
				float percent = camLerpTimer / camLerpInterval;
				if (direction == 1) {
					Camera.main.backgroundColor = Color.Lerp(camColor,camColorBlack,percent);
					tileMat.color = Color.Lerp(tileMatDay[actTileColor], tileMatNight,percent);
					dayLight.intensity = 1-percent;
				}
				else {
					Camera.main.backgroundColor = Color.Lerp(camColorBlack,camColor,percent);
					tileMat.color = Color.Lerp(tileMatNight, tileMatDay[actTileColor],percent);
					dayLight.intensity = percent;
				}
				if (percent > 0.98f) {
					camLerpTimer = 1;
					direction *= -1;
					camColorLerp = false;
					if (direction == -1)
						actTileColor = Random.Range(0,tileMatDay.Length);
				}
			}

			break;
		case GameState.over:
			break;

		}
	}

	/// <summary>
	/// Setups the game.
	/// </summary>
	void setupGame() {
		// clesr the score
		score = 0;
		// set the ui
		scoreText.text = score.ToString ("D5");
		scoreText1.text = score.ToString ("D5");

		menuBestScore.text = "BEST SCORE: " + PlayerPrefs.GetInt ("HiScore").ToString();
		menuGamesPlayed.text = "GAMES PLAYED: " + PlayerPrefs.GetInt ("GamesPlayed").ToString();

		// clean up old tile from potential previous game
		GameObject[] tiles = GameObject.FindGameObjectsWithTag ("Tile");
		foreach (GameObject t in tiles)
			Destroy (t);
		// generate new starting plane
		GameObject planeGO = Instantiate (plane, new Vector3 (0, -2, 0), Quaternion.identity) as GameObject;
		planeGO.name = "Plane";
		// generate the player object
		GameObject sphereGO = Instantiate (sphere, new Vector3 (0, -0.1f, 0), Quaternion.Euler(45,63,23)) as GameObject;
		sphereGO.name = "Sphere";

		// set the camera object
		Camera.main.transform.position = new Vector3 (5, 5, -5);
		Camera.main.GetComponent<cameraScript> ().setup ();

		// set the starting position for the blue tiles
		startPosition = new Vector3(-2,-2,3);
		GameObject newTile=Instantiate(tile,startPosition,Quaternion.identity) as GameObject;
		newTile.name = "Tile";
		actualPosition = startPosition;
		// now generate 20 tile in advance
		for (int i = 0; i < 20; i++) {
			buildTile ();
		}
	}

	/// <summary>
	/// Builds the tile.
	/// </summary>
	void buildTile() {
		Vector3 newPosition = actualPosition; 
		myRandom = Random.Range(0,101);
		// with a probability of 50% set the new tile left or right
		if (myRandom < 50) {
			newPosition.x -= 1;
		}
		else {
			newPosition.z += 1;
		}
		actualPosition = newPosition;
		GameObject newTile=Instantiate(tile,actualPosition,Quaternion.identity) as GameObject;
		newTile.name = "Tile";
		newTile.tag = "Tile";
	}

	// for other instances
	// check, if game is still in game mode
	public bool inGame() {
		if (gameState == GameState.game)
			return true;
		else
			return false;
	}

	// add points to the player score
	public void addScore(int amount) {
		score += amount;
		scoreText.text = score.ToString ("D5");
		scoreText1.text = score.ToString ("D5");
	}

	// player lost the game
	// display the gameover panel
	// check the score and the actual hiScore
	public void gameOver() {
		gameState = GameState.over;
		gameOverPanel.SetActive (true);
		int hiScore = PlayerPrefs.GetInt ("HiScore");
		if (score > hiScore)
			PlayerPrefs.SetInt ("HiScore", score);

		scoreText1.text = score.ToString ("D5");
		hiScore = PlayerPrefs.GetInt ("HiScore");
		hiScoreText1.text = hiScore.ToString ("D5");

		int gamesPlayed = PlayerPrefs.GetInt ("GamesPlayed");
		gamesPlayed++;
		PlayerPrefs.SetInt ("GamesPlayed", gamesPlayed);
	}


	/// <summary>
	/// Now following the ui button events.
	/// </summary>

	public void playGame() {
		menuPanel.SetActive (false);
		gameState = GameState.game;
		soundManager.PlayMusicGame ();
	}

	public void showRules() {
		menuPanel.SetActive (false);
		rulePanel.SetActive (true);
	}

	public void exitGame() {
		menuPanel.SetActive (false);
		gameOverPanel.SetActive (false);
		exitPanel.SetActive (true);
	}


	public void exitRules() {
		menuPanel.SetActive (true);
		rulePanel.SetActive (false); 
		gameState = GameState.menu;
	}

	public void resumeGame() {
		exitPanel.SetActive (false);
		if (Time.timeScale == 0) {
			Time.timeScale = 1;
			gameState = GameState.game;
		} 
		else {
			if (GameObject.Find("Plane") == null)
				setupGame();
			menuPanel.SetActive (true);
			gameState = GameState.menu;
		}
	}

	public void retryGame() {
		setupGame ();
		menuPanel.SetActive (true);
		gameOverPanel.SetActive (false);
		gameState = GameState.menu;
		soundManager.PlayMenuMusic ();
	}

	public void quitGame() {
		Application.Quit ();
	}
}
