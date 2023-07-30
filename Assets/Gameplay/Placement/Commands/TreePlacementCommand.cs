using Gameplay.Placement;
using Gameplay.Progression;
using Gameplay.Simulation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Placement {

	[CreateAssetMenu(menuName = "自定/放置树种选项")]
	public class TreePlacementCommand:CommandData {
		[SerializeField] TreeData treeType;
		GameObject placementPrefab => treeType.prefab;

		public override void UpdateCommand(GridElement instance) {
			base.UpdateCommand(instance);

			if(instance.placedObjectController==null) instance.PlaceObject(treeType);


			instance.ClearCommand();
		}

		PlacementPreviewController previewController;

		public override void DoPreview(Vector2Int start,Vector2Int end,bool doPreview) {
			base.DoPreview(start,end,doPreview);
			bool canAfford = CanAfford(start,end,PlacementController.instance.fillRatio);
			bool canPlace = CanPlace(end)&&canAfford;

			Vector3 position = GridObject.instance.GetGridPosition(end);
			if(previewController==null&&previewMaterial!=null) {
				GameObject previewObject = Instantiate(placementPrefab);
				previewController=previewObject.AddComponent<PlacementPreviewController>();
				previewController.Init(previewMaterial);
				Destroy(previewObject.GetComponent<TreeGrowth>());
				Destroy(previewObject.GetComponent<PlacedObjectController>());
			} else if(previewController==null) return;
			previewController.transform.position=position;
			previewController.UpdateColor(canPlace ? new Color(1,1,1,0.5f) : new Color(1,0.3f,0.3f,0.5f));
			previewController.gameObject.SetActive(true);

			previewAreaMaterial.color=canAfford ? new Color(1,1,1,0.5f) : new Color(0.6f,0.1f,0.1f,0.5f);
		}
		public override void EndPreview() {
			base.EndPreview();
			if(!previewController) return;
			previewController.gameObject.SetActive(false);
		}

		public override bool CanPlace(Vector2Int index) {
			if(Game.instance.money<treeType.cost) return false;
			if(GridObject.instance.IsIndexOOB(index)) return false;
			return GridObject.instance.GetElement(index).placedObjectController==null;
		}

		public static TreePlacementCommand GenerateCommand(TreeData boundData) {
			TreePlacementCommand result = CreateInstance<TreePlacementCommand>();
			result.treeType=boundData;
			return result;
		}

		public override int cost => treeType.cost;

	}

}