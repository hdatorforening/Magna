using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class Star {

	public bool starwayGen = false;
	public List<Star> connectedStars = new List<Star>(); //Alla stjärnor som denna stjärna har anslutning till.

	Galaxy galaxy;

	int starID;
	public int id { get { return starID;} }

	Vector3 starPosition;
	public Vector3 position { get { return starPosition;} }

	public Star( Galaxy galaxy, Vector3 position, int id ){
		this.galaxy = galaxy;
		this.starPosition = position;
		this.starID = id;
	}

}
