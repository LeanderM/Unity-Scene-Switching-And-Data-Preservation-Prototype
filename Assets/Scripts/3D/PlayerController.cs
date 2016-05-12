using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	public float speed;

	private Rigidbody rb;
	private int score;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		score = 0;
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);
	}

	public void increaseScore ()
	{
		score++;
	}
}