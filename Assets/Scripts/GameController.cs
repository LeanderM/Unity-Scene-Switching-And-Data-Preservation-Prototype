using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
using System.Text;
using System.Runtime.InteropServices.ComTypes;

public class GameController : MonoBehaviour
{

	private static GameController controller;
	private String saveLocation;

	private Dictionary<String, String> data;
	private List<IPersistableGameObject> observers;

	// Use this for initialization, before Start
	void Awake ()
	{
		//singleton constructor
		if (controller == null) {
			DontDestroyOnLoad (gameObject);
			controller = this;
		} else if (controller != this) {
			Destroy (gameObject);
		}
	}

	//constructor
	void Start ()
	{
		data = new Dictionary<String, String> ();
		observers = new List<IPersistableGameObject> ();
		saveLocation = Application.persistentDataPath + "/save.json";
		LoadFromHardDrive ();

	}

	public static GameController GetGameController ()
	{
		return controller;
	}

	/**
	 * Adds an observer.
	 */
	public GameController registerObserver (IPersistableGameObject newObserver)
	{
		observers.Add (newObserver);
		return this;
	}

	/**
	 * Unregisters all current observers, for when a new scene gets loaded
	 */
	private void unregisterAllObservers ()
	{
		observers.Clear ();
	}

	/**
	 * Load saved data and notifies objects
	 */
	public void LoadFromHardDrive ()
	{
		if (File.Exists (saveLocation)) {
			String json = File.ReadAllText (saveLocation, Encoding.UTF8);
			data = JsonMapper.ToObject<Dictionary<String, String>> (json);

			GameObject[] dataTargets = GameObject.FindGameObjectsWithTag ("HasPersistentData");
			foreach (GameObject dataTarget in dataTargets) {
				dataTarget.GetComponent<IPersistableGameObject> ().OnLoad ();
			}
		} else {
			Debug.Log ("Nothing to load.");
		}
	}

	/**
	 * Save the session to the hard drive
	 */
	public void SaveToHardDrive ()
	{
		Debug.Log ("Saving to harddrive...");
		SaveToMemory ();

		String json = JsonMapper.ToJson (data);
		File.WriteAllText (saveLocation, json);
	}

	/**
	 *Save the session to memory (the data field
	 */
	public void SaveToMemory ()
	{
		Debug.Log ("Saving to memory...");

		data.Clear ();
		FetchDataToSave ();
	}

	/**
	 * Get all the data that has to be saved from the objects that are tagged as 'hasPersistentData'
	 */
	private void FetchDataToSave ()
	{
		Debug.Log ("Fetching data from observers list, of which there are " + observers.Count);

		foreach (IPersistableGameObject dataSource in observers) {
			//IPersistableGameObject script = dataSource.GetComponent<IPersistableGameObject> ();

			dataSource.PrepareToSwitchScene ();
			Dictionary<String, String> objData = dataSource.getPersistentData ();

			foreach (KeyValuePair<String, String> item in objData) {

				Debug.Log ("Saving value " + item.Value + " With key " + item.Key);
				data.Add (item.Key, item.Value);
			}
		}
	}

	public Dictionary<String, String> GetData ()
	{
		return data;
	}

}