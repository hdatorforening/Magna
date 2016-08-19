using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using gameSettings;
using globalVariables;

public class MouseController : MonoBehaviour{

	bool mouseDebug = false;


	Vector3 currFramePosition;
	Vector3 tmpFramePosition;
	Vector3 lastFramePosition;

	Galaxy galaxy = GlobalVariables.galaxy; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		currFramePosition = Camera.main.ScreenToWorldPoint ( Input.mousePosition );

		//Flytta kameran
		if (Input.GetMouseButton (1)) { //Right Mouse Button
			Camera.main.transform.Translate (lastFramePosition - currFramePosition);
		}


		//Markera object
		if (Input.GetMouseButtonDown (0)){
			Interact ();
		}


		//Camera Zoom
		if (Input.GetAxis ("Mouse ScrollWheel") != 0) {
			Zoom ();
		}

		lastFramePosition = Camera.main.ScreenToWorldPoint ( Input.mousePosition );

	}

	void Zoom(){
		GameSettings.cameraSizeGalaxy -= Input.GetAxis ("Mouse ScrollWheel") * GameSettings.scrollSpeed * GameSettings.cameraSizeGalaxy;
		Camera.main.orthographicSize = GameSettings.cameraSizeGalaxy;

		tmpFramePosition = Camera.main.ScreenToWorldPoint ( Input.mousePosition );

		Camera.main.transform.Translate (currFramePosition - tmpFramePosition);
	}

	void Interact(){

		InteractGalaxy ();

	}

	void InteractGalaxy(){
		//Profiler.BeginSample ("Interact()");

		Vector3 galaxyMousePos = currFramePosition;
		galaxyMousePos.z = 0;
		Galaxy galaxy = GlobalVariables.galaxy; 
		Sector sector = galaxy.GetSectorFromPos(galaxyMousePos);
		//Debug.Log (sector.X + " : " + sector.Y);
		if (sector != null) {
			foreach (Star star in sector.starList) {
				if (Vector3.Distance (galaxyMousePos, star.position) < gameSettings.GameSettings.StarClickBox) { //TODO StarClickBox < StarDistance / 2.
					
				}
			}
		}
			
		if (mouseDebug) {
			int x = Mathf.CeilToInt ((galaxyMousePos.x - 5) / gameSettings.GameSettings.sectorSize);
			int y =	Mathf.CeilToInt ((galaxyMousePos.y - 5) / gameSettings.GameSettings.sectorSize);
			galaxy.GetSector (x, y);
		}

		//Profiler.EndSample();
	}

}
