using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using YCGU;

namespace Gameplay.Placement {

	public class CommandData:ScriptableObject {

		[SerializeField] GameObject previewAreaPrefab;
		public readonly static Dictionary<string,CommandData> datas = new Dictionary<string,CommandData>();
		protected static GameObject previewAreaDisplay;
		protected static Material previewAreaMaterial;

		public virtual void UpdateCommand(GridElement instance) { }
		protected virtual void OnEnable() {
			datas.Add(name,this);
		}
		public virtual void DoPreview(Vector2Int start,Vector2Int end,bool doPreview) {
			foreach(var i in datas) {
				if(i.Value!=this) i.Value.EndPreview();
			}
			if(!doPreview) {
				EndPreview();
				previewAreaDisplay?.SetActive(false);
			} else {
				if(previewAreaDisplay==null) {
					previewAreaDisplay=Instantiate(previewAreaPrefab);
					previewAreaMaterial=previewAreaDisplay.GetComponent<Renderer>().material;
				}
				Vector3 startPosition = GridObject.instance.GetGridPosition(start);
				Vector3 endPosition = GridObject.instance.GetGridPosition(end);
				if(startPosition.x>endPosition.x) Utility.Swap(ref startPosition.x,ref endPosition.x);
				if(startPosition.z>endPosition.z) Utility.Swap(ref startPosition.z,ref endPosition.z);
				bool isSingleTile = startPosition==endPosition;
				startPosition+=new Vector3(-0.5f,0,-0.5f)*GridObject.instance.spacing;
				endPosition+=new Vector3(0.5f,0,0.5f)*GridObject.instance.spacing;
				previewAreaDisplay.transform.position=(startPosition+endPosition)*0.5f;
				previewAreaDisplay.transform.localScale=new Vector3(endPosition.x-startPosition.x,0.3f,endPosition.z-startPosition.z);
				previewAreaDisplay?.SetActive(!isSingleTile);
				return;
			}

		}
		public virtual void EndPreview() {

		}

		public virtual bool CanPlace(Vector2Int index) => false;

	}


	[CreateAssetMenu(menuName = "自定/放置选项")]
	public class PlacementOptionData:CommandData {
		[SerializeField] GameObject placementPrefab;
		[SerializeField] Material previewMaterial;
		[SerializeField] bool isTile;

		public override void UpdateCommand(GridElement instance) {
			base.UpdateCommand(instance);

			if(isTile) {
				if(instance.placedTileController==null) instance.PlaceTile(placementPrefab);
			} else {
				if(instance.placedObjectController==null) instance.PlaceObject(placementPrefab);
			}

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
			if(GridObject.instance.IsIndexOOB(index)) return false;
			if(isTile) return GridObject.instance.GetElement(index).placedTileController==null;
			else return GridObject.instance.GetElement(index).placedObjectController==null;
		}
	}


	[CreateAssetMenu(menuName = "自定/删除选项")]
	public class RemoveCommand:CommandData {
		[SerializeField] bool isTile;

		public override void UpdateCommand(GridElement instance) {
			base.UpdateCommand(instance);
			if(isTile) instance.PlaceTile(null);
			else instance.PlaceObject(null);
			instance.ClearCommand();
		}

		public override bool CanPlace(Vector2Int index) {
			if(GridObject.instance.IsIndexOOB(index)) return false;
			if(isTile) return GridObject.instance.GetElement(index).placedTileController!=null;
			else return GridObject.instance.GetElement(index).placedObjectController!=null;
		}

		public override void DoPreview(Vector2Int start,Vector2Int end,bool doPreview) {
			base.DoPreview(start,end,doPreview);
			if(isTile) previewAreaDisplay.transform.position+=Vector3.up*0.3f;
			previewAreaDisplay.SetActive(true);
			previewAreaMaterial.color=new Color(1f,0.3f,0.3f,0.5f);
		}
	}

}