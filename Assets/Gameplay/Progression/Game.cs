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

		public static Game instance;
		private void Start() {
			instance=this;

			for(int i = 0;i<GridObject.instance.size.x;i++)
				for(int j = 0;j<GridObject.instance.size.y;j++) {
					GridObject.instance.GetElement(new Vector2Int(i,j)).PlaceTile(tileBase.prefab);
				}
			int edgeWidth = 5;
			for(int i = 0;i<GridObject.instance.size.x;i++) {
				edgeWidth+=Random.Range(-1,2);
				edgeWidth=Mathf.Clamp(edgeWidth,3,10);
			}

		}

		public int money;

	}

}