using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour
{
	public void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.collider.tag == "Player")
		{
			CodingPiratesCharacter character = coll.collider.GetComponent<CodingPiratesCharacter> ();
			if (!character)
				return;

			character.LevelComplete ();
		}
	}
}
