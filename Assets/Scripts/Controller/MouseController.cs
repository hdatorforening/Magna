using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class MouseController : MonoBehaviour {

	Vector3 lastFramePosition;

	GameSettings gameSettings = new GameSettings();


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 currFramePosition = Camera.main.ScreenToWorldPoint ( Input.mousePosition );

		if (Input.GetMouseButton (1)) { //Right Mouse Button

			Vector3 diff = lastFramePosition - currFramePosition;
			Camera.main.transform.Translate (diff);

		}

		lastFramePosition = Camera.main.ScreenToWorldPoint ( Input.mousePosition );

		gameSettings.cameraDistance += Input.GetAxis("Mouse ScrollWheel") * gameSettings.scrollSpeed;
		gameSettings.cameraDistance = Mathf.Clamp(gameSettings.cameraDistance, gameSettings.cameraDistanceMin, gameSettings.cameraDistanceMax);

	}
}
