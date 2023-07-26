using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowSeedDetails : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler   
{
    public int order = 1; 
    public RectTransform Order1;
    public RectTransform Order2;
    public RectTransform Order3;
    public RectTransform Order4;

    private float height = 281.8f;
    private float y = 140f;
    private Vector2 OriginPosition;
    private Vector2 OriginSize;
    private RectTransform RT;
    private float Y1 = 301f;
    private float Y2 = 225f;
    private float Y3 = 149f;
    private float Y4 = 73f;



    void Awake()
    {
        RT = GetComponent<RectTransform >();
        Initialize();
        OriginPosition = RT.anchoredPosition;
        OriginSize = RT.sizeDelta;
        
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if(order == 1){
            if(RT.anchoredPosition == OriginPosition){
            RT.anchoredPosition = new Vector2(0,Y1);
            RT.sizeDelta = new Vector2(RT.sizeDelta.x , 357.8f);
            }else {
            RT.anchoredPosition = new Vector2(0,Y1);
            RT.sizeDelta = new Vector2(RT.sizeDelta.x , 357.8f);
            Order2.anchoredPosition = new Vector2(0,85f);
            Order2.sizeDelta = new Vector2(881.5f, 76.8f);
            Order3.anchoredPosition = new Vector2(0,9f);
            Order3.sizeDelta = new Vector2(881.5f, 76.8f);
            Order4.anchoredPosition = new Vector2(0,-67f);
            Order4.sizeDelta = new Vector2(881.5f, 76.8f);
            }
        }
        if(order == 2){
            if(RT.anchoredPosition == OriginPosition){
            RT.anchoredPosition = new Vector2(0,Y2);
            RT.sizeDelta = new Vector2(RT.sizeDelta.x , 357.8f);
            Order1.anchoredPosition = new Vector2(0,441.8f);
            Order1.sizeDelta = new Vector2(881.5f, 76.8f);
            }else{
                RT.anchoredPosition = new Vector2(0,Y2);
                RT.sizeDelta = new Vector2(RT.sizeDelta.x , 357.8f);
                Order1.anchoredPosition = new Vector2(0,160f);
                Order1.sizeDelta = new Vector2(881.5f, 76.8f);
                Order3.anchoredPosition = new Vector2(0,9f);
                Order3.sizeDelta = new Vector2(881.5f, 76.8f);
                Order4.anchoredPosition = new Vector2(0,-67f);
                Order4.sizeDelta = new Vector2(881.5f, 76.8f);
            }
        } 
        if(order == 3){
            if(RT.anchoredPosition == OriginPosition){
            RT.anchoredPosition = new Vector2(0,Y3);
            RT.sizeDelta = new Vector2(RT.sizeDelta.x , 357.8f);
            Order1.anchoredPosition = new Vector2(0,441.8f);
            Order1.sizeDelta = new Vector2(881.5f, 76.8f);
            Order2.anchoredPosition = new Vector2(0,85f + height);
            Order2.sizeDelta = new Vector2(881.5f, 76.8f);
            }else {
            RT.anchoredPosition = new Vector2(0,Y3);
            RT.sizeDelta = new Vector2(RT.sizeDelta.x , 357.8f);
            Order1.anchoredPosition = new Vector2(0,441.8f);
            Order1.sizeDelta = new Vector2(881.5f, 76.8f);
            Order2.anchoredPosition = new Vector2(0,85f + height);
            Order2.sizeDelta = new Vector2(881.5f, 76.8f);
            Order4.anchoredPosition = new Vector2(0,-67f);
            Order4.sizeDelta = new Vector2(881.5f, 76.8f);
            }
        }
        if(order == 4){
            if(RT.anchoredPosition == OriginPosition){
            RT.anchoredPosition = new Vector2(0,Y4);
            RT.sizeDelta = new Vector2(RT.sizeDelta.x , 357.8f);
            Order1.anchoredPosition = new Vector2(0,441.8f);
            Order1.sizeDelta = new Vector2(881.5f, 76.8f);
            Order2.anchoredPosition = new Vector2(0,85f + height);
            Order2.sizeDelta = new Vector2(881.5f, 76.8f);
            Order3.anchoredPosition = new Vector2(0,9f + height);
            Order3.sizeDelta = new Vector2(881.5f, 76.8f);
            }else {
            RT.anchoredPosition = new Vector2(0,Y4);
            RT.sizeDelta = new Vector2(RT.sizeDelta.x , 357.8f);
            Order1.anchoredPosition = new Vector2(0,441.8f);
            Order1.sizeDelta = new Vector2(881.5f, 76.8f);
            Order2.anchoredPosition = new Vector2(0,85f + height);
            Order2.sizeDelta = new Vector2(881.5f, 76.8f);
            Order3.anchoredPosition = new Vector2(0,9f + height);
            Order3.sizeDelta = new Vector2(881.5f, 76.8f);
            }
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
            Order1.anchoredPosition = new Vector2(0,160f);
            Order1.sizeDelta = new Vector2(881.5f, 76.8f);
            Order2.anchoredPosition = new Vector2(0,85f);
            Order2.sizeDelta = new Vector2(881.5f, 76.8f);
            Order3.anchoredPosition = new Vector2(0,9f);
            Order3.sizeDelta = new Vector2(881.5f, 76.8f);
            Order4.anchoredPosition = new Vector2(0,-67f);
            Order4.sizeDelta = new Vector2(881.5f, 76.8f);        
    }
    void Initialize(){
        if(order == 1){
            RT.anchoredPosition = new Vector2(0,160f);
            RT.sizeDelta = new Vector2(881.5f, 76.8f);
        }
        if(order == 2){
            RT.anchoredPosition = new Vector2(0,85f);
            RT.sizeDelta = new Vector2(881.5f, 76.8f);
        }
        if(order == 3){
            RT.anchoredPosition = new Vector2(0,9f);
            RT.sizeDelta = new Vector2(881.5f, 76.8f);
        }
        if(order == 4){
            RT.anchoredPosition = new Vector2(0,-67f);
            RT.sizeDelta = new Vector2(881.5f, 76.8f);
        }
    }
}
