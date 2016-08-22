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

	Sector hoverSector; //Sektorn musen är i.
	Star hoverStar; //Stjärnan musen är över.
	Star selectedStar = null;

	Vector3 currFramePosition;
	Vector3 tmpFramePosition; //Used for zoom to mouse.
	Vector3 lastFramePosition;

	Vector3 galaxyMousePos;


	Galaxy galaxy; 
	Text text;

	// Use this for initialization
	void Start () {
		text = globalVariables.UI.ui.transform.GetChild (0).GetChild (0).GetChild(0).GetComponent<Text>();
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


		/*if (hoverStar != null) {
			if (Vector3.Distance (galaxyMousePos, hoverStar.position) > gameSettings.GameSettings.StarClickBox / 2) {
				HoverStar ();
				if (hoverStar != null) {
					if (selectedStar == null) {
						globalVariables.UI.greenSelectCircle.transform.position = hoverStar.position;

						text.text = hoverStar.id.ToString ();
						globalVariables.UI.greenSelectCircle.SetActive (true);
					}
		} else if (selectedStar == null) {
					globalVariables.UI.greenSelectCircle.SetActive (false);
				}
			}
		} else {
			HoverStar ();
			//globalVariables.UI.greenSelectCircle.SetActive (false);
		}*/


		//Markera object
		if (Input.GetMouseButtonDown (0)){
			if (hoverStar != null) {
				selectedStar = hoverStar;
			}

			if (selectedStar != null) {
				if (Vector3.Distance (galaxyMousePos, selectedStar.position) > gameSettings.GameSettings.StarClickBox) {
					selectedStar = null;
				}
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

		if (galaxy != null) {
			hoverSector = galaxy.GetSectorFromPos (galaxyMousePos);
		}

		if (hoverSector != null) {
			foreach (Star star in hoverSector.starList) {
				if (Vector3.Distance (galaxyMousePos, star.position) < gameSettings.GameSettings.StarClickBox) { //TODO StarClickBox < StarDistance / 2.
					hoverStar = star;
					break;
				} else {
					hoverStar = null;
				}
			}
		} else {
			hoverStar = null;
		}
	}

}
