using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Progression {

	public class Game:MonoBehaviour {

		public static Game instance;
		private void Start() {
			instance=this;
		}

		public int money;

	}

}