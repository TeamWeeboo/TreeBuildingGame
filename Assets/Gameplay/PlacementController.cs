using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementController:MonoBehaviour {

	new Camera camera;
	private void Start() {
		camera=GetComponent<Camera>();
	}

	private void Update() {
		Vector3 mouseOverPosition;
		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		int cnt = Physics.RaycastNonAlloc(ray,UtilityPhysics.rayactBuffer);
		for(int i = 0;i<cnt;i++) {
			RaycastHit hit = UtilityPhysics.rayactBuffer[i];
			if(hit.collider.GetComponent<PlacementController>()){
				mouseOverPosition=hit.point;
				break;
			}
		}

		

	}

}
