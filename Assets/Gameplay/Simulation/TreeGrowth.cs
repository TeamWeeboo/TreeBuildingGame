using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Simulation {

	public class TreeGrowth:MonoBehaviour {

		[SerializeField] TreeData boundData;
		
		public int stage { get; protected set; }
		float timeGrowth;

		private void FixedUpdate() {
			timeGrowth+=Time.deltaTime;
			
		}

		public void OnStageChange(){
			
		}

	}

}