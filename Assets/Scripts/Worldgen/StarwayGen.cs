using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class StarwayGen {

	Galaxy galaxy;
	Starway starway;

	public List<int> StarwayCollision = new List<int>(); //List of collisions with active starway

	public void GenerateStarways (Galaxy galaxy ,int starwayLenght){

		this.galaxy = galaxy;

		//LineRenderer [] newStarway = new LineRenderer[];
		//LineRenderer newStarway = new LineRenderer;
		bool alreadyGenerated;

		Vector3 lineStart, lineEnd;

		foreach(Star star in galaxy.starList){
			foreach (Star destination in galaxy.starList){
				star.starwayGen = true;
				alreadyGenerated = destination.starwayGen;

				if ((star != destination) && !alreadyGenerated && (Vector2.Distance(star.position, destination.position)) < starwayLenght ) {
					lineStart = star.position;
					lineEnd = destination.position;

					if (!CheckStarwayCollision (lineStart, lineEnd)) {
						galaxy.starwayList.Add (starway = new Starway (lineStart, lineEnd));
					} else {

					}
				}
			}
		}

		StarwayCollision.Sort ();
		StarwayCollision.Reverse ();
		VogonConstructionFleet (1); //Ränsar ovälkomna starways.
		StarwayCollision.Clear();

		foreach (Starway line in galaxy.starwayList) {
			DrawLine (line.StartPoint, line.EndPoint);
			Debug.Log ("Drawline");
		}
	}

	/*void SetupLine(Vector3 start, Vector3 end)
	{
		Starway newStarway = gameObject.AddComponent<Starway>();
		newStarway.startPoint = start;
		newStarway.endPoint = end;

		starwayList.Add (newStarway);
	}*/

	void DrawLine (Vector3 start, Vector3 end)
	{
		var newLine = new GameObject().AddComponent<LineRenderer> ();

		newLine.sortingLayerName = "OnTop";
		newLine.sortingOrder = 5;
		newLine.SetVertexCount(2);
		newLine.SetPosition(0, start);
		newLine.SetPosition(1, end);
		newLine.SetWidth(0.04f, 0.04f);
		newLine.useWorldSpace = true;
		newLine.SetColors (Color.white, Color.white);
	}

	bool CheckStarwayCollision(Vector3 start, Vector3 end){
		Vector3 ps1, pe1, ps2, pe2;

		int itemNumber = 0;

		float A1, B1, C1; //Line1 calculations

		float A2, B2, C2; //Line2 calculations

		ps1 = start;
		pe1 = end;

		// Get A,B,C of first line - points : ps1 to pe1
		A1 = pe1.y-ps1.y;
		B1 = ps1.x-pe1.x;
		C1 = A1*ps1.x+B1*ps1.y;

		//Debug.Log ("\n New Line");

		foreach (Starway line2 in galaxy.starwayList) {

			ps2 = line2.StartPoint;
			pe2 = line2.EndPoint;

			//Debug.Log (ps2 + " " + pe2);

			// Get A,B,C of second line - points : ps2 to pe2
			A2 = pe2.y-ps2.y;
			B2 = ps2.x-pe2.x;
			C2 = A2*ps2.x+B2*ps2.y;

			//Debug.Log(A1 +" "+ B1 +" "+ C1 +" | "+ A2 +" "+ B2 +" "+ C2);
			//Debug.Log ("1:Start:"+start+" End:"+end);

			// Get delta and check if the lines are parallel
			float delta = A1*B2 - A2*B1;
			if (delta != 0) {
				// now return the Vector2 intersection point
				Vector3 intersection; 
				intersection.x = (B2 * C1 - B1 * C2) / delta;
				intersection.y = (A1 * C2 - A2 * C1) / delta;
				intersection.z = 0;


				if ( !(intersection == start || intersection == end) ) {
					//Debug.Log ("Intersection? \n P1 "+ps1+", "+pe1+" : P2 "+ps2+ ", "+pe2);

					if (notRect (ps1, pe1, intersection)) {
						//Debug.Log ("Rect1");
						if (notRect (ps2, pe2, intersection)) {
							//Debug.Log ("Rect2");
							if (Vector3.Distance (ps1, pe1) < Vector3.Distance (ps2, pe2)) {
								//Debug.Log ("Shorter.");
								if (!StarwayCollision.Contains (itemNumber)) {
									StarwayCollision.Add (itemNumber);
								}
							} else {
								//Debug.Log ("Stop");
								return true;
							}
						}
					}
				}
			}
			itemNumber++;
		}

		return false;
	}

	/*Vector2 LineIntersectionPoint(Vector3 ps1, Vector3 pe1, Vector3 ps2, 
		Vector3 pe2)
	{
		// Get A,B,C of first line - points : ps1 to pe1
		float A1 = pe1.y-ps1.y;
		float B1 = ps1.x-pe1.x;
		float C1 = A1*ps1.x+B1*ps1.y;

		// Get A,B,C of second line - points : ps2 to pe2
		float A2 = pe2.y-ps2.y;
		float B2 = ps2.x-pe2.x;
		float C2 = A2*ps2.x+B2*ps2.y;

		// Get delta and check if the lines are parallel
		float delta = A1*B2 - A2*B1;
		if(delta == 0)
			throw new System.Exception("Lines are parallel");

		// now return the Vector2 intersection point
		return new Vector2(
			(B2*C1 - B1*C2)/delta,
			(A1*C2 - A2*C1)/delta
		);
	}*/

	bool notRect(Vector3 p1, Vector3 p2, Vector3 intersect){
		bool sect = false;

		float rectOffset = 0;

		if(p1.x < p2.x){
			if (((p1.x + rectOffset) < intersect.x) && (intersect.x < (p2.x - rectOffset))){
				sect = true;
				//Debug.Log ("NotRect.x.if");
			}
		}else{
			if (((p2.x + rectOffset) < intersect.x) && (intersect.x < (p1.x - rectOffset))){
				sect = true;
				//Debug.Log ("NotRect.x.else");
			}
		}

		if (sect) {
			if (p1.y < p2.y) {
				if (((p1.y + rectOffset) < intersect.y) && (intersect.y < (p2.y - rectOffset))) {
					//Debug.Log ("NotRect.y.if");
					return true;
				}
			} else {
				if (((p2.y + rectOffset) < intersect.y) && (intersect.y < (p1.y - rectOffset))) {
					//Debug.Log ("NotRect.y.else");
					return true;
				}
			}
		}
		//Debug.Log ("NotRect.False");
		return false;

	}

	void VogonConstructionFleet(int operation){
		if (operation == 1) { //Rensar lägre stående starways.
			foreach (int index in StarwayCollision) {
				galaxy.starwayList.RemoveAt (index);
			}
		} /*else if (operation == 2) { //Skjuter vilt med dekonstuktionslaser.
			int i;
			int u;
			for (i = 0; i++; i < starwayList.Count() ) {
				if (u = Random.value (0, 10) == 0) {
					Random.value (0, starwayList.Count () - 1);
				}
			}
		}*/
	}
}
