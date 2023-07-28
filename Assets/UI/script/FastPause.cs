using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastPause : MonoBehaviour
{
    public int order;
    public RectTransform CoverRT;
    private RectTransform RT;

    
    private void Awake() {
        RT = GetComponent<RectTransform>();
    }
    public void Pause(){
        Time.timeScale = (0);
    }
    public void Continue(){
        Time.timeScale = (1);
    }
    public void TwoFast(){
        Time.timeScale = (2);
    }
    public void ThreeFast(){
        Time.timeScale = (3);
    }
    private void Light(){
        if(order == 0 && Time.timeScale == 0){
            CoverRT.anchoredPosition = RT.anchoredPosition;
        }
        if(order == 1 && Time.timeScale == 1){
            CoverRT.anchoredPosition = RT.anchoredPosition;
        }
        if(order == 2 && Time.timeScale == 2){
            CoverRT.anchoredPosition = RT.anchoredPosition;
        }
        if(order == 3 && Time.timeScale == 3){
            CoverRT.anchoredPosition = RT.anchoredPosition;
        }
            
    }
    void Update(){
        Light();
    }
}
