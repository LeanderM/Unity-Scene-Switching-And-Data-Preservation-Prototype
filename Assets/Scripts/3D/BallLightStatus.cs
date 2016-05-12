using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

/**
 * Load the status from the ball's light from the previous scene, and display it in the GUI
 */
public class BallLightStatus : MonoBehaviour, IPersistableGameObject
{
	private GameController controller;
	private bool lightStatus;

	// Use this for initialization
	void Start ()
	{
		controller = GameController.GetGameController ();
		controller.registerObserver (this);
		OnLoad ();

	}

	public void PrepareToSwitchScene ()
	{
		//doesn't have to do anything, really.
	}

	public Dictionary<String, String> getPersistentData ()
	{
		Dictionary<String, String> data = new Dictionary<String, String> ();
		data.Add ("BallLightController.Light.ActiveAndEnabled", lightStatus.ToString ());

		return data;
	}

	public void OnLoad ()
	{
		lightStatus = bool.Parse (controller.GetData () ["BallLightController.Light.ActiveAndEnabled"]);
		GetComponent<Text> ().text = "The ball light in 2D was " + lightStatus;
	}
}
