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

        tutorial.SetActive(true);
        Destroy(this);
   }
}
