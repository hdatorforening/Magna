using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using gameSettings;

public class MouseController : MonoBehaviour {

	Vector3 currFramePosition;
	Vector3 tmpFramePosition;
	Vector3 lastFramePosition;


	//GameSettings gameSettings = new GameSettings();


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		currFramePosition = Camera.main.ScreenToWorldPoint ( Input.mousePosition );

		if (Input.GetMouseButton (1)) { //Right Mouse Button

			Camera.main.transform.Translate (lastFramePosition - currFramePosition);

		}


		//Camera Zoom

		if (Input.GetAxis ("Mouse ScrollWheel") != 0) {
			GameSettings.cameraSizeGalaxy -= Input.GetAxis ("Mouse ScrollWheel") * GameSettings.scrollSpeed * GameSettings.cameraSizeGalaxy;
			Camera.main.orthographicSize = GameSettings.cameraSizeGalaxy;

			tmpFramePosition = Camera.main.ScreenToWorldPoint ( Input.mousePosition );

			Camera.main.transform.Translate (currFramePosition - tmpFramePosition);
		}



		//Camera.main.transform.Translate ( Camera.main.ViewportToScreenPoint ( Input.mousePosition ) * Input.GetAxis("Mouse ScrollWheel") * GameSettings.scrollToMouseSpeed );
		//Debug.Log (Camera.main.transform.position);
		//Debug.Log (GameSettings.cameraSizeGalaxy);
		//gameSettings.cameraDistance = Mathf.Clamp(gameSettings.cameraDistance, gameSettings.cameraDistanceMin, gameSettings.cameraDistanceMax);

		lastFramePosition = Camera.main.ScreenToWorldPoint ( Input.mousePosition );

	}
}
