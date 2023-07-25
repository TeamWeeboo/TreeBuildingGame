﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Gameplay.Placement;
using Gameplay.Progression;
using Gameplay.Simulation;

public class MoneyDisplay:MonoBehaviour {
	[SerializeField] TextMeshProUGUI textMoney;
	[SerializeField] TextMeshProUGUI textCost;
	[SerializeField] TextMeshProUGUI textCoverage;

	private void Update() {
		textCost.text=$"种植花费·planting cost： {PlacementController.instance.commandData.cost}¥";
		textMoney.text=$"经费·Fund： {Game.instance.money} ¥";
		textCoverage.text=$"{TreeGrowth.totalCoverage} / "+
		$"{SandstormController.instance.difficultyList[SandstormController.instance.currentDifficulty]} ";
	}
}
