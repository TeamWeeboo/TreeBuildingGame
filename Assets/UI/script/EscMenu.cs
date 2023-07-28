using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscMenu : MonoBehaviour
{
    public GameObject Menu;
    private bool MenuKey = true;

    void Update()
    {

        if(MenuKey == true){
            if(Input.GetKeyDown(KeyCode.Escape)){
            Menu.SetActive(true);
            MenuKey = false;
            Time.timeScale = (0);
            }
        }else if(Input.GetKeyDown(KeyCode.Escape)){
            Menu.SetActive(false);
            MenuKey = true;
            Time.timeScale = (1);
        }
    }

    public void Return(){
        Menu.SetActive(false);
        MenuKey = true;
        Time.timeScale = (1);
    }
    public void ClickMenu(){
        Menu.SetActive(true);
        MenuKey = false;
        Time.timeScale = (0);
    }
}
