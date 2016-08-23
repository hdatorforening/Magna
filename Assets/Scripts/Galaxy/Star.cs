using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class Star {

	public bool starwayGen = false;
	public List<Star> connectedStars = new List<Star>(); //Alla stjärnor som denna stjärna har anslutning till.
	public List<Body> bodyList = new List<Body>();

	Sector sector; //TODO - Används inte, kanske bör tas bort.

	int starID;
	public int id { get { return starID;} }

	Vector3 starPosition;
	public Vector3 position { get { return starPosition;} }

	public GameObject go;

	public Star( Sector sector, Vector3 position){
		this.sector = sector;
		this.starPosition = position;
		this.starID = sector.Galaxy.GetNewStarID();

		for (int i = 0; i < Random.value * 3; i++) {
			bodyList.Add (new Body ());
		}

	}

}
