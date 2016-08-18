using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using gameSettings;
using Funktioner;
using starwayGen;

public class Sector {

	//GameSettings gameSettings;

	Galaxy galaxy;
	public Galaxy Galaxy { get { return galaxy;} }
	GameController gameController;

	int x;
	public int X { get { return x;} }

	int y;
	public int Y { get { return y;} }

	Vector3 position;
	public Vector3 Position { get { return position;} }
		
	public Sector[] neighbours = new Sector[8]; // 0 = Norr

	public List<Star> starList = new List<Star>();
	public List<Starway> starwayList = new List<Starway> ();

	public Sector (int x, int y, Galaxy galaxy) {
		//Debug.Log ("New Sector Generated.");

		this.galaxy = galaxy;
		galaxy.sectorList.Add (this);

		this.gameController = galaxy.gameController;

		float sectorSize = GameSettings.sectorSize;

		this.x = x;
		this.y = y;

		this.position = new Vector3 (x * sectorSize, y * sectorSize, 0);

		GenerateSector (Random.Range(2, 8) + Random.Range(2, 8) + Random.Range(2, 8));

		foreach (Star star in starList) {
			gameController.DrawStar (star);
		}

		foreach (Starway starway in starwayList) {
			gameController.DrawStarway (starway);
		}

	}

	public bool GenerateSector(int starsToGenerate = 10){
		Profiler.BeginSample ("GenerateSector()");

		//Debug.Log (starsToGenerate);
		float sectorSize = GameSettings.sectorSize;
		bool failed = false;
		int starCurrent = 0;
		//Vector3[] starStream = new Vector3[starsToGenerate];

		if (x == y && x == 0) {
			//starStream [starCurrent] = new Vector3(0, 0, 0);
			starList.Add (new Star (this, new Vector3(0f, 0f, 0)));
			//Debug.Log (starCurrent);
			starCurrent++;
		}
			
		int tries = 0;
		while ( starCurrent < starsToGenerate) {
			failed = false;

			Vector3 starPos = new Vector3 (
				Random.Range (-sectorSize / 2f, sectorSize / 2f),
				Random.Range (-sectorSize / 2f, sectorSize / 2f),
				0
			);

			starPos = starPos + position;
			//if (systemPlacementDebug) { Debug.Log (parentID); }

			//Vector3 sectorOffset; //Del av ev. optimering av intersector beräknignar.

			//Gammal stjärnplacering
			foreach (var starCheck in starList) {
				//Debug.Log (Vector3.Distance (starCheck.position, starPos));
				if (Vector3.Distance (starCheck.position, starPos) <= GameSettings.minStarDistance) {
					//Debug.Log("failed");
					failed = true;
					break;
				}
			}

			if (!failed) {
				for (int i = 0; i < 8; i++) {

					CheckNeighbours (i);

					int count = 0;
					if (neighbours [i] != null) {
						foreach (Star starCheck in neighbours[i].starList) {

							if (Vector3.Distance (starCheck.position, starPos) <= GameSettings.minStarDistance) {
								//Debug.Log("failed");
								failed = true;
								break;
							}

							count++;
							if (count > 1000) {
								Debug.LogError ("Game stuck in GenerateSector() loop");
								break;
							}
						}
					}
					if (failed) {
						break;
					}

				}
			}

			//Eventuell kod för att optimera intersector beräkningar.
			/*
			if (north) {
			} else if (south) {
			}
			if (east)


			NotRect.notRect (
				this.position - sectorOffset (GameSettings.sectorSize / 2, GameSettings.sectorSize / 2, 0),
				this.position + sectorOffset (GameSettings.sectorSize / 2, GameSettings.sectorSize / 2, 0),

			);
			*/



			if (!failed) {
				
				//starStream [starCurrent] = starPos;
				;
				starList.Add (new Star (this, starPos));
				//Debug.Log (starCurrent);
				starCurrent++;
				tries = 0;
			} else {
				tries++;
				//Debug.Log (starCurrent + " " + starPos);
				if (tries > 10) {
					starCurrent++;
					//Debug.Log (starCurrent);
					tries = 0;
				}
			}

		}

		starwayGen.StarwayGen.GenerateStarways (this);

		Profiler.EndSample ();
		return false;
	}

	public Sector CheckNeighbours(int i){
		//Debug.Log (i);
		//Debug.Log (neighbours [i]);
		if (neighbours [i] == null) {
			foreach (Sector sectorNeighbour in galaxy.sectorList) {
				if (i == 0) {
					if (sectorNeighbour.X == this.X && sectorNeighbour.Y == this.Y + 1) {
						neighbours [i] = sectorNeighbour;
						break;
					}
				}else if(i == 1){
					if (sectorNeighbour.X == this.X + 1 && sectorNeighbour.Y == this.Y + 1) {
						neighbours [i] = sectorNeighbour;
						break;
					}
				}else if(i == 2){
					if (sectorNeighbour.X == this.X + 1 && sectorNeighbour.Y == this.Y) {
						neighbours [i] = sectorNeighbour;
						break;
					}
				}else if(i == 3){
					if (sectorNeighbour.X == this.X + 1 && sectorNeighbour.Y == this.Y - 1) {
						neighbours [i] = sectorNeighbour;
						break;
					}
				}else if(i == 4){
					if (sectorNeighbour.X == this.X && sectorNeighbour.Y == this.Y - 1) {
						neighbours [i] = sectorNeighbour;
						break;
					}
				}else if(i == 5){
					if (sectorNeighbour.X == this.X - 1 && sectorNeighbour.Y == this.Y - 1) {
						neighbours [i] = sectorNeighbour;
						break;
					}
				}else if(i == 6){
					if (sectorNeighbour.X == this.X - 1 && sectorNeighbour.Y == this.Y) {
						neighbours [i] = sectorNeighbour;
						break;
					}
				}else if(i == 7){
					if (sectorNeighbour.X == this.X - 1 && sectorNeighbour.Y == this.Y + 1) {
						neighbours [i] = sectorNeighbour;
						break;
					}
				}

			}
		}

		return neighbours[i];
	}
}
