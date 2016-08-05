using UnityEngine;
using System.Collections;

namespace Galaxy {
	public class Starway : MonoBehaviour {
		//public LineRenderer starway; // = new GameObject().AddComponent<LineRenderer> ();
		public Vector3 startPoint;
		public Vector3 endPoint;

		public Starway (/*LineRenderer line,*/ Vector3 start, Vector3 end){
			//starway = line;
			startPoint = start;
			endPoint = end;
		}
	}	
}
