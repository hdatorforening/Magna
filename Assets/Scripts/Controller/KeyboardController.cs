using UnityEngine;
using System.Collections;

using gameSettings;
using keyLayout;

public class KeyboardController : MonoBehaviour{

	// Update is called once per frame
	void Update () {


		if (Input.anyKey) {

			//Camera control ----------------------------------------------------------------------------------
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

			//Camera control - Alternative --------------------------------------------------------------------
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


			//Interface ---------------------------------------------------------------------------------------
			if (Input.GetKeyDown(keyLayout.KeyLayout.exit)){
				globalVariables.GlobalVariables.MainMenu.SetActive( !globalVariables.GlobalVariables.MainMenu.activeSelf ? true : false );
				Debug.Log ("Exit");
			}


			//Extra ------------------------------------
			if (Input.GetKeyDown(keyLayout.KeyLayout.ctrl)){
				Debug.Log ("Ctrl");
			}
			if (Input.GetKeyDown(keyLayout.KeyLayout.alt)){
				Debug.Log ("Alt");
			}
			if (Input.GetKeyDown(keyLayout.KeyLayout.altGr)){
				Debug.Log ("AltGr");
			}

			if (Input.anyKeyDown) {
				Debug.Log (Input.anyKeyDown);
			}
		}

	}

}
