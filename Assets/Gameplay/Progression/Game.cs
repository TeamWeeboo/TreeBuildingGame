using Gameplay.Placement;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay.Progression {

	public class Game:MonoBehaviour {

		[SerializeField] TileData tileBase;
		[SerializeField] TileData tileBorder;
		[SerializeField] TileData tileSpread;

		[SerializeField] Vector2Int borderWidthRange;
		[SerializeField] int spreadCoreCountMax;
		[SerializeField] int spreadExtensionCountMax;
		[SerializeField] Vector2Int spreadCoreRadiusRange;
		[SerializeField] Vector2Int spreadExtensionRadiusRange;

		public static Game instance;
		private void Start() {
			instance=this;
			GridObject gridObject = GridObject.instance;

			for(int i = 0;i<gridObject.size.x;i++)
				for(int j = 0;j<gridObject.size.y;j++) {
					gridObject.GetElement(new Vector2Int(i,j)).PlaceTile(tileBase);
				}

			#region PlaceSpread

			for(int i = 0;i<spreadCoreCountMax;i++) {
				Vector2Int center = new Vector2Int(Random.Range(0,gridObject.size.x),Random.Range(0,gridObject.size.y));
				int radius = Random.Range(spreadCoreRadiusRange.x,spreadCoreRadiusRange.y);
				bool canAdd = true;

				for(int x = Mathf.Max(0,center.x-radius);x<Mathf.Min(gridObject.size.x,center.x+radius+1);x++) {
					for(int y = Mathf.Max(0,center.y-radius);y<Mathf.Min(gridObject.size.y,center.y+radius+1);y++) {
						if(gridObject.GetElement(new Vector2Int(x,y)).placedTileController.prefab==tileSpread.prefab) {
							canAdd=false;
							break;
						}
					}
					if(!canAdd) break;
				}

				if(!canAdd) continue;

				for(int x = Mathf.Max(0,center.x-radius);x<Mathf.Min(gridObject.size.x,center.x+radius+1);x++)
					for(int y = Mathf.Max(0,center.y-radius);y<Mathf.Min(gridObject.size.y,center.y+radius+1);y++) {
						Vector2Int position = new Vector2Int(x,y);
						if((center-position).sqrMagnitude>radius*radius) continue;
						gridObject.GetElement(position).PlaceTile(tileSpread);
					}
			}

			for(int i = 0;i<spreadExtensionCountMax;i++) {
				Vector2Int center = new Vector2Int(Random.Range(0,gridObject.size.x),Random.Range(0,gridObject.size.y));
				int radius = Random.Range(spreadExtensionRadiusRange.x,spreadExtensionRadiusRange.y);
				bool canAdd = gridObject.GetElement(center).placedTileController.prefab==tileSpread.prefab;
				
				for(int x = Mathf.Max(0,center.x-radius);x<Mathf.Min(gridObject.size.x,center.x+radius+1);x++) {
					for(int y = Mathf.Max(0,center.y-radius);y<Mathf.Min(gridObject.size.y,center.y+radius+1);y++) {
						if(gridObject.GetElement(new Vector2Int(x,y)).placedTileController.prefab==tileSpread.prefab) {
							canAdd=true;
							break;
						}
					}
					if(canAdd) break;
				}
				
				if(!canAdd) continue;

				for(int x = Mathf.Max(0,center.x-radius);x<Mathf.Min(gridObject.size.x,center.x+radius+1);x++)
					for(int y = Mathf.Max(0,center.y-radius);y<Mathf.Min(gridObject.size.y,center.y+radius+1);y++) {
						Vector2Int position = new Vector2Int(x,y);
						if((center-position).sqrMagnitude>radius*radius) continue;
						gridObject.GetElement(position).PlaceTile(tileSpread);
					}
			}

			#endregion

			#region PlaceBorder
			int edgeWidth = (borderWidthRange.x+borderWidthRange.y)/2;
			for(int i = 0;i<gridObject.size.x;i++) {
				edgeWidth+=Random.Range(-1,2);
				edgeWidth+=Random.Range(-1,2);
				edgeWidth=Mathf.Clamp(edgeWidth,borderWidthRange.x,borderWidthRange.y);
				for(int j = 0;j<edgeWidth;j++)
					gridObject.GetElement(new Vector2Int(i,j)).PlaceTile(tileBorder);
			}
			for(int i = 0;i<gridObject.size.x;i++) {
				edgeWidth+=Random.Range(-1,2);
				edgeWidth+=Random.Range(-1,2);
				edgeWidth=Mathf.Clamp(edgeWidth,borderWidthRange.x,borderWidthRange.y);
				for(int j = 0;j<edgeWidth;j++)
					gridObject.GetElement(new Vector2Int(i,gridObject.size.y-1-j)).PlaceTile(tileBorder);
			}
			for(int i = 0;i<gridObject.size.y;i++) {
				edgeWidth+=Random.Range(-1,2);
				edgeWidth+=Random.Range(-1,2);
				edgeWidth=Mathf.Clamp(edgeWidth,borderWidthRange.x,borderWidthRange.y);
				for(int j = 0;j<edgeWidth;j++)
					gridObject.GetElement(new Vector2Int(j,i)).PlaceTile(tileBorder);
			}
			for(int i = 0;i<gridObject.size.y;i++) {
				edgeWidth+=Random.Range(-1,2);
				edgeWidth+=Random.Range(-1,2);
				edgeWidth=Mathf.Clamp(edgeWidth,borderWidthRange.x,borderWidthRange.y);
				for(int j = 0;j<edgeWidth;j++)
					gridObject.GetElement(new Vector2Int(gridObject.size.x-1-j,i)).PlaceTile(tileBorder);
			}
			#endregion

		}

		public int money;

	}

}