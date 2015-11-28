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

	public GameObject[] Characters;
	public GameObject Player;
	public Transform PlayerStartPosition;
	public Transform Goal; // used to point camera to goal, so we can see where it is

	void Start ()
	{
		UpdateUI ();

		// only enable the current active player
		foreach (var character in Characters)
		{
			// set event handlers for our character
			var player = character.GetComponent<CodingPiratesCharacter> ();
			player.Died += PlayerDied;
			player.LevelCompleted += PlayerLevelCompleted;
		}

		Camera.main.transform.position = new Vector3(Goal.position.x, Goal.position.y, Camera.main.transform.position.z);
	}

	private void StartGame()
	{
		// only enable the current active player
		foreach (var p in Characters)
		{
			p.SetActive (p == Player);
		}

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
		(sender as MonoBehaviour).gameObject.SetActive (false);
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
			// Player.gameObject.SetActive (false);
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

	public void SelectPlayer(GameObject player)
	{
		this.Player = player;
		StartGame ();
	}

	public void Restart()
	{
		State = GameState.NotStarted;
		UpdateUI ();
	}
}
