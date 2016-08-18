using UnityEngine;
using System.Collections;

public class Agent{

	bool player;

	int money;
 	int resourse = 0;


	public Agent(bool player = false){
		this.player = player;
		resourse -= 1;

		Debug.Log (resourse);
	}
}
