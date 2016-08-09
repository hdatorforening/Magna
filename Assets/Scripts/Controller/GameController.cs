using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	float time = 0;
	int[] statistik = new int[11];
	float num;
	Sector tmpsect;

	void Update(){

		/*time += Time.deltaTime;
		if (time > 10f) {
			time = 0;

			foreach (Sector sector in galaxy.sectorList) {
				sector.starList.Clear ();
				sector.GenerateSector (10);
			}

			foreach (Sector sector in galaxy.sectorList) {
				foreach (Star star in sector.starList) {
					DrawStar (star);
				}
				tmpsect = sector;
			}
				
		}*/
	}

	GameObject dummyStar;
	GameObject dummyStarway;

	//Sprites
	public Sprite starSprite;

	Galaxy galaxy;

	GameSettings gameSettings = new GameSettings();

	// Use this for initialization
	void Start () {
		Setup ();

		galaxy = new Galaxy ();

		foreach (Sector sector in galaxy.sectorList) {
			foreach (Star star in sector.starList) {
				DrawStar (star);
			}
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
		star_go.name = "Star";
		star_go.transform.position = star.position;

		star_go.transform.localScale = new Vector3 (
			gameSettings.starSize, 
			gameSettings.starSize, 
			gameSettings.starSize
		);

		star_go.transform.SetParent (dummyStar.transform, true);

		SpriteRenderer star_sr = star_go.AddComponent<SpriteRenderer> ();
		star_sr.sprite = starSprite;
		//Destroy (star_go, Time.deltaTime);
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
