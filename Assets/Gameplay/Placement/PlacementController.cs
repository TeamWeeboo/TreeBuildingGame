using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CULU;
using Unity.VisualScripting;
using Gameplay.Progression;

namespace Gameplay.Placement {

	public class PlacementController:MonoBehaviour {

		public CommandData commandData;
		CameraController cameraController;

		public float fillRatio;

		public static PlacementController instance { get; private set; }

		private void Start() {
			instance=this;
			cameraController=GetComponentInParent<CameraController>();
		}

		Vector2Int startIndex;
		bool isDragging;

		private void Update() {

			Vector3 mouseOverPosition = cameraController.mouseOverPosition;

			if(mouseOverPosition.y>=-9999) {

				Vector2Int index = GridObject.instance.GetGridIndex(mouseOverPosition);
				Vector2 start = startIndex;
				Vector2 end = index;
				if(start.x>end.x) Utility.Swap(ref start.x,ref end.x);
				if(start.y>end.y) Utility.Swap(ref start.y,ref end.y);

				commandData.DoPreview(isDragging ? startIndex : index,index,true);
				if(!isDragging&&Input.GetMouseButton(0)&&!BlockDetector.isBlocked) {
					startIndex=index;
					isDragging=true;
				}

				Debug.Log(GridObject.instance.GetElement(mouseOverPosition));

				if(isDragging&&!Input.GetMouseButton(0)) {
					isDragging=false;

					float fillT = 1;

					if(commandData.CanAfford(start,end,PlacementController.instance.fillRatio))
						for(int i = (int)start.x;i<=end.x;i+=1)
							for(int j = (int)start.y;j<=end.y;j+=1) {
								Vector2Int indexHere = new Vector2Int(i,j);
								var targetElement = GridObject.instance.GetElement(indexHere);
								if(targetElement!=null&commandData.CanPlace(indexHere)) {
									fillT+=Random.Range(fillRatio*0.5f,fillRatio*1.5f);
									if(commandData is RemoveCommand) fillT+=1;
									if(fillT<=0) continue;
									fillT-=1;
									Game.instance.money-=commandData.cost;
									targetElement.AddCommand(commandData);
								}
							}
				}
			}


		}

	}

}