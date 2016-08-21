using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using gameSettings;
using globalVariables;

public class MouseController : MonoBehaviour{

	bool mouseDebug = false;

	Sector hoverSector; //Sektorn musen är i.
	Star hoverStar; //Stjärnan musen är över.
	Star selectedStar = null;

	Vector3 currFramePosition;
	Vector3 tmpFramePosition; //Used for zoom to mouse.
	Vector3 lastFramePosition;

	Vector3 galaxyMousePos;


	Galaxy galaxy; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Profiler.BeginSample ("MouseController:Update()");

		galaxy = globalVariables.GlobalVariables.galaxy; 

		currFramePosition = Camera.main.ScreenToWorldPoint ( Input.mousePosition );

		galaxyMousePos = currFramePosition;
		galaxyMousePos.z = 0;

		//Flytta kameran
		if (Input.GetMouseButton (1)) { //Right Mouse Button
			Camera.main.transform.Translate (lastFramePosition - currFramePosition);
		}

		HoverStar ();
		if (selectedStar == null) {
			globalVariables.UI.greenSelectCircle.transform.position = hoverStar.position;
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

		Profiler.EndSample ();
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


		if (mouseDebug) {
			int x = Mathf.CeilToInt ((galaxyMousePos.x - 5) / gameSettings.GameSettings.sectorSize);
			int y =	Mathf.CeilToInt ((galaxyMousePos.y - 5) / gameSettings.GameSettings.sectorSize);
			galaxy.GetSector (x, y);
		}

		//Profiler.EndSample();
	}

	void HoverStar(){
		
		hoverSector = galaxy.GetSectorFromPos (galaxyMousePos);

		if (hoverSector != null) {
			foreach (Star star in hoverSector.starList) {
				if (Vector3.Distance (galaxyMousePos, star.position) < gameSettings.GameSettings.StarClickBox) { //TODO StarClickBox < StarDistance / 2.
					hoverStar = star;
					Debug.Log (hoverStar);
				}
			}
		} else {
			hoverStar = null;
		}
	}

}
