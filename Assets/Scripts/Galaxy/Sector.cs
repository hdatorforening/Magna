using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Sector {

	GameSettings gameSettings;

	Galaxy galaxy;

	int x;
	public int X { get { return x;} }

	int y;
	public int Y { get { return y;} }

	Vector3 position;
	public Vector3 Position { get { return position;} }

	public enum directions
	{
		north, ne, east, se, south, sw, west, nw 
	};

	public Sector[] neighbours = new Sector[8];

	/*Sector north, south, east, west, ne, nw, se, sw;
	public Sector North { get { return north;} set { north = value;} }
	public Sector South { get { return south;} set { south = value;} }
	public Sector East { get { return east;} set { east = value;} }
	public Sector West { get { return west;} set { west = value;} }
	public Sector Ne { get { return ne;} set { ne = value;} }
	public Sector Nw { get { return nw;} set { nw = value;} }
	public Sector Se { get { return se;} set { ne = value;} }
	public Sector Sw { get { return sw;} set { nw = value;} }*/


	public List<Star> starList = new List<Star>();

	public Sector (int x, int y, GameSettings gameSettings) {

		this.gameSettings = gameSettings;

		float sectorSize = gameSettings.sectorSize;

		this.x = x;
		this.y = y;

		this.position = new Vector3 (x * sectorSize, y * sectorSize, 0);

		GenerateSector (Random.Range(3, 15));

	}

	public bool GenerateSector(int starsToGenerate = 10){
		//Debug.Log (starsToGenerate);
		float sectorSize = gameSettings.sectorSize;
		bool failed = false;
		Star star;
		int starCurrent = 0;
		//Vector3[] starStream = new Vector3[starsToGenerate];

		if (x == y && x == 0) {
			//starStream [starCurrent] = new Vector3(0, 0, 0);
			starList.Add (star = new Star (this, new Vector3(5f, 5f, 0), starCurrent));
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


			//Gammal stjärnplacering
			foreach (var starCheck in starList) {
				//Debug.Log (Vector3.Distance (starCheck.position, starPos));
				if (Vector3.Distance (starCheck.position, starPos) <= gameSettings.minStarDistance){
					//Debug.Log("failed");
					failed = true;
					break;
				}
			}

			if (!failed) {
				
				//starStream [starCurrent] = starPos;
				starList.Add (star = new Star (this, starPos, starCurrent));
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
		return false;
	}

	public Sector CheckNeighbours(int i){
		Debug.Log (i);
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
