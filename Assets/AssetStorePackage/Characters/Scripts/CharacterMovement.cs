using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
	public float Speed = 3f;
	public float Jump = 8f;
	public bool IsPlaying = true;

	private Animator animator;
	private bool pointingRight = true;
	private bool isJumping;

	// Use this for initialization
	void Start ()
	{
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!IsPlaying)
		{
			animator.SetFloat ("Speed", 0);
			return;
		}
		
		float horizontal = Input.GetAxis ("Horizontal");
		if (Input.GetKey (KeyCode.Space) && !isJumping)
		{
			isJumping = true;
			animator.SetBool ("IsJumping", true);
			GetComponent<Rigidbody2D> ().AddForce (Vector2.up * Jump, ForceMode2D.Impulse);
		}
		
		if ((horizontal < 0) && (pointingRight))
		{
			transform.rotation = Quaternion.Euler (0, 180, 0);
			pointingRight = false;
		}
		else if ((horizontal > 0) && (!pointingRight))
		{
			transform.rotation = Quaternion.Euler (0, 0, 0);
			pointingRight = true;
		}

		animator.SetFloat ("Speed", Mathf.Abs(horizontal));
		if (horizontal != 0)
		{
			transform.Translate (Vector2.right * Speed * Time.deltaTime);
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		isJumping = false;
		animator.SetBool ("IsJumping", false);
	}
}