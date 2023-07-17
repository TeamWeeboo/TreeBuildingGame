using Gameplay.Placement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay {

	public class PlacedObjectController:MonoBehaviour {
		public GridElement boundElement { get; private set; }
		public GameObject prefab { get; private set; }
		public void Init(GridElement boundElement,GameObject prefab) {
			this.boundElement=boundElement;
			this.prefab=prefab;
		}
	}

}