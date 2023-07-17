using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Placement {

	public class PlacementController:MonoBehaviour {

		[SerializeField] CommandData commandData;

		new Camera camera;
		private void Start() {
			camera=GetComponent<Camera>();
		}

		private void Update() {
			Vector3 mouseOverPosition = Vector3.down*10;
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			int cnt = Physics.RaycastNonAlloc(ray,UtilityPhysics.rayactBuffer);
			for(int i = 0;i<cnt;i++) {
				RaycastHit hit = UtilityPhysics.rayactBuffer[i];
				if(hit.collider.GetComponent<GridObject>()) {
					mouseOverPosition=hit.point;
					break;
				}
			}

			if(mouseOverPosition.y>=0) {

				Vector2Int index = GridObject.instance.GetGridIndex(mouseOverPosition);
				bool canPlace=commandData.CanPlace(index);

				commandData.DoPreview(GridObject.instance.GetGridPosition(index),canPlace);

				if(Input.GetMouseButtonDown(0)&&canPlace) {
					var targetElement = GridObject.instance.GetElement(mouseOverPosition);
					targetElement.AddCommand(commandData);
				}

			}


		}

	}

}