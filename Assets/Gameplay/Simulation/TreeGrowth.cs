using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Simulation {

	public class TreeGrowth:MonoBehaviour {

		TreeData boundData;
		[SerializeField] GameObject saplingObject;
		[SerializeField] GameObject treeObject;
		[SerializeField] GameObject diseaseObject;

		PlacedObjectController placement;
		float timeGrowth;
		float timeDisease = -1;
		int stage;

		static float moneyCumulated;

		private void Start() {
			placement=GetComponent<PlacedObjectController>();
			treeObject.SetActive(false);
			saplingObject.SetActive(true);
			diseaseObject.SetActive(true);
			boundData=placement.boundElement.treeData;
			if(!treeCount.ContainsKey(boundData)) treeCount[boundData]=0;
		}

		private void FixedUpdate() {
			timeGrowth+=Time.deltaTime;
			if(stage==0&&timeGrowth>boundData.growthTime) OnGrow();

			moneyCumulated+=boundData.economics*Time.deltaTime;
			if(moneyCumulated>1) {
				Progression.Game.instance.money+=1;
				moneyCumulated-=1;
			}

			if(timeDisease>=0) {

			}

		}

		public virtual void OnGrow() {
			float chance = boundData.survivalChance[placement.boundElement.tileData];
			bool doGrow = Random.Range(0f,1f)<chance;
			if(doGrow) {
				stage=1;
				saplingObject.SetActive(false);
				treeObject.SetActive(true);
				treeCount[boundData]++;
			} else placement.boundElement.PlaceObject(null);
		}
		private void OnDestroy() {
			if(stage==1) treeCount[boundData]--;
		}

		static Dictionary<TreeData,int> treeCount = new Dictionary<TreeData,int>();
		static Dictionary<TreeData,float> lastDiseaseAttempt = new Dictionary<TreeData,float>();
		static Dictionary<TreeData,float> diseaseChance = new Dictionary<TreeData,float>();
		const float updateIntercal = 10;
		static readonly float[] diseaseChanceIncrease = { 0.01f,0.05f,0.1f };
		static void UpdateDiseaseCreation(TreeData toUpdate) {
			if(!lastDiseaseAttempt.ContainsKey(toUpdate))
				lastDiseaseAttempt.Add(toUpdate,Time.time);
			if(!diseaseChance.ContainsKey(toUpdate))
				diseaseChance.Add(toUpdate,0);

			if(lastDiseaseAttempt[toUpdate]<Time.time-updateIntercal) {
				lastDiseaseAttempt[toUpdate]=Time.time;
				int diseaseStage = 0;
				for(int _ = 0;_<100;_++) {
					if(diseaseStage+1>=toUpdate.diseaseThreshold.Length) break;
					if(toUpdate.diseaseThreshold[diseaseStage+1]>treeCount[toUpdate]) break;
					diseaseStage++;
				}
				diseaseChance[toUpdate]+=diseaseChanceIncrease[diseaseStage];

				bool doCatch = Random.Range(0f,1f)<diseaseChance[toUpdate];
				if(doCatch) {
					bool catchSuccess = false;
					for(int _ = 0;_<20;_++) {
						Vector2Int index = new Vector2Int();
					}
				}

			}

		}

	}

}