using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Simulation {

	public class TreeGrowth:MonoBehaviour {

		TreeData boundData;
		[SerializeField] GameObject saplingObject;
		[SerializeField] GameObject treeObject;

		PlacedObjectController placement;
		float timeGrowth;
		int stage;


		private void Start() {
			placement=GetComponent<PlacedObjectController>();
			treeObject.SetActive(false);
			saplingObject.SetActive(true);
			boundData=placement.boundElement.treeData;
		}

		private void FixedUpdate() {
			timeGrowth+=Time.deltaTime;
			if(stage==0&&timeGrowth>boundData.growthTime) OnGrow();
		}

		public virtual void OnGrow() {
			float chance = boundData.survivalChance[placement.boundElement.tileData];
			bool doGrow = Random.Range(0f,1f)<chance;
			if(doGrow) {
				stage=1;
				saplingObject.SetActive(false);
				treeObject.SetActive(true);
			} else placement.boundElement.PlaceObject(null);
		}

	}

}