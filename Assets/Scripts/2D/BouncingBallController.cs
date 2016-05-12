using UnityEngine;
using System.Collections;

public class BouncingBallController : MonoBehaviour
{

	//what to do when hitting something
	void OnCollisionEnter ()
	{
		GetComponent<Rigidbody> ().AddForce (new Vector3 (0.0f, 1000.0f, 0.0f));
	}
}
