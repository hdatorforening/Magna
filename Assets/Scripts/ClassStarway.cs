using UnityEngine;
using System.Collections;

namespace Galaxy {
	public class Starway : MonoBehaviour {
		LineRenderer starway; // = new GameObject().AddComponent<LineRenderer> ();
		Vector3 startPoint;
		Vector3 endPoint;

		public Starway (LineRenderer line, Vector3 start, Vector3 end){
			starway = line;
			startPoint = start;
			endPoint = end;
		}
	}	
}
