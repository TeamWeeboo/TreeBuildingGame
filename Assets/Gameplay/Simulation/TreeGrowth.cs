using Gameplay.Placement;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

namespace Gameplay.Simulation {

	public class TreeGrowth:MonoBehaviour {

		TreeData boundData;
		[SerializeField] GameObject saplingObject;
		[SerializeField] GameObject treeObject;
		[SerializeField] GameObject diseaseObject;

		PlacedObjectController placement;
		float timeGrowth;
		float timeAfterDisease = -1;
		int stage;

		static public float totalCoverage { get; private set; }
		static float moneyCumulated;
		float diseaseSpreadTime;
		float coverageContribution;

		private void Start() {
			placement=GetComponent<PlacedObjectController>();
			treeObject.SetActive(false);
			saplingObject.SetActive(true);
			diseaseObject.SetActive(false);
			boundData=placement.boundElement.treeData;
			if(!treeCount.ContainsKey(boundData)) treeCount[boundData]=0;
		}

		private void FixedUpdate() {
			timeGrowth+=Time.deltaTime;
			if(stage==0&&timeGrowth>boundData.growthTime) OnGrow();

			if(stage==1) moneyCumulated+=boundData.economics*Time.deltaTime;
			if(moneyCumulated>1) {
				Progression.Game.instance.money+=1;
				moneyCumulated-=1;
			}

			if(timeAfterDisease>=0) {
				timeAfterDisease+=Time.deltaTime;
				if(timeAfterDisease>diseaseSpreadTime) {
					diseaseSpreadTime=boundData.diseaseTime+1;
					for(int i = -1;i<2;i++)
						for(int j = -1;j<2;j++) {
							Vector2Int targetIndex = placement.boundElement.selfIndex+new Vector2Int(i,j);
							if(GridObject.instance.IsIndexOOB(targetIndex)) continue;
							GridElement element = GridObject.instance.GetElement(targetIndex);
							if(element.treeData!=boundData) continue;
							TreeGrowth tree = element.placedObjectController.GetComponent<TreeGrowth>();
							if(tree.timeAfterDisease>=0) continue;
							if(CULU.Utility.Chance(boundData.diseaseSpreadChance))
								tree.CatchDisease();
						}
				}
				if(timeAfterDisease>boundData.diseaseTime) {
					placement.boundElement.PlaceObject(null);
				}
			}

			UpdateDiseaseCreation(boundData);
		}

		public virtual void OnGrow() {
			float chance = boundData.survivalChance[placement.boundElement.tileData];
			bool doGrow = Random.Range(0f,1f)<chance;
			if(doGrow) {
				stage=1;
				saplingObject.SetActive(false);
				treeObject.SetActive(true);
				treeCount[boundData]++;
				coverageContribution+=boundData.coverageContribution;
				totalCoverage+=coverageContribution;
			} else placement.boundElement.PlaceObject(null);
		}
		private void OnDestroy() {
			if(stage==1) treeCount[boundData]--;
			totalCoverage-=coverageContribution;
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
					Vector2Int index = new Vector2Int();
					Vector2Int maxIndex = GridObject.instance.size;
					for(int _ = 0;_<20;_++) {
						index=new Vector2Int(Random.Range(0,maxIndex.x),Random.Range(0,maxIndex.y));
						if(GridObject.instance.GetElement(index).treeData==toUpdate) {
							GridObject.instance.GetElement(index).placedObjectController.GetComponent<TreeGrowth>().CatchDisease();
							catchSuccess=true;
							diseaseChance[toUpdate]=0;
							break;
						}
					}
				}

			}

		}

		protected virtual void CatchDisease() {
			if(timeAfterDisease>=0) return;
			timeAfterDisease=0;
			diseaseObject.SetActive(true);
			diseaseSpreadTime=Random.Range(boundData.diseaseTime*0.4f,boundData.diseaseTime*0.8f);
		}

	}

}