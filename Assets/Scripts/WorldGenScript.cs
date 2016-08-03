using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Galaxy;

public class WorldGenScript : MonoBehaviour {

	bool speltest;


	public GameObject starPrefab;
	public GameObject starwayPrefab;
	public Material lineMaterial;

	public List<Object> starList = new List<Object>(); //List of all existing stars
	public List<Starway> starwayList = new List<Starway>(); //List of all existing starways

	Quaternion quat;

	Vector3 lineStart;
	Vector3 lineEnd;


	// Use this for initialization
	void Start () {
		speltest = true;
		if (speltest) {
			Debug.Log ("Debugging mode");
		}
		int numberOfStars = 5;

		GenerateStars (numberOfStars);
		GenerateStarWays ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GenerateStars (int n){
		float x;
		float y;
		float z;

		if (!speltest) {
			for (int i = 0; i < n; i++) {
				x = Random.Range (-5.0f, 5.0f);
				y = Random.Range (-5.0f, 5.0f);
				z = 0;
				starList.Add (Instantiate (starPrefab, new Vector3 (x, y, z), quat));
			}
		} else {
			starList.Add (Instantiate (starPrefab, new Vector3 (-2, -2, 0), quat));
			starList.Add (Instantiate (starPrefab, new Vector3 (-2, 2, 0), quat));
			starList.Add (Instantiate (starPrefab, new Vector3 (2, -2, 0), quat));
			starList.Add (Instantiate (starPrefab, new Vector3 (2, 2, 0), quat));
			starList.Add (Instantiate (starPrefab, new Vector3 (3, 0, 0), quat));
		}

		//Vector2.Distance();
		return;
	}

	void GenerateStarWays (){
		//LineRenderer [] newStarway = new LineRenderer[];
		//LineRenderer newStarway = new LineRenderer;
		bool alreadyGenerated;


		foreach(GameObject star in starList){
			foreach (GameObject destination in starList){
				star.GetComponent<StarScript> ().starwayGen = true;
				alreadyGenerated = destination.GetComponent<StarScript>().starwayGen;

				if ((star != destination) && !alreadyGenerated && (Vector2.Distance(star.transform.position, destination.transform.position)) < 10 ) {
					lineStart = star.transform.position;
					lineEnd = destination.transform.position;

					if (!CheckStarwayCollision (lineStart, lineEnd)) {
						SetupLine (lineStart, lineEnd);
					}
				}
			}
		}
	}

	void SetupLine(Vector3 start, Vector3 end)
	{
		var newLine = new GameObject().AddComponent<LineRenderer> ();


		newLine.sortingLayerName = "OnTop";
		newLine.sortingOrder = 5;
		newLine.SetVertexCount(2);
		newLine.SetPosition(0, start);
		newLine.SetPosition(1, end);
		newLine.SetWidth(0.03f, 0.03f);
		newLine.useWorldSpace = true;
		newLine.material = lineMaterial;

		//Starway newStarway = new Starway (newLine, start, end);

		Starway newStarway = gameObject.AddComponent<Starway>();
		newStarway.startPoint = start;
		newStarway.endPoint = end;
		newStarway.starway = newLine;

		starwayList.Add (newStarway);
	}

	bool CheckStarwayCollision(Vector3 start, Vector3 end){
		Vector3 ps1, pe1, ps2, pe2;

		Rect rect = new Rect (0, 0, 0, 0);

		float A1;
		float B1;
		float C1;

		float A2;
		float B2;
		float C2;

		ps1 = start;
		pe1 = end;

		//Vector2 s2d = new Vector2 (start.x, start.y);
		//Vector2 e2d = new Vector2 (end.x, end.y);

		//Debug.Log ("\n New Line");

		foreach (Starway line2 in starwayList) {

			ps2 = line2.startPoint;
			pe2 = line2.endPoint;

			//Debug.Log (ps2 + " " + pe2);


			// Get A,B,C of first line - points : ps1 to pe1
			A1 = pe1.y-ps1.y;
			B1 = ps1.x-pe1.x;
			C1 = A1*ps1.x+B1*ps1.y;

			// Get A,B,C of second line - points : ps2 to pe2
			A2 = pe2.y-ps2.y;
			B2 = ps2.x-pe2.x;
			C2 = A2*ps2.x+B2*ps2.y;

			//Debug.Log(A1 +" "+ B1 +" "+ C1 +" | "+ A2 +" "+ B2 +" "+ C2);
			Debug.Log ("1:Start:"+start+" End:"+end);

			// Get delta and check if the lines are parallel
			float delta = A1*B2 - A2*B1;
			if (delta != 0) {
				// now return the Vector2 intersection point
				Vector3 intersection; 
				intersection.x = (B2 * C1 - B1 * C2) / delta;
				intersection.y = (A1 * C2 - A2 * C1) / delta;
				intersection.z = 0;

				if ( !(intersection == start || intersection == end) ) {
					//Debug.Log (s2d +" : "+ intersection +" : "+ e2d);

					rect.xMin = ps1.x;
					rect.yMin = ps1.y;
					rect.xMax = pe1.x;
					rect.yMax = pe1.y;

					Debug.Log ("2:Rect:"+rect +" Iners:"+ intersection);

					if (rect.Contains (intersection, true)) {

						Debug.Log ("Yay!");

						rect.xMin = ps2.x;
						rect.yMin = ps2.y;
						rect.xMax = pe2.x;
						rect.yMax = pe2.y;

						//Debug.Log (rect);

						if (rect.Contains (intersection)) {
							Debug.Log ("Starway denied!");
							return true;
						}
					}
				}
			}
		}

		return false;
	}

	Vector2 LineIntersectionPoint(Vector3 ps1, Vector3 pe1, Vector3 ps2, 
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
	}
}
