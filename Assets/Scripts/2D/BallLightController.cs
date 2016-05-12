using UnityEngine;
using System.Collections;
using System;
using LitJson;
using System.Collections.Generic;

/**
 * Controlls the light that moves with the ball, and is disableable from the user interface
 */
public class BallLightController : MonoBehaviour, IPersistableGameObject
{
	private GameController controller;

	void Start ()
	{
		controller = GameController.GetGameController ();
		controller.registerObserver (this);
	}

	public void switchLight ()
	{
		GetComponent<Light> ().enabled = !IsEnabled ();
	}

	public bool IsEnabled ()
	{
		return GetComponent<Light> ().enabled;
	}

	public void PrepareToSwitchScene ()
	{
		//interface requires it, but there's nothing to do here
	}

	//return the values that have to be saved
	public Dictionary<String, String> getPersistentData ()
	{
		Dictionary<String, String> data = new Dictionary<String, String> ();
		data.Add ("BallLightController.Light.ActiveAndEnabled", IsEnabled ().ToString ());

		return data;
	}

	//check if the required data exists and load it if it does
	public void OnLoad ()
	{
		GameController controller = GameObject.Find ("GameController").GetComponent<GameController> ();

		String loadedData = controller.GetData () ["BallLightController.Light.ActiveAndEnabled"];
		GetComponent<Light> ().enabled = bool.Parse (loadedData);
	}
}
