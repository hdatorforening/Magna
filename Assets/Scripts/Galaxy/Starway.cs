using UnityEngine;
using System.Collections;

public class Starway {
	
	Vector3 start;
	public Vector3 Start { get { return start;} }

	Vector3 end;
	public Vector3 End { get { return end;} }

	Star starStart;
	public Star StarStart { get { return starStart;} }

	Star starEnd;
	public Star StarEnd { get { return starEnd;} }

	int id;
	public int Id { get { return id;} }

	public GameObject gameObject;

	public Starway (int id, Vector3 start, Vector3 end, Star starStart, Star starEnd){
		this.id = id;
		this.start = start;
		this.end = end;

		this.starStart = starStart;
		this.starEnd = starEnd;
	}
}	

