using UnityEngine;
using System.Collections;

using gameSettings;
using keyLayout;

public class KeyboardController : MonoBehaviour {

	// Update is called once per frame
	void Update () {

		//Camera control
		if (Input.anyKey) {

			//WASD
			if (Input.GetKey(keyLayout.KeyLayout.cameraMoveUp)){
				Camera.main.transform.Translate ( 
					new Vector3(0, +1, 0) * GameSettings.KeyboardScrollSpeed * GameSettings.cameraSizeGalaxy );
			}

			if (Input.GetKey(keyLayout.KeyLayout.cameraMoveDown)){
				Camera.main.transform.Translate ( 
					new Vector3(0, -1, 0) * GameSettings.KeyboardScrollSpeed * GameSettings.cameraSizeGalaxy );
			}

			if (Input.GetKey(keyLayout.KeyLayout.cameraMoveRight)){
				Camera.main.transform.Translate ( 
					new Vector3(+1, 0, 0) * GameSettings.KeyboardScrollSpeed * GameSettings.cameraSizeGalaxy );
			}

			if (Input.GetKey(keyLayout.KeyLayout.cameraMoveLeft)){
				Camera.main.transform.Translate ( 
					new Vector3(-1, 0, 0) * GameSettings.KeyboardScrollSpeed * GameSettings.cameraSizeGalaxy );
			}

			//Arrow keys
			if (Input.GetKey(keyLayout.KeyLayout.cameraMoveUp2)){
				Camera.main.transform.Translate ( 
					new Vector3(0, +1, 0) * GameSettings.KeyboardScrollSpeed * GameSettings.cameraSizeGalaxy );
			}

			if (Input.GetKey(keyLayout.KeyLayout.cameraMoveDown2)){
				Camera.main.transform.Translate ( 
					new Vector3(0, -1, 0) * GameSettings.KeyboardScrollSpeed * GameSettings.cameraSizeGalaxy );
			}

			if (Input.GetKey(keyLayout.KeyLayout.cameraMoveRight2)){
				Camera.main.transform.Translate ( 
					new Vector3(+1, 0, 0) * GameSettings.KeyboardScrollSpeed * GameSettings.cameraSizeGalaxy );
			}

			if (Input.GetKey(keyLayout.KeyLayout.cameraMoveLeft2)){
				Camera.main.transform.Translate ( 
					new Vector3(-1, 0, 0) * GameSettings.KeyboardScrollSpeed * GameSettings.cameraSizeGalaxy );
			}
				

		}

	}

}
