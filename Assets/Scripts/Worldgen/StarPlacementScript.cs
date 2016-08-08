using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public class StarPlacementScript {

	bool systemPlacementDebug;

	Vector3[] starStream;
	int starCurrent;
	float maxStarDistance;
	float minStarDistance;

	WorldGenScript worldGenScript;
	GameController gameController;

	public void GenerateStarCluster(int numberOfSystems, Vector3 startLoc = default (Vector3)){

		systemPlacementDebug = true;

		if (systemPlacementDebug) { Debug.Log ("GenerateStarCluster"); }

		starStream = new Vector3[numberOfSystems];
		//worldGenScript = GetComponent<WorldGenScript> ();

		int starParent = 0;
		starCurrent = 0;

		int failedPlacementsLoop = 0;

		starStream [0] = startLoc;
		starCurrent++;

		while (starCurrent < numberOfSystems) {
			//if (systemPlacementDebug) { Debug.Log ("Number of Systems: " + numberOfSystems); }
			//int genPar = (int) ((starParent - 4) / 1.1f);
			if (starParent >= starCurrent){ 
				starParent = starCurrent / 2; 
				failedPlacementsLoop++;
			}

			GenerateStarLocation ((int) ((starParent - 4) / 1.1f));
			starParent++;
		}

		if (systemPlacementDebug) { Debug.Log ("GenerateStarPlacement"); }
		foreach (Vector3 pos in starStream) {
			GenerateStarPlacement (pos);
		}

		if (systemPlacementDebug) {
			Debug.Log ("Failed Placements Loops: " + failedPlacementsLoop);
		}

	}

	bool GenerateStarLocation(int parentID){

		maxStarDistance = 7;
		minStarDistance = 1;

		if (parentID < 0) {
			parentID = 0;
		}
		bool failed = false;

		for (int i = 0; i < 10; i++) {
			Vector3 pos = Random.insideUnitCircle * maxStarDistance;
			if (Vector3.Distance (new Vector3 (0, 0, 0), pos) > minStarDistance) {
				pos = pos + starStream [parentID];
				//if (systemPlacementDebug) { Debug.Log (parentID); }

				for (int n = 0; n < starCurrent; n++) {
					if (Vector3.Distance (starStream[n], pos) <= minStarDistance) {
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
		GameObject star_go = new GameObject ();
		star_go.name = "Star_" + starCurrent;
		star_go.transform.position = coords;

		SpriteRenderer star_sr = star_go.AddComponent<SpriteRenderer> ();
		star_sr.sprite = gameController.starSprite;
		//worldGenScript.starList.Add (Instantiate (worldGenScript.starPrefab, coords, Quaternion.identity));
	}
}

