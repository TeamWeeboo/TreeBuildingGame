using Gameplay.Placement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Placement {

	[CreateAssetMenu(menuName = "�Զ�/��������ѡ��")]
	public class TreePlacementCommand:CommandData {
		[SerializeField] TreeData treeType;
		GameObject placementPrefab => treeType.prefab;
		[SerializeField] Material previewMaterial;

		public override void UpdateCommand(GridElement instance) {
			base.UpdateCommand(instance);

			if(instance.placedObjectController==null) instance.PlaceObject(placementPrefab);


			instance.ClearCommand();
		}

		PlacementPreviewController previewController;

		public override void DoPreview(Vector2Int start,Vector2Int end,bool doPreview) {
			base.DoPreview(start,end,doPreview);
			bool canPlace = CanPlace(end);
			Vector3 position = GridObject.instance.GetGridPosition(end);
			if(previewController==null) {
				GameObject previewObject = Instantiate(placementPrefab);
				previewController=previewObject.AddComponent<PlacementPreviewController>();
				previewController.Init(previewMaterial);
			}
			previewController.transform.position=position;
			previewController.UpdateColor(canPlace ? new Color(1,1,1,0.5f) : new Color(1,0.3f,0.3f,0.5f));
			previewController.gameObject.SetActive(true);

			previewAreaMaterial.color=new Color(1,1,1,0.5f);
		}
		public override void EndPreview() {
			base.EndPreview();
			if(!previewController) return;
			previewController.gameObject.SetActive(false);
		}

		public override bool CanPlace(Vector2Int index) {
			if(Progression.Game.instance.money<treeType.cost) return false;
			if(GridObject.instance.IsIndexOOB(index)) return false;
			return GridObject.instance.GetElement(index).placedObjectController==null;
		}
	}

}