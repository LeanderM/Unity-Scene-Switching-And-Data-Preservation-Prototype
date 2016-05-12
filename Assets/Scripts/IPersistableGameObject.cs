using System.Collections;
using System.Collections.Generic;
using System;

/**
 * Interface for objects that contain data that persists when a new scene is loaded
 */
public interface IPersistableGameObject
{
	//Stuff to do before switching can begin. may be empty
	void PrepareToSwitchScene ();

	//retrieve all data that has to be stored
	Dictionary<String, String> getPersistentData ();

	//what to do when new data has been loaded.
	void OnLoad ();
}
