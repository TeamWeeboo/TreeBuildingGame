using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedDetails : MonoBehaviour
{
    public GameObject Detail;



    public void OnPointerEnter()
    {
        Detail.SetActive(true);
    } 

    public void OnPointerExit()
    {
        Detail.SetActive(false);
    }
}
