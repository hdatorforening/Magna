using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using gameSettings;
using globalVariables;

public class GameController : MonoBehaviour {

	Agent player;

	//Scripts.
	StarController starController;
	MouseController mouseController;
	//StarController starController;

	//Dummy objects
	GameObject dummyStar;
	GameObject dummyStarway;
 	public GameObject dummyUI;

	//Prefabs
	public GameObject mainMenuPrefab;
	public GameObject greenSelectCirlce;
	public GameObject systemUIBodyPrefab;

	//Sprites
	public Sprite starSprite;

	public Galaxy galaxy;
	public Galaxy Galaxy { get { return galaxy;} }

	//public Galaxy Galaxy { get { return galaxy;} }

	// Use this for initialization
	void Awake () {
		Setup ();

		galaxy = new Galaxy (this);
		globalVariables.GlobalVariables.galaxy = galaxy;

		player = new Agent ();
			
	}

	void Setup(){
		//Setting the default settings.
		globalVariables.UI.ui = dummyUI;

		//Set the default camera zoom.
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
		globalVariables.UI.systemUIBody = systemUIBodyPrefab;
	}

	public void DrawStar(Star star){
		Profiler.BeginSample ("DrawStar()");

		star.go = new GameObject ();
		star.go.AddComponent<StarController> ();
		star.go.AddComponent<CircleCollider2D> ();

		star.go.name = "Star_" + star.id;
		star.go.transform.position = star.position;

		star.go.GetComponent<StarController> ().star = star;

		star.go.transform.localScale = new Vector3 (
			GameSettings.starSize, 
			GameSettings.starSize, 
			GameSettings.starSize
		);

		star.go.transform.SetParent (dummyStar.transform, true);

		SpriteRenderer star_sr = star.go.AddComponent<SpriteRenderer> ();
		star_sr.sprite = starSprite;
		//StarController star_control = 



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
