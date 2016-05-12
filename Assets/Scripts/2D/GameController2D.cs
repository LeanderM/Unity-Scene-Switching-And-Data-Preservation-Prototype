using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

//Controlls the 2D scene's loading behaviour
public class GameController2D : MonoBehaviour
{

	private AsyncOperation asyncLoadedScene;

	public String sceneToLoad;
	public GameObject loadingSlider;

	private const int FLOATTOPERCENTAGE = 100;
	private Text loadingText;

	// Use this for initialization
	void Start ()
	{
		loadingText = loadingSlider.GetComponentInChildren<Text> ();
		StartCoroutine ("PreloadScene");
	}

	//tell the game to load the 3d scene ahead of time, but do not allow it to switch yet
	IEnumerator PreloadScene ()
	{
		Debug.LogWarning ("WARNING: starting async load! unity WILL CRASH if you stop playing before it is done!");
		asyncLoadedScene = SceneManager.LoadSceneAsync (sceneToLoad);
		asyncLoadedScene.allowSceneActivation = false;
		yield return asyncLoadedScene;
	}

	//go to the next scene
	public void ActivateScene ()
	{
		GameController.GetGameController ().SaveToMemory ();
		asyncLoadedScene.allowSceneActivation = true;
	}

	//check loading progress
	public void Update ()
	{
		//updates loadbar every 60 seconds. functions of time are usually preferred
		//over functions of framerate, as it differs on consoles and VR, but in this case
		//it doesn't really matter.
		if (Time.frameCount % 60 == 0 && asyncLoadedScene != null) {

			float progress = asyncLoadedScene.progress;

			if (progress == 0.9f) {
				Debug.Log ("Loading at 90%. The remaining 10 procent will be loaded on scene activation. Slider will report that loading is done.");
				loadingText.text = "Loading done!";
				loadingText.color = Color.green;
			} else {
				loadingText.text = "Loading " + (progress * FLOATTOPERCENTAGE) + "%";
			}

			loadingSlider.GetComponent<Slider> ().value = progress;
		}
	}

}
