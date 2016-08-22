using UnityEngine;
using System.Collections;

using globalVariables;

public class StarController : MonoBehaviour {

	void OnMouseEnter(){
		//globalVariables.UI.greenSelectCircle.transform.position = this.transform.position;
		Debug.Log ("StarController.Enter");
	}

	void OnMouseExit(){
		Debug.Log ("StarController.Exit");
	}

}
