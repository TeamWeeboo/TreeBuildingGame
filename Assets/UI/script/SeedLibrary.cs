using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Placement;
using Gameplay.Progression;
using Gameplay;

public class SeedLibrary : MonoBehaviour
{
    public GameObject Detail;
    public GameObject Sort1;
    public GameObject Sort2;
    public GameObject Sort3;
    public GameObject Sort4;
    public GameObject Sorts;
    public GameObject Library;
    public TreeData treedata;
    public GameObject Tree1;



    public void SeedLibraryClick()
    {
        Library.SetActive(false);
        Sort1.SetActive(true);
        Sorts.SetActive(true);
    }

    public void Return()
    {
        Library.SetActive(true);
        Sort1.SetActive(false);
        Sort2.SetActive(false);
        Sort3.SetActive(false);
        Sort4.SetActive(false);
        Sorts.SetActive(false);
    }
    public void SeedSelet()
    {
        TreePlacementCommand myCommand = treedata.command;
        PlacementController.instance.commandData = myCommand;
    }

    public void SeedSort1()
    {
        Sort1.SetActive(true);
        Sort2.SetActive(false);
        Sort3.SetActive(false);
        Sort4.SetActive(false);
    }
    public void SeedSort2()
    {
        Sort1.SetActive(false);
        Sort2.SetActive(true);
        Sort3.SetActive(false);
        Sort4.SetActive(false);
    }
    public void SeedSort3()
    {
        Sort1.SetActive(false);
        Sort2.SetActive(false);
        Sort3.SetActive(true);
        Sort4.SetActive(false);
    }

    public void SeedSort4()
    {
        Sort1.SetActive(false);
        Sort2.SetActive(false);
        Sort3.SetActive(false);
        Sort4.SetActive(true);
    }
}
