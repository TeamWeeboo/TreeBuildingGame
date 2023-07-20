using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Gameplay.Placement;
using Gameplay.Progression;

public class MoneyDisplay:MonoBehaviour {
	TextMeshProUGUI textMoney;
	TextMeshProUGUI textCost;

	private void Update() {
		textCost.text=$"»¨·Ñ {PlacementController.instance.commandData.cost}";
		textMoney.text=$"»õ±Ò´¢´æ {Game.instance.money}";
	}
}
