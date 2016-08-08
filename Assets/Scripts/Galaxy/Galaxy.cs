using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public class Galaxy {

	GameSettings gameSettings = new GameSettings();

	StarPlacementScript starPlacementScript = new StarPlacementScript();
	StarwayGen starwayGen = new StarwayGen ();

	public List<Star> starList = new List<Star>(); //List of all existing stars
	public List<Starway> starwayList = new List<Starway>(); //List of all existing starways

	public Galaxy( int numberOfSystems = 100){
		Debug.Log ("New Galaxy created");

		starPlacementScript.GenerateStarCluster (this, numberOfSystems);

		starwayGen.GenerateStarways (this, gameSettings.starwayLenght);
	}

}