using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour
{
	public enum GameState
	{
		NotStarted,
		Playing,
		WonLevel,
	}

	public GameState State = GameState.NotStarted;

	public GameObject StartMenuUI;
	public GameObject LevelCompleteUI;

	public GameObject Player;
	public Transform PlayerStartPosition;
	public Transform Goal; // used to point camera to goal, so we can see where it is

	void Start ()
	{
		UpdateUI ();

		var player = Player.GetComponent<CodingPiratesCharacter> ();
		player.Died += PlayerDied;
		player.LevelCompleted += PlayerLevelCompleted;
		Camera.main.transform.position = new Vector3(Goal.position.x, Goal.position.y, Camera.main.transform.position.z);
	}

	void Update ()
	{
		if ((State == GameState.NotStarted || State == GameState.WonLevel) && Input.GetKeyDown (KeyCode.Space))
		{
			StartGame ();
		}
	}

	private void StartGame()
	{
		Camera.main.GetComponent<FollowCamera> ().target = Player.gameObject;
		Player.transform.position = PlayerStartPosition.position;
		State = GameState.Playing;
		UpdateUI ();
	}

	void PlayerLevelCompleted (object sender, System.EventArgs e)
	{
		State = GameState.WonLevel;
		UpdateUI ();
	}

	void PlayerDied (object sender, System.EventArgs e)
	{
		State = GameState.NotStarted;
		UpdateUI ();
	}

	public void UpdateUI()
	{
		switch (State)
		{
		case GameState.NotStarted:
			StartMenuUI.SetActive (true);
			LevelCompleteUI.SetActive (false);
			Player.gameObject.SetActive (false);
			break;

		case GameState.Playing:
			StartMenuUI.SetActive (false);
			LevelCompleteUI.SetActive (false);
			Player.gameObject.SetActive (true);
			Player.GetComponent<CharacterMovement> ().IsPlaying = true;
			break;

		case GameState.WonLevel:
			StartMenuUI.SetActive (false);
			LevelCompleteUI.SetActive (true);
			Player.gameObject.SetActive (true);
			Player.GetComponent<CharacterMovement> ().IsPlaying = false;
			break;
		}
	}
}
