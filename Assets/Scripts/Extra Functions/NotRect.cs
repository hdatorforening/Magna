using UnityEngine;
using System.Collections;

namespace Funktioner{
	public static class NotRect {

		public static bool notRect(Vector3 p1, Vector3 p2, Vector3 intersect){
			bool sect = false;

			float rectOffset = 0;

			if(p1.x < p2.x){
				if (((p1.x + rectOffset) < intersect.x) && (intersect.x < (p2.x - rectOffset))){
					sect = true;
					//Debug.Log ("NotRect.x.if");
				}
			}else{
				if (((p2.x + rectOffset) < intersect.x) && (intersect.x < (p1.x - rectOffset))){
					sect = true;
					//Debug.Log ("NotRect.x.else");
				}
			}

			if (sect) {
				if (p1.y < p2.y) {
					if (((p1.y + rectOffset) < intersect.y) && (intersect.y < (p2.y - rectOffset))) {
						//Debug.Log ("NotRect.y.if");
						return true;
					}
				} else {
					if (((p2.y + rectOffset) < intersect.y) && (intersect.y < (p1.y - rectOffset))) {
						//Debug.Log ("NotRect.y.else");
						return true;
					}
				}
			}
			//Debug.Log ("NotRect.False");
			return false;

		}

	}
}
