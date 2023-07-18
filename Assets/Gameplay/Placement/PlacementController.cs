using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Placement {

	public class PlacementController:MonoBehaviour {

		[SerializeField] CommandData commandData;
		CameraController cameraController;

		private void Start() {
			cameraController=GetComponentInParent<CameraController>();
		}

		private void Update() {
			Vector3 mouseOverPosition = cameraController.mouseOverPosition;

			if(mouseOverPosition.y>=0) {

				Vector2Int index = GridObject.instance.GetGridIndex(mouseOverPosition);
				bool canPlace = commandData.CanPlace(index);
				commandData.DoPreview(GridObject.instance.GetGridPosition(index),canPlace);
				if(Input.GetMouseButtonDown(0)&&canPlace) {
					var targetElement = GridObject.instance.GetElement(mouseOverPosition);
					targetElement.AddCommand(commandData);
				}

			}


		}

	}

}