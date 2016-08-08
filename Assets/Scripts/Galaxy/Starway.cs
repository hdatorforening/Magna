using UnityEngine;
using System.Collections;

public class Starway {
	
	Vector3 start;
	public Vector3 Start { get { return start;} }

	Vector3 end;
	public Vector3 End { get { return end;} }

	int id;
	public int Id { get { return id;} }

	public Starway (int id, Vector3 start, Vector3 end){
		this.id = id;
		this.start = start;
		this.end = end;
	}
}	

