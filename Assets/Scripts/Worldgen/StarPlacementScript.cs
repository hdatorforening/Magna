using UnityEngine;
using System.Collections;

using Galaxy;

namespace Galaxy {
	public class StarPlacementScript : MonoBehaviour {

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
}
