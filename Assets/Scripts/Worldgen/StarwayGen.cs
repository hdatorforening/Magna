using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class StarwayGen {

	Galaxy galaxy;
	Starway starway;
	int starwayID;
	int starwayLenght;
	Sector.directions dir;

	public List<int> StarwayCollision = new List<int>(); //List of collisions with active starway


	public void GenerateStarways (Galaxy galaxy ,int starwayLenght){
		

		//Setup
		this.galaxy = galaxy;
		this.starwayLenght = starwayLenght;

		starwayID = galaxy.starwayList.Count;

		//LineRenderer [] newStarway = new LineRenderer[];
		//LineRenderer newStarway = new LineRenderer;


		foreach(Sector sector in galaxy.sectorList){
			foreach (Star star in sector.starList){
				star.starwayGen = true;

				//Alla stjärnor i samma sektor
				foreach (Star destination in sector.starList) {

					StarwayCalculator (star, destination);
				}

				Debug.Log (sector.neighbours [0]);

				for (int i = 0; i < 8; i++) {

					sector.CheckNeighbours (i);

					int count = 0;
					if (sector.neighbours [i] != null) {
						foreach (var destination in sector.neighbours[i].starList) {
							StarwayCalculator (star, destination);
							count++;
							if (count > 1000)
								break;
						}
					}

				}
			}
		}

		StarwayCollision.Sort ();
		StarwayCollision.Reverse ();
		VogonConstructionFleet (1); //Ränsar ovälkomna starways.
		StarwayCollision.Clear();


	}


	void StarwayCalculator(Star star, Star destination){
		Vector3 lineStart, lineEnd;
		bool alreadyGenerated = destination.starwayGen;
		if ((star != destination) && !alreadyGenerated && (Vector2.Distance (star.position, destination.position)) < starwayLenght) {
			lineStart = star.position;
			lineEnd = destination.position;

			if (!CheckStarwayCollision (lineStart, lineEnd)) {
				galaxy.starwayList.Add (starway = new Starway (starwayID, lineStart, lineEnd));
				starwayID++;
			} else {

			}
		}
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

			ps2 = line2.Start;
			pe2 = line2.End;

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
