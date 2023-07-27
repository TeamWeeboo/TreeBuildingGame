using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastPause : MonoBehaviour
{

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
}
