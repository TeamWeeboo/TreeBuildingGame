using Gameplay.Placement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Simulation {

	public class TileChange:MonoBehaviour {

		[SerializeField] Vector2 changeTimeRange;
		[SerializeField] TileData changeFrom;
		[SerializeField] TileData changeTo;
		[SerializeField] bool destroySelfAfterChange;
		PlacedObjectController placement;

		float changeTime;
		float timeAfterStart;
		private void Start() {
			placement=GetComponent<PlacedObjectController>();
			changeTime=Random.Range(changeTimeRange.x,changeTimeRange.y);
		}
		private void FixedUpdate() {
			if(!placement) Destroy(this);
			timeAfterStart+=Time.deltaTime;
			if(timeAfterStart>=changeTime) {
				if(placement.boundElement.tileData==changeFrom)
					placement.boundElement.PlaceTile(changeTo);
				if(destroySelfAfterChange) placement.boundElement.PlaceObject(null);
			}
		}
	}

}