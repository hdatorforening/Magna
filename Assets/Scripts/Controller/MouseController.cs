using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;

using gameSettings;
using globalVariables;

public class MouseController : MonoBehaviour{

	bool mouseDebug = false;
	//bool firstRun = true;

	Star hoverStar; //Stjärnan musen är över.

	Vector3 currFramePosition;
	Vector3 tmpFramePosition; //Used for zoom to mouse.
	Vector3 lastFramePosition;

	Vector3 galaxyMousePos;


	Galaxy galaxy; 
	Text text;

	// Use this for initialization
	void Start () {

		galaxy = globalVariables.GlobalVariables.galaxy; 

		text = globalVariables.UI.ui.transform.GetChild (0).GetChild (0).GetChild(0).GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {
		Profiler.BeginSample ("MouseController:Update()");

		currFramePosition = Camera.main.ScreenToWorldPoint ( Input.mousePosition );

		galaxyMousePos = currFramePosition;
		galaxyMousePos.z = 0;

		//Flytta kameran
		if (Input.GetMouseButton (1)) { //Right Mouse Button
			Camera.main.transform.Translate (lastFramePosition - currFramePosition);
		}




		//Markera object
		if (Input.GetMouseButtonDown (0)){

			if (globalVariables.UI.hoverStar != null) {
				globalVariables.UI.selectedStar = globalVariables.UI.hoverStar;
			} else {
				globalVariables.UI.selectedStar = null;
			}
			
			Interact ();

		}


		//Camera Zoom
		if (Input.GetAxis ("Mouse ScrollWheel") != 0) {
			Zoom ();
		}

		lastFramePosition = Camera.main.ScreenToWorldPoint ( Input.mousePosition );

		Profiler.EndSample ();
	}

	void Zoom(){
		GameSettings.cameraSizeGalaxy -= Input.GetAxis ("Mouse ScrollWheel") * GameSettings.scrollSpeed * GameSettings.cameraSizeGalaxy;

		if (GameSettings.cameraSizeGalaxy < GameSettings.cameraMaxZoomIn) {
			GameSettings.cameraSizeGalaxy = 1f;
		}

		Camera.main.orthographicSize = GameSettings.cameraSizeGalaxy;

		tmpFramePosition = Camera.main.ScreenToWorldPoint ( Input.mousePosition );

		Camera.main.transform.Translate (currFramePosition - tmpFramePosition);
	}

	void Interact(){

		InteractGalaxy ();

	}

	void InteractGalaxy(){
		//Profiler.BeginSample ("Interact()");


		if (mouseDebug) {
			int x = Mathf.CeilToInt ((galaxyMousePos.x - 5) / gameSettings.GameSettings.sectorSize);
			int y =	Mathf.CeilToInt ((galaxyMousePos.y - 5) / gameSettings.GameSettings.sectorSize);
			galaxy.GetSector (x, y);
		}

		//Profiler.EndSample();
	}

}
