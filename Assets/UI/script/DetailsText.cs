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
    private int GreenHigh = 10;
    private int GreenMid = 4;
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
            Green = "HIGH";
        }
        else if (Data.coverageContribution >= GreenMid && Data.coverageContribution < GreenHigh)
        {
            Green = "MID";
        }
        else
        {
            Green = "LOW";
        }

        Text.text = $"种植花费·Planting Cost:\t\t{Data.cost}¥\n经济效益·economic benefits:\t{Data.economics}¥\n生长时间·growth time：\t\t{Data.growthTime}s\n绿化值·Green value：\t\t{Green}\n存活率·survival rate↓";
    }
}
