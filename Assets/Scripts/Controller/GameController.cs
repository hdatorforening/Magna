using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using gameSettings;
using globalVariables;

public class GameController : MonoBehaviour {

	Agent player;

	MouseController mouseController;


	GameObject dummyStar;
	GameObject dummyStarway;
 	public GameObject dummyUI;

	public GameObject mainMenuPrefab;
	public GameObject greenSelectCirlce;

	//Sprites
	public Sprite starSprite;

	public Galaxy galaxy;
	public Galaxy Galaxy { get { return galaxy;} }

	//public Galaxy Galaxy { get { return galaxy;} }

	// Use this for initialization
	void Start () {
		Setup ();

		galaxy = new Galaxy (this);
		GlobalVariables.galaxy = galaxy;

		player = new Agent ();
			
	}

	void Setup(){
		//Set the default camera zoom
		Camera.main.orthographicSize = GameSettings.cameraSizeGalaxy;
		
		//Creates an object to place the stars under.
		dummyStar = new GameObject ("Stars");
		dummyStar.transform.position = new Vector3 ();
		dummyStar.transform.SetParent (this.transform);

		//Creates an object to place the starways under.
		dummyStarway = new GameObject ("Starways");
		dummyStarway.transform.position = new Vector3 ();
		dummyStarway.transform.SetParent (this.transform);

		//Creates and hides the main menu.
		globalVariables.UI.MainMenu = Instantiate (mainMenuPrefab);
		globalVariables.UI.MainMenu.SetActive (false);
		globalVariables.UI.MainMenu.transform.SetParent (dummyUI.transform);

		//Setup the basic prefabs
		globalVariables.UI.greenSelectCircle = (GameObject)Instantiate(greenSelectCirlce, new Vector3(0, 0, 0), Quaternion.identity);
		globalVariables.UI.greenSelectCircle.transform.SetParent (dummyUI.transform);
	}

	public void DrawStar(Star star){
		Profiler.BeginSample ("DrawStar()");


		GameObject star_go = new GameObject ();
		star_go.name = "Star_" + star.id;
		star_go.transform.position = star.position;

		star_go.transform.localScale = new Vector3 (
			GameSettings.starSize, 
			GameSettings.starSize, 
			GameSettings.starSize
		);

		star_go.transform.SetParent (dummyStar.transform, true);

		SpriteRenderer star_sr = star_go.AddComponent<SpriteRenderer> ();
		star_sr.sprite = starSprite;
		//Destroy (star_go, Time.deltaTime);
		Profiler.EndSample ();
	}
		
	public void DrawStarway (Starway line){
		Profiler.BeginSample ("DrawStarway()");
		
		Vector3 start = line.Start;
		Vector3 end = line.End;

		Material whiteStarway = new Material(Shader.Find("Unlit/Texture"));

		GameObject lineObj = new GameObject("Starway_"+line.Id, typeof(LineRenderer));
		lineObj.transform.SetParent (dummyStarway.transform, true);
		LineRenderer newLine = lineObj.GetComponent<LineRenderer> ();

		newLine.sortingLayerName = "OnTop";
		newLine.sortingOrder = 5;
		newLine.SetVertexCount(2);
		newLine.SetPosition(0, start);
		newLine.SetPosition(1, end);
		newLine.SetWidth(0.04f, 0.04f);
		newLine.useWorldSpace = true;
		newLine.SetColors (Color.white, Color.white);
		newLine.material = whiteStarway;

		line.gameObject = lineObj;

		Profiler.EndSample ();
	}

}
