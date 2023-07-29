using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using CULU;
using Gameplay.Progression;
using Unity.VisualScripting;

namespace Gameplay.Placement {

	public class CommandData:ScriptableObject {

		[SerializeField] GameObject previewAreaPrefab;
		[SerializeField] Material _previewMaterial;
		public readonly static HashSet<CommandData> datas = new HashSet<CommandData>();
		protected static GameObject previewAreaDisplay;
		protected static Material previewAreaMaterial;
		protected static Material previewMaterial;

		public virtual void UpdateCommand(GridElement instance) { }
		protected virtual void OnEnable() {

			datas.Add(this);
			if(_previewMaterial!=null&&previewAreaDisplay==null) previewMaterial=_previewMaterial;
		}
		public virtual void DoPreview(Vector2Int start,Vector2Int end,bool doPreview) {
			foreach(var i in datas) {
				if(i!=this) i.EndPreview();
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

		public virtual int cost => 0;

		public bool CanAfford(Vector2 start,Vector2 end,float fillRatio) {

			float fillT = 0;

			if(start.x>end.x) Utility.Swap(ref start.x,ref end.x);
			if(start.y>end.y) Utility.Swap(ref start.y,ref end.y);
			bool canAfford = false;
			int totalCost = 0;
			for(int i = (int)start.x;i<=end.x;i+=1)
				for(int j = (int)start.y;j<=end.y;j+=1) {
					Vector2Int indexHere = new Vector2Int(i,j);
					var targetElement = GridObject.instance.GetElement(indexHere);
					if(targetElement!=null&CanPlace(indexHere)) {
						fillT+=fillRatio;
						if(fillT<1) continue;
						fillT-=1;
						totalCost+=cost;
					}
				}
			canAfford=totalCost<=Game.instance.money;
			return canAfford;

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