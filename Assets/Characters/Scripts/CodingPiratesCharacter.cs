using System;
using UnityEngine;
using System.Collections;

public class CodingPiratesCharacter : MonoBehaviour
{
	public event EventHandler Died;
	public event EventHandler LevelCompleted;

	public void Die()
	{
		if (Died != null)
		{
			Died (this, null); // raise event so listeners can catch this
		}
	}

	public void LevelComplete()
	{
		if (LevelCompleted != null)
		{
			LevelCompleted (this, null);
		}
	}
}
