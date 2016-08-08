using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	//Sprites
	public Sprite starSprite;

	Galaxy galaxy = new Galaxy ();

	// Use this for initialization
	void Start () {
		galaxy.GenerateGalaxy ();
	}

}
