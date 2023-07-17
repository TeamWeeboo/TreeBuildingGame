using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Gameplay.Placement {

	public class CommandData:ScriptableObject {

		public readonly static Dictionary<string,CommandData> datas = new Dictionary<string,CommandData>();

		public virtual void UpdateCommand(GridElement instance) { }
		protected virtual void OnEnable() {
			datas.Add(name,this);
		}
		public virtual void DoPreview(Vector3 position,bool canPlace) {
			foreach(var i in datas) {
				if(i.Value!=this) i.Value.EndPreview();
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

		public override void UpdateCommand(GridElement instance) {
			base.UpdateCommand(instance);
			if(instance.placedObject!=placementPrefab) instance.PlaceObject(placementPrefab);
			instance.ClearCommand();
		}

		PlacementPreviewController previewController;

		public override void DoPreview(Vector3 position,bool canPlace) {
			base.DoPreview(position,canPlace);
			if(previewController==null) {
				GameObject previewObject = Instantiate(placementPrefab);
				previewController=previewObject.AddComponent<PlacementPreviewController>();
				previewController.Init(previewMaterial);
			}
			previewController.transform.position=position;
			previewController.UpdateColor(canPlace ? new Color(1,1,1,0.3f) : new Color(1,0.3f,0.3f,0.3f));
			previewController.gameObject.SetActive(true);
		}
		public override void EndPreview() {
			base.EndPreview();
			previewController.gameObject.SetActive(false);
		}

		public override bool CanPlace(Vector2Int index) => GridObject.instance.GetElement(index).placedObject==null;
	}


	[CreateAssetMenu(menuName = "自定/删除选项")]
	public class RemoveCommand:CommandData {
		public override void UpdateCommand(GridElement instance) {
			base.UpdateCommand(instance);
			instance.PlaceObject(null);
			instance.ClearCommand();
		}
	}

}