using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Director;

public class PickupController : MonoBehaviour
{

	private Vector3 rotation;
	public float rotationSpeed;

	private Transform pickup;

	// Use this for initialization
	void Start ()
	{
		pickup = transform.GetChild (0);
		rotation = new Vector3 (10.0f, 10.0f, 10.0f);
		StartCoroutine (playDelay ());
	}

	// Update is called once per frame
	void Update ()
	{
		pickup.transform.Rotate (rotation * (rotationSpeed * Time.deltaTime));
	}

	private IEnumerator playDelay ()
	{
		Animator animator = GetComponent<Animator> ();
		int timeToWait = (int)UnityEngine.Random.Range (0f, 3f);

		yield return new WaitForSeconds (timeToWait);
		Debug.Log ("OMG na return! wacht voor " + timeToWait + " seconden");
		animator.enabled = true;
		animator.Play ("BreathingPickup");
	}
}
