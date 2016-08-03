using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Galaxy;

public class WorldGenScript : MonoBehaviour {

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

		for (int i = 0; i < 5; i++) {
			x = Random.Range(-5.0f,5.0f);
			y = Random.Range(-5.0f,5.0f);
			z = 0;
			starList.Add (Instantiate (starPrefab, new Vector3 (x, y, z), quat));
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

					SetupLine (lineStart, lineEnd);
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

		var newStarway = new Starway (newLine, start, end);

		starwayList.Add (newStarway);
	}

	void ClearExessLines(){
		foreach (GameObject line1 in starwayList) {
			foreach (GameObject line2 in starList) {
				if (line1 != line2) {
					
				}
			}
		}
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
