using UnityEngine;
using System.Collections;

public class SystemInterface : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject systemNameGO = new GameObject ("SystemNameGO");
		systemNameGO.transform.SetParent (this.transform);

		UnityEngine.UI.Text systemName = systemNameGO.AddComponent<UnityEngine.UI.Text> ();
		//systemName.Text = "Testing.";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
