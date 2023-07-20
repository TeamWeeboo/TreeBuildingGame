using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Placement;
using Gameplay.Progression;
using Gameplay;
using Unity.VisualScripting;

public class SeedLibrary : MonoBehaviour
{
    public GameObject Detail;
    public GameObject Sort1;
    public GameObject Sort2;
    public GameObject Sort3;
    public GameObject Sort4;
    public GameObject Sorts;
    public GameObject Library;
    public TreeData BaiYang;
    public TreeData HanLiu;
    public TreeData ZhangZiSong;
    public TreeData PingGuoShu;
    public TreeData YouHao;
    public TreeData ShaLiu;
    public TreeData GuaiLiu;
    public TreeData JiJICao;
    public TreeData MuSu;
    public TreeData YuMi;
    public TreeData XiangRiKui;



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

    public void SeedSeletBaiYang()
    {
        PlacementController.instance.commandData = BaiYang.command;
    }

    public void SeedSeletHanLiu()
    {
        PlacementController.instance.commandData = HanLiu.command;
    }
    public void SeedSeletZhangZiSong()
    {
        PlacementController.instance.commandData = ZhangZiSong.command;
    }
    public void SeedSeletPingGuoShu()
    {
        PlacementController.instance.commandData = PingGuoShu.command;
    }
    public void SeedSeletYouHao()
    {
        PlacementController.instance.commandData = YouHao.command;
    }
    
    public void SeedSeletShaLiu()
    {
        PlacementController.instance.commandData = ShaLiu.command;
    }
    public void SeedSeletGuaiLiu()
    {
        PlacementController.instance.commandData = GuaiLiu.command;
    }
    public void SeedSeletJiJICao()
    {
        PlacementController.instance.commandData = JiJICao.command;
    }
    public void SeedSeletMuSu()
    {
        PlacementController.instance.commandData = MuSu.command;
    }
    public void SeedSeletYuMi()
    {
        PlacementController.instance.commandData = YuMi.command;
    }
    public void SeedSeletXiangRiKui()
    {
        PlacementController.instance.commandData = XiangRiKui.command;
    }
}
