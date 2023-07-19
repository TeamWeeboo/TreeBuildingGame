using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscMenu : MonoBehaviour
{
    public GameObject Menu;
    private bool MenuKey = true;
    public AudioSource Bgm;

    void Update()
    {

        if(MenuKey == true){
            if(Input.GetKeyDown(KeyCode.Escape)){
            Menu.SetActive(true);
            MenuKey = false;
            Time.timeScale = (0);
            Bgm.Pause();
            }
        }else if(Input.GetKeyDown(KeyCode.Escape)){
            Menu.SetActive(false);
            MenuKey = true;
            Time.timeScale = (1);
            Bgm.Pause();
        }
    }

    public void Return(){
        Menu.SetActive(false);
        MenuKey = true;
        Time.timeScale = (1);
        Bgm.Pause();
    }
    public void Option(){
        Debug.Log("111");
    }
    public void QuitGame(){
        Application.Quit();
    }
}
