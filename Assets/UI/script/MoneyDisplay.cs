using System.Collections;
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
		textCost.text=$"花费 {PlacementController.instance.commandData.cost}";
		textMoney.text=$"经费 {Game.instance.money}";
		textCoverage.text=$"绿化进度 {TreeGrowth.totalCoverage} / "+
		$"{SandstormController.instance.difficultyList[SandstormController.instance.currentDifficulty]} ";
	}
}
