using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowSeedDetails : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler   
{
    public float height = 281f;
    public float y = 143f;
    private Vector2 OriginPosition;
    private Vector2 OriginSize;



    private RectTransform RT;



    void Awake()
    {
        RT = GetComponent<RectTransform >();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        OriginPosition = RT.anchoredPosition;
        OriginSize = RT.sizeDelta;
        RT.anchoredPosition = new Vector2(0,RT.anchoredPosition.y + y);
        RT.sizeDelta = new Vector2(RT.sizeDelta.x , RT.sizeDelta.y + height);
    } 

    public void OnPointerExit(PointerEventData eventData)
    {
        RT.anchoredPosition = OriginPosition;
        RT.sizeDelta = OriginSize;
    }
}
