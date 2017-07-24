using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private static GameManager Instance;
    public static GameManager _Instance
    {
        get
        {
            return Instance;
        }
    }

    [HideInInspector] public int one;     //点燃得到的火把
    [HideInInspector] public int two;     //点燃墙上的火把
    [HideInInspector] public int three;       //启动箱子
    [HideInInspector] public int four;         //把火药洒在石门处
    [HideInInspector] public int five;        //把芯插在火药中
    [HideInInspector] public int fivePoint;   //用墙上的火把点燃插芯的火药
    [HideInInspector] public int six;        //用镐凿门
    [HideInInspector] public int seven;       //箱子打开
    [HideInInspector] public int eight;       //箱子下一层
    [HideInInspector] public int nine;        //把石块放到洞里
    [HideInInspector] public int ten;         //石门升起后点击出口
    [HideInInspector] public bool firstEnd=false;      //结局一
    [HideInInspector] public bool secondEnd=false;      //结局二

    private void Awake()
    {
        Instance = this;
    }
    


}
