using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Gameplay.Placement;
using Gameplay.Progression;
using Gameplay.Simulation;
using Gameplay;

public class DetailsText : MonoBehaviour
{
    private TextMeshProUGUI Text;
    public TreeData Data;
    private int GreenHigh = 3;
    private int GreenMid = 2;
    private int GreenLow = 1;
    private string Green;
    

    private void Awake()
    {
        Text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (Data.coverageContribution >= GreenHigh)
        {
            Green = "¸ß";
        }
        else if (Data.coverageContribution >= GreenMid && Data.coverageContribution < GreenHigh)
        {
            Green = "ÖÐ";
        }
        else
        {
            Green = "µÍ";
        }

        Text.text = $"{Data.cost}\n{Data.economics}\n{Data.growthTime}\n{Green}";
    }
}
