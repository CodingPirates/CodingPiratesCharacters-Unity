using UnityEngine;
using System.Collections;

public class OutsideCollider : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.collider.tag == "Player")
		{
			CodingPiratesCharacter character = coll.collider.gameObject.GetComponent<CodingPiratesCharacter> ();
			if (!character)
				return;

			character.Die ();
		}
	}
}
