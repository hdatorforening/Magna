using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using gameSettings;

public class Galaxy {

	public GameController gameController;
	//GameSettings gameSettings = new GameSettings();

	//StarPlacementScript starPlacementScript = new StarPlacementScript();

	public List<Sector> sectorList = new List<Sector> (); //List att existing secotrs
	public List<Starway> starwayList = new List<Starway>(); //List of all existing starways
	int starID = -1;

	public Galaxy(GameController gameController){
		Debug.Log ("New Galaxy created");

		this.gameController = gameController;

		for (int x = -3; x < 4; x++) {
			for (int y = -3; y < 4; y++) {
				GetSector (x, y);
			}
		}

		//GetSector (0, 0);
		//GetSector (1, 1);

		foreach (Star star in GetSector(0,0).starList) {
			if(star.position == new Vector3()){
				//Debug.Log("Starcount = "+star.connectedStars.Count);
			}
		}
	}

	public Sector GetSector(int x, int y){
		foreach (Sector sector in sectorList) {
			if (sector.X == x && sector.Y == y) { return sector; }
		}

		Sector newSector = new Sector (x, y, this);
		return newSector;
	}

	public Sector GetSectorFromPos(Vector3 position){
		int x = Mathf.CeilToInt ((position.x - 5) / gameSettings.GameSettings.sectorSize);
		int y =	Mathf.CeilToInt ((position.y - 5) / gameSettings.GameSettings.sectorSize);

		//Debug.Log ("Count: " + sectorList.Count ());
		//Debug.Log ("Pos: " + position + ", X: " + x + ", Y: " + y);

		foreach (Sector sector in sectorList) {
			//Debug.Log ("Sector: " + sector.X + ", " + sector.Y);
			if (sector.X == x && sector.Y == y) { return sector; }
		}

		//Debug.Log ("Return NULL");

		return null;
	}

	//Retunerar den ID som en ny stjärna skall bli tilldelad.
	public int GetNewStarID (){
		starID++;
		return starID;
	}

}