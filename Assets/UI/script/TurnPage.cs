using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnPage : MonoBehaviour
{
    public GameObject Text1;
    public GameObject Text2;
    public int amount = 1;
    private int page = 1;



    public void Turn(){
        if(amount == 1){
            gameObject.SetActive(false);
            Text1.SetActive(false);
            page = 1;
            Time.timeScale = (1);
        }
        if(amount == 2 && page == 1){
            Text1.SetActive(false);
            Text2.SetActive(true);
            page = 2;
            Time.timeScale = (0);
        }else if(amount == 2 && page == 2){
            gameObject.SetActive(false);
            Text1.SetActive(false);
            Text2.SetActive(false);
            page = 1;
            Time.timeScale = (1);
        }
    }
}
