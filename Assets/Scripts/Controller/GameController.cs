using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	GameObject dummyStar;
	GameObject dummyStarway;

	//Sprites
	public Sprite starSprite;

	Galaxy galaxy;

	GameSettings gameSettings = new GameSettings();

	// Use this for initialization
	void Start () {
		Setup ();

		galaxy = new Galaxy (gameSettings.numberOfStars);

		Debug.Log (galaxy.starList.Count);

		foreach (Star star in galaxy.starList) {
			DrawStar (star);
		}

		foreach (Starway line in galaxy.starwayList) {
			DrawStarway (line);
		}
			
	}

	void Setup(){
		Camera.main.orthographicSize = gameSettings.worldSize;

		dummyStar = new GameObject ("Stars");
		dummyStar.transform.position = new Vector3 ();
		dummyStar.transform.SetParent (this.transform);

		dummyStarway = new GameObject ("Starways");
		dummyStarway.transform.position = new Vector3 ();
		dummyStarway.transform.SetParent (this.transform);

	}

	void DrawStar(Star star){
		GameObject star_go = new GameObject ();
		star_go.name = "Star_" +star.id;
		star_go.transform.position = star.position;
		star_go.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
		star_go.transform.SetParent (dummyStar.transform, true);

		SpriteRenderer star_sr = star_go.AddComponent<SpriteRenderer> ();
		star_sr.sprite = starSprite;
	}
		
	void DrawStarway (Starway line){
		
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
	}

}
