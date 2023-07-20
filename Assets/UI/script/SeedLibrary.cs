using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedLibrary : MonoBehaviour
{
    public GameObject Detail;
    public GameObject Sort;
    public GameObject Icon;
    public GameObject Library;

    public void SeedLibraryClick()
    {
        Library.SetActive(false);
        Sort.SetActive(true);
        Icon.SetActive(true);
    }

    public void Return()
    {
        Library.SetActive(true);
        Sort.SetActive(false);
        Icon.SetActive(false);
    }
}
