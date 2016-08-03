using UnityEngine;
using System.Collections;

namespace Galaxy {
	public class Starway : MonoBehaviour {
		GameObject starway.AddComponent<LineRenderer> ();
		Vector3 startPoint;
		Vector3 endPoint;

		public Starway (GameObject line, Vector3 start, Vector3 end){
			starway = line;
			startPoint = start;
			endPoint = end;
		}
	}	
}
