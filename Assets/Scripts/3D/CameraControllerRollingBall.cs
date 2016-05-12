using UnityEngine;
using System.Collections;

public class CameraControllerRollingBall : MonoBehaviour
{

	private Vector3 offset;
	private Vector3 velocity;

	public GameObject player;
	public float cameraMovementSmoothing = 0.3F;

	// Use this for initialization
	void Start ()
	{
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		Vector3 targetPosition = player.transform.position + offset;
		transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref velocity, cameraMovementSmoothing);
	}
}
