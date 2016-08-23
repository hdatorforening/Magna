using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


namespace globalVariables{

	public static class prefabs{
		//public static GameObject greenSelectCircleGo;
	}

	public static class GlobalVariables{

		static Agent player;
		public static Agent Player { get { return player;} set { player = value;} }

		public static Galaxy galaxy;

	}

	public static class UI{

		//Prefabs
		public static GameObject greenSelectCircle;
		public static GameObject systemUIBody;
		public static GameObject ui;

		public static Star hoverStar;
		public static Star selectedStar;

		static GameObject mainMenu;
		public static GameObject MainMenu { get { return mainMenu;} set { mainMenu = value;} }
	}
}
