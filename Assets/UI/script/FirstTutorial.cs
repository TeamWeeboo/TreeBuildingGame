using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTutorial : MonoBehaviour
{
    public GameObject BackGround;
    private void Awake() {
        BackGround.SetActive(true);
        Time.timeScale = (0);
    }

}
