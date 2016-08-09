using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public class Galaxy {

	GameSettings gameSettings = new GameSettings();

	//StarPlacementScript starPlacementScript = new StarPlacementScript();
	StarwayGen starwayGen = new StarwayGen ();

	public List<Sector> sectorList = new List<Sector> (); //List att existing secotrs
	//public List<Star> starList = new List<Star>(); //List of all existing stars
	public List<Starway> starwayList = new List<Starway>(); //List of all existing starways

	public Galaxy(){
		Debug.Log ("New Galaxy created");

		//starPlacementScript.GenerateStarCluster (this, numberOfSystems);
		for (int x = -2; x < 3; x++) {
			for (int y = -2; y < 3; y++) {
				Sector newSector = GetSector(x,y);
				newSector.GenerateSector ();
				sectorList.Add( newSector );
			}
		}


		starwayGen.GenerateStarways (this, gameSettings.starwayLenght);
		Debug.Log (sectorList);
	}

	Sector GetSector(int x, int y){
		foreach (Sector sector in sectorList) {
			if (sector.X == x && sector.Y == y) { return sector; }
		}

		Sector newSector = new Sector (x, y, gameSettings);
		sectorList.Add (newSector);
		return newSector;
	}

}