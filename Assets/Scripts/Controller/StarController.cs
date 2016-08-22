using UnityEngine;
using System.Collections;

using globalVariables;
using gameSettings;

public class StarController : MonoBehaviour {

	Star Star;

	void Start(){
		CircleCollider2D collider = GetComponentInParent<CircleCollider2D> ();
		collider.radius = gameSettings.UI.StarClickBox;
	}

	void OnMouseEnter(){
		globalVariables.UI.greenSelectCircle.transform.position = this.transform.position;
		globalVariables.UI.greenSelectCircle.SetActive (true);
		//globalVariables.UI.hoverStar = this.GetComponentInChildren<Star>();
		//Debug.Log ("StarController.Enter");
		//Debug.Log (globalVariables.UI.hoverStar);
	}

	void OnMouseExit(){
		globalVariables.UI.greenSelectCircle.SetActive (false);
		//Debug.Log ("StarController.Exit");
	}

}
