using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;

using globalVariables;

public class UIController : MonoBehaviour {

	Star selectedStar;

	Text UISystemText;

	List<GameObject> uiBodyList = new List<GameObject> ();

	GameObject ui;

	void Start(){
		for (int i = 0; i < 20; i++) {
			PrepareUiBody ();
		}
	}

	int bodyListCount;

	// Update is called once per frame
	void Update () {

		if (selectedStar != globalVariables.UI.selectedStar) {
			Debug.Log ("UIControl.Update");

			for (int i = 0; i < bodyListCount; i++){
				uiBodyList [i].SetActive (false);
			}

			selectedStar = globalVariables.UI.selectedStar;

			//this.UIsystem.GetComponent<GUIText> ().text = "" + selectedStar.id;
			if (selectedStar != null) {
				bodyListCount = globalVariables.UI.hoverStar.bodyList.Count;

				globalVariables.UI.ui.transform.GetChild (0).GetChild (0).GetChild (0).GetComponent<Text> ().text = "" + globalVariables.UI.hoverStar.id + " : " + bodyListCount;

				for (int i = 0; i < bodyListCount; i++){
					uiBodyList [i].SetActive (true);
				}

			} else {
				globalVariables.UI.ui.transform.GetChild (0).GetChild (0).GetChild (0).GetComponent<Text> ().text = "";

				/*for (int i = 0; i < bodyListCount; i++){
					uiBodyList [i].SetActive (false);
				}*/

			}
		}
	}

	void PrepareUiBody() {
		Profiler.BeginSample ("PrepareUiBody()");

		GameObject body_go = (GameObject)Instantiate(globalVariables.UI.systemUIBody);

		body_go.transform.SetParent (globalVariables.UI.ui.transform.GetChild (0).GetChild (0).GetChild (1), false);
		body_go.transform.localScale = new Vector3(1,1,1);
		body_go.name = "Body";
		body_go.SetActive (false);
		body_go.layer = 5;
		//body_go.GetComponents<SpriteRenderer>().
		//body_go.transform.GetChild(0).transform = body_go.transform.localScale;
		//body_go.AddComponent<TextMesh> ().text = "Heil Hitler!";
		//body_go.GetComponent<TextMesh> ().fontSize = 300;
		//body_go.GetComponent<TextMesh> ().alignment

		uiBodyList.Add (body_go);

		//Destroy (star_go, Time.deltaTime);
		Profiler.EndSample ();
	}
}
