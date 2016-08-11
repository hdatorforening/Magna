using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

using gameSettings;
using Funktioner;


namespace starwayGen{
	public static class StarwayGen {

		static int starwayID;
		static Sector.directions dir;

		static public List<Starway> StarwayCollision = new List<Starway>(); //List of collisions with active starway


		public static void GenerateStarways (Sector sector){
			Profiler.BeginSample ("GenerateStarways()");

			//starwayID = sector.starwayList.Count;

			//LineRenderer [] newStarway = new LineRenderer[];
			//LineRenderer newStarway = new LineRenderer;


			//foreach(Sector sector in galaxy.sectorList){
			foreach (Star star in sector.starList){
				//star.starwayGen = true;

				//Alla stjärnor i samma sektor
				foreach (Star destination in sector.starList) {
					if (star != destination) {
						StarwayCalculator (star, destination, sector);
					}
				}

				//Debug.Log (sector.neighbours [0]);

				for (int i = 0; i < 8; i++) {

					sector.CheckNeighbours (i);

					int count = 0;
					if (sector.neighbours [i] != null) {
						foreach (Star destination in sector.neighbours[i].starList) {
						StarwayCalculator (star, destination, sector);
							count++;
							if (count > 1000) {
								Debug.LogError ("Game stuck in StarwayGen loop");
								break;
							}
						}
					}

				}
			}
			//}

			//VogonConstructionFleet (1, sector); //Ränsar ovälkomna starways.
			//StarwayCollision.Clear();

			Profiler.EndSample ();
		}


		static void StarwayCalculator(Star star, Star destination, Sector sector){
			Profiler.BeginSample ("StarwayCalculator()");

			Vector3 lineStart, lineEnd;
			bool alreadyGenerated = false;
			foreach (Star starConnected in star.connectedStars) {
				if (starConnected == destination) {
					alreadyGenerated = true;
				}
			}

			if ((star != destination) && !alreadyGenerated && (Vector2.Distance (star.position, destination.position)) < GameSettings.starwayLenght) {
				lineStart = star.position;
				lineEnd = destination.position;

				if (!CheckStarwayCollision (lineStart, lineEnd, sector)) {
					sector.starwayList.Add (new Starway (starwayID, lineStart, lineEnd));
					star.connectedStars.Add (destination);
					destination.connectedStars.Add (star);
					starwayID++;
				} else {

				}
			}

			Profiler.EndSample ();
		}


		static bool CheckStarwayCollision(Vector3 start, Vector3 end, Sector sector){
			Profiler.BeginSample ("CheckStarwayCollision()");

			Vector3 ps1, pe1, ps2, pe2;

			float A1, B1, C1; //Line1 calculations

			float A2, B2, C2; //Line2 calculations

			ps1 = start;
			pe1 = end;

			// Get A,B,C of first line - points : ps1 to pe1
			A1 = pe1.y-ps1.y;
			B1 = ps1.x-pe1.x;
			C1 = A1*ps1.x+B1*ps1.y;

			//Debug.Log ("\n New Line");

			foreach (Starway line2 in sector.starwayList) {

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

						if (NotRect.notRect (ps1, pe1, intersection)) {
							//Debug.Log ("Rect1");
							if (NotRect.notRect (ps2, pe2, intersection)) {
								//Debug.Log ("Rect2");
								if (Vector3.Distance (ps1, pe1) < Vector3.Distance (ps2, pe2)) {
									//Debug.Log ("Shorter.");
									/*if (!StarwayCollision.Contains (line2)) {
										StarwayCollision.Add (line2);
									}*/
									sector.starwayList.Remove (line2);
								} else {
									//Debug.Log ("Stop");

									Profiler.EndSample ();
									return true;
								}
							}
						}
					}
				}
			}

			for (int i = 0; i < 8; i++) {
				if (sector.CheckNeighbours(i) != null) {
					foreach (Starway line2 in sector.neighbours[i].starwayList) {

						ps2 = line2.Start;
						pe2 = line2.End;

						//Debug.Log (ps2 + " " + pe2);

						// Get A,B,C of second line - points : ps2 to pe2
						A2 = pe2.y - ps2.y;
						B2 = ps2.x - pe2.x;
						C2 = A2 * ps2.x + B2 * ps2.y;

						//Debug.Log(A1 +" "+ B1 +" "+ C1 +" | "+ A2 +" "+ B2 +" "+ C2);
						//Debug.Log ("1:Start:"+start+" End:"+end);

						// Get delta and check if the lines are parallel
						float delta = A1 * B2 - A2 * B1;
						if (delta != 0) {
							// now return the Vector2 intersection point
							Vector3 intersection; 
							intersection.x = (B2 * C1 - B1 * C2) / delta;
							intersection.y = (A1 * C2 - A2 * C1) / delta;
							intersection.z = 0;


							if (!(intersection == start || intersection == end)) {
								//Debug.Log ("Intersection? \n P1 "+ps1+", "+pe1+" : P2 "+ps2+ ", "+pe2);

								if (NotRect.notRect (ps1, pe1, intersection)) {
									//Debug.Log ("Rect1");
									if (NotRect.notRect (ps2, pe2, intersection)) {
										//Debug.Log ("Rect2");
										if (Vector3.Distance (ps1, pe1) < Vector3.Distance (ps2, pe2)) {
											//Debug.Log ("Shorter.");
											/*if (!StarwayCollision.Contains (line2)) {
												StarwayCollision.Add (line2);
											}*/
											sector.starwayList.Remove (line2);
										} else {
											//Debug.Log ("Stop");

											Profiler.EndSample ();
											return true;
										}
									}
								}
							}
						}
					}
				}
			}

			Profiler.EndSample ();
			return false;
		}


		/*
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
		*/


		static void VogonConstructionFleet(int operation, Sector sector){
			Profiler.BeginSample ("VogonConstructionFleet()");

			if (operation == 1) { //Rensar lägre stående starways.
				foreach (Starway index in StarwayCollision) {
					sector.starwayList.Remove (index);

				}

			} 

			/*else if (operation == 2) { //Skjuter vilt med dekonstuktionslaser.
				int i;
				int u;
				for (i = 0; i++; i < starwayList.Count() ) {
					if (u = Random.value (0, 10) == 0) {
						Random.value (0, starwayList.Count () - 1);
					}
				}
			}*/

			Profiler.EndSample ();
		}
	}
}
