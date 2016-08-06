using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Galaxy;

public class WorldGenScript : MonoBehaviour {
	//Debugstuff
	bool speltestStars;
	bool speltestStarway;
	public GameObject intdebugstar;
	public Material testmaterial;

	//Imports
	public StarPlacementScript scriptStarPlace;

	//Proper stuff
	public float worldSize;

	public GameObject starPrefab;
	public Material lineMaterial;

	//Public lists
	public List<Object> starList = new List<Object>(); //List of all existing stars
	public List<Starway> starwayList = new List<Starway>(); //List of all existing starways
	public List<int> StarwayCollision = new List<int>(); //List of collisions with active starway

	Quaternion quat;

	Vector3 lineStart;
	Vector3 lineEnd;


	// Use this for initialization
	void Start () {
		speltestStars = false;
		speltestStarway = false;
		if (speltestStars || speltestStarway) {
			Debug.Log ("Debugging mode");
		}
		int numberOfStars = 100;
		int starwayLenght = 4;
		worldSize = 10f;
		Camera.main.orthographicSize = worldSize;

		GenerateStars (numberOfStars);
		GenerateStarWays (starwayLenght);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GenerateStars (int n){
		/*float x;
		float y;
		float z;*/

		if (!speltestStars) {
			//scriptStarPlace = scriptStarPlace.GetComponent<StarPlacementScript> ().GenerateStarCluster(n);
			//scriptStarPlace.GenerateStarCluster (n /*, new Vector3(0, 0, 0)*/);
			GenerateStarCluster(n);
			/*for (int i = 0; i < n; i++) {
				x = Random.Range (-worldSize, worldSize);
				y = Random.Range (-worldSize, worldSize);
				z = 0;
				starList.Add (Instantiate (starPrefab, new Vector3 (x, y, z), quat));

			}*/
		} else {
			starList.Add (Instantiate (starPrefab, new Vector3 (-2, -2, 0), quat));
			starList.Add (Instantiate (starPrefab, new Vector3 (-4, 5, 0), quat));
			starList.Add (Instantiate (starPrefab, new Vector3 (3, -3, 0), quat));
			starList.Add (Instantiate (starPrefab, new Vector3 (1, 3, 0), quat));
			starList.Add (Instantiate (starPrefab, new Vector3 (3, 0, 0), quat));
			starList.Add (Instantiate (starPrefab, new Vector3 (3, 3, 0), quat));
			starList.Add (Instantiate (starPrefab, new Vector3 (-1, 1, 0), quat));
			starList.Add (Instantiate (starPrefab, new Vector3 (-3, 2, 0), quat));
		}

		//Vector2.Distance();
		return;
	}

	void GenerateStarWays (int starwayLenght){
		//LineRenderer [] newStarway = new LineRenderer[];
		//LineRenderer newStarway = new LineRenderer;
		bool alreadyGenerated;


		foreach(GameObject star in starList){
			foreach (GameObject destination in starList){
				star.GetComponent<StarScript> ().starwayGen = true;
				alreadyGenerated = destination.GetComponent<StarScript>().starwayGen;

				if ((star != destination) && !alreadyGenerated && (Vector2.Distance(star.transform.position, destination.transform.position)) < starwayLenght ) {
					lineStart = star.transform.position;
					lineEnd = destination.transform.position;

					if (!CheckStarwayCollision (lineStart, lineEnd)) {
						SetupLine (lineStart, lineEnd);
					} else {
						
					}
				}
			}
		}
		StarwayCollision.Sort ();
		StarwayCollision.Reverse ();
		VogonConstructionFleet (1); //Ränsar ovälkomna starways.
		StarwayCollision.Clear();

		foreach (Starway line in starwayList) {
			DrawLine (line.startPoint, line.endPoint);
		}
	}

	void SetupLine(Vector3 start, Vector3 end)
	{
		Starway newStarway = gameObject.AddComponent<Starway>();
		newStarway.startPoint = start;
		newStarway.endPoint = end;

		starwayList.Add (newStarway);
	}

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
		newLine.material = lineMaterial;
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

		foreach (Starway line2 in starwayList) {

			ps2 = line2.startPoint;
			pe2 = line2.endPoint;

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

					if (speltestStarway){
						Instantiate (intdebugstar, intersection, quat);
					}

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
				starwayList.RemoveAt (index);
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


	//------------------------------------------------------------------------------------------------

	Vector3[] starStream;
	int starCurrent;

	WorldGenScript worldGen;

	public void GenerateStarCluster(int numberOfSystems, Vector3 startLoc = default (Vector3)){
		starStream = new Vector3[numberOfSystems];

		int starParent = 0;
		starCurrent = 0;

		starStream [0] = startLoc;
		starCurrent++;

		while (starCurrent < numberOfSystems) {
			GenerateStarLocation (starParent);
		}

		foreach (Vector3 pos in starStream) {
			GenerateStarPlacement (pos);
		}

	}
	bool GenerateStarLocation(int parentID){
		bool failed = false;

		worldGen = GetComponent<WorldGenScript> ();
		for (int i = 0; i < 5; i++) {
			Vector3 pos = Random.insideUnitCircle * 8.0f;
			if (Vector3.Distance (new Vector3 (0, 0, 0), pos) > 1) {
				pos += starStream [parentID];

				for (int n = 0; n < starCurrent; n++) {
					if (Vector3.Distance (starStream[n], pos) <= 1) {
						failed = true;
						break;
					}
				}

				if (!failed) {
					starStream [starCurrent] = pos;
					starCurrent++;
					return true;
				}
			}
		}
		return false;
	}

	void GenerateStarPlacement(Vector3 coords){
		worldGen.starList.Add (Instantiate (worldGen.starPrefab, coords, Quaternion.identity));
	}


}
