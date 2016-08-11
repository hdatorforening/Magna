using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using gameSettings;

public class Galaxy {

	//GameSettings gameSettings = new GameSettings();

	//StarPlacementScript starPlacementScript = new StarPlacementScript();

	public List<Sector> sectorList = new List<Sector> (); //List att existing secotrs
	//public List<Star> starList = new List<Star>(); //List of all existing stars
	public List<Starway> starwayList = new List<Starway>(); //List of all existing starways

	public Galaxy(){
		Debug.Log ("New Galaxy created");

		for (int x = -5; x < 6; x++) {
			for (int y = -5; y < 6; y++) {
				GetSector(x,y);
			}
		}

		foreach (Star star in GetSector(0,0).starList) {
			if(star.position == new Vector3()){
				Debug.Log("Starcount = "+star.connectedStars.Count);
			}
		}
	}

	Sector GetSector(int x, int y){
		foreach (Sector sector in sectorList) {
			if (sector.X == x && sector.Y == y) { return sector; }
		}

		Sector newSector = new Sector (x, y, this);
		sectorList.Add (newSector);
		return newSector;
	}

}