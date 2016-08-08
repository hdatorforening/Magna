using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	//Sprites
	public Sprite starSprite;

	Galaxy galaxy;

	GameSettings gameSettings = new GameSettings();

	// Use this for initialization
	void Start () {
		Camera.main.orthographicSize = gameSettings.worldSize;

		galaxy = new Galaxy (gameSettings.numberOfStars);

		foreach (Star star in galaxy.starList) {
			GameObject star_go = new GameObject ();
			star_go.name = "Star_" +star.id;
			star_go.transform.position = star.position;
			star_go.transform.localScale = new Vector3 (0.2f, 0.2f, 0.2f);

			SpriteRenderer star_sr = star_go.AddComponent<SpriteRenderer> ();
			star_sr.sprite = starSprite;
		}
	}

}
