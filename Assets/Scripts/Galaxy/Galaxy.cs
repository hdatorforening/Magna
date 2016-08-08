using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Galaxy {

	//Calling scripts
	StarPlacementScript starPlacementScript = new StarPlacementScript();


	public List<Object> starList = new List<Object>(); //List of all existing stars
	public List<Starway> starwayList = new List<Starway>(); //List of all existing starways

	public void GenerateGalaxy(int numberOfSystems = 100){
		Debug.Log ("New Galaxy created");

		starPlacementScript.GenerateStarCluster (numberOfSystems);
	}

}
