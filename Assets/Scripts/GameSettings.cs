using UnityEngine;
using System.Collections;

namespace gameSettings{
	public static class GameSettings {

		//Sector Settings
		public static float sectorSize = 10f;

		//Star Settings
		//public int numberOfStars = 2000;
		public static float starSize = 0.3f;
		//public float maxStarDistance = 8.0f;
		public static float minStarDistance = 2.0f;


		//Starway Settings
		public static float starwayLenght = 4f;


		//Camera Settings
		public static int worldSize = 50;

		public static float cameraDistanceMax = 20f;
		public static float cameraDistanceMin = 5f;
		public static float cameraDistance = 10f;
		public static float scrollSpeed = 0.5f;
		
	}
}
