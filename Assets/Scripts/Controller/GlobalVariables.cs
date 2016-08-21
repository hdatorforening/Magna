﻿using UnityEngine;
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

		public static GameObject greenSelectCircle;
		public static GameObject ui;

		static GameObject mainMenu;
		public static GameObject MainMenu { get { return mainMenu;} set { mainMenu = value;} }
	}
}
