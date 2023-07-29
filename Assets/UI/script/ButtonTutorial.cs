using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTutorial : MonoBehaviour
{
    public GameObject tutorial;

    void Start()
    {
        tutorial.SetActive(false);
    }

    public void ShowTutorial(){
        Time.timeScale = (0);
        tutorial.SetActive(true);
        Destroy(this);
   }
}
