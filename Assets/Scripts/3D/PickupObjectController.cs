using UnityEngine;
using System.Collections;

public class PickupObjectController : MonoBehaviour
{

	//called when another object collides with this one, and the rigidbody is set to 'trigger' mode.
	void OnTriggerEnter (Collider collider)
	{
		Debug.Log ("Triggah bitch!");

		if (collider.tag == "Player") {
			collider.gameObject.GetComponent<PlayerController> ().increaseScore ();
			enabled = false;
		}
	}
}
