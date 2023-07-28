using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastPause : MonoBehaviour
{
    public int order;
    private Image image;
    
    private void Awake() {
        image = GetComponent<Image>();
        image.material = Instantiate(image.material);
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
        if(order == 0){
            if(Time.timeScale == 0){
                image.material.SetFloat("_Outline",1);
            }else image.material.SetFloat("_Outline",0);
        }
        if(order == 1){
            if(Time.timeScale == 1){
                image.material.SetFloat("_Outline",1);
            }else image.material.SetFloat("_Outline",0);
        }
        if(order == 2){
            if(Time.timeScale == 2){
                image.material.SetFloat("_Outline",1);
            }else image.material.SetFloat("_Outline",0);
        }
        if(order == 3){
            if(Time.timeScale == 3){
                image.material.SetFloat("_Outline",1);
            }else image.material.SetFloat("_Outline",0);
        }
    }
    void Update(){
        Light();
    }
}
