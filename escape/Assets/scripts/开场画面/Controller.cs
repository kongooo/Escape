using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public GameObject word1;
    public GameObject word2;
    public GameObject word3;
    public GameObject sprite;

    private int[] storyTime = { 2, 8, 12, 19 };    //控制时间数组
    private int index = 0;

    private bool next = false;

    void Start()
    {
        word1.SetActive(false);
        word2.SetActive(false);
        word3.SetActive(false);
        sprite.SetActive(false);
    }


    void Update()
    {
        if (index < storyTime.Length && Time.timeSinceLevelLoad > storyTime[index])  //根据时间依次出现
        {
            OnTime(index);
            index++;
        }
        if(Time.timeSinceLevelLoad>24)
        {
            Application.LoadLevel("Main");
        }
    }
    void OnTime(int id)    //控制出现顺序
    {
        switch (id)
        {
            case 0:
                word1.SetActive(true);
                break;
            case 1:
                word2.SetActive(true);
                break;
            case 2:
                sprite.SetActive(true);
                break;
            case 3:
                word3.SetActive(true);
                break;
            
        }
    }
}
