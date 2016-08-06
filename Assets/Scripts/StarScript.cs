using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

namespace Galaxy{
	public class StarScript : MonoBehaviour,
		IPointerClickHandler

	{

		public bool starwayGen = false;
		public List<Object> connectedStars = new List<Object>(); //Alla stjärnor som denna stjärna har anslutning till.

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public void OnPointerClick( PointerEventData data ){
			Debug.Log("Oh, you are making me horny.");
		}
	}
}
