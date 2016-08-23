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
		//UISystemText = ;
		//ui = 
	}

	// Update is called once per frame
	void Update () {

		if (selectedStar != globalVariables.UI.selectedStar) {
			Debug.Log ("UIControl.Update");
			selectedStar = globalVariables.UI.selectedStar;

			//this.UIsystem.GetComponent<GUIText> ().text = "" + selectedStar.id;
			if (selectedStar != null) {
				globalVariables.UI.ui.transform.GetChild (0).GetChild (0).GetChild (0).GetComponent<Text> ().text = "" + globalVariables.UI.hoverStar.id;

				foreach (Body body in selectedStar.bodyList) {
					DrawUiBody (body);
				}
			} else {
				globalVariables.UI.ui.transform.GetChild (0).GetChild (0).GetChild (0).GetComponent<Text> ().text = "";
			}
		}
	}

	void DrawUiBody(Body body) {
		Profiler.BeginSample ("DrawUiBody()");

		GameObject body_go;

		body_go = Instantiate(globalVariables.UI.systemUIBody);
		uiBodyList.Add (body_go);
		//body_go.AddComponent<StarController> ();
		//body_go.AddComponent<Collider2D> ();

		body_go.name = "Body";
		//body_go.transform.position = star.position;

		//body_go.GetComponent<StarController> ().star = star;

		/*body_go.transform.localScale = new Vector3 (
			GameSettings.starSize, 
			GameSettings.starSize, 
			GameSettings.starSize
		);*/

		body_go.transform.SetParent (globalVariables.UI.ui.transform.GetChild (0).GetChild (0).GetChild (1), true);

		//SpriteRenderer star_sr = star.go.AddComponent<SpriteRenderer> ();
		//star_sr.sprite = starSprite;
		//StarController star_control = 



		//Destroy (star_go, Time.deltaTime);
		Profiler.EndSample ();
	}
}
