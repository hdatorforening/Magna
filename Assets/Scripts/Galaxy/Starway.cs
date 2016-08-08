using UnityEngine;
using System.Collections;

public class Starway {
	//public LineRenderer starway; // = new GameObject().AddComponent<LineRenderer> ();
	Vector3 startPoint;

	public Vector3 StartPoint {
		get {
			return startPoint;
		}
	}

	Vector3 endPoint;

	public Vector3 EndPoint {
		get {
			return endPoint;
		}
	}

	public Starway (/*LineRenderer line,*/ Vector3 start, Vector3 end){
		//starway = line;
		startPoint = start;
		endPoint = end;
	}
}	

