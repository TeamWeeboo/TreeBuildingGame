using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SeedDetails : MonoBehaviour
{
    public GameObject Detail;



    public void OnPointerEnter(PointerEventData PointerEvent)
    {
        Detail.SetActive(true);
    } 

    public void OnPointerExit(PointerEventData Pointerevent)
    {
        Detail.SetActive(false);
    }
}
