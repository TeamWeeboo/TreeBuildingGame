using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gameplay.Placement;
using Gameplay.Progression;
using Gameplay;

public class RatioBar:MonoBehaviour {
	public Slider slider;

	void Update() {
		PlacementController.instance.fillRatio=slider.value;
		if(PlacementController.instance.fillRatio<0.1f) PlacementController.instance.fillRatio=0.1f;
	}
}
