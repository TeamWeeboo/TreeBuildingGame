using Gameplay.Placement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gameplay.Placement {

	[Obsolete]
	//[CreateAssetMenu(menuName = "自定/放置地面选项")]
	public class PlacementOptionData:CommandData {
		[SerializeField] TileData tileType;
		GameObject placementPrefab => tileType.prefab;
		[SerializeField] Material previewMaterial;

		public override void UpdateCommand(GridElement instance) {
			base.UpdateCommand(instance);

			if(instance.placedTileController==null) instance.PlaceTile(tileType);


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
			if(Progression.Game.instance.money<tileType.cost) return false;
			if(GridObject.instance.IsIndexOOB(index)) return false;
			return GridObject.instance.GetElement(index).placedTileController==null;
		}
	}

}