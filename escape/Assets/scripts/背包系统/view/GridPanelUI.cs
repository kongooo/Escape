using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPanelUI : MonoBehaviour {

    public  Transform [] GridTs;            //取得格子的Transform数组

    public Transform  GetEmptyItem()        //得到空格子
    {
        for(int i =0;i<GridTs.Length; i++)
        {
            if (GridTs[i].childCount == 0)      //若这个格子中没有子物体
                return GridTs[i];
        }
        return null;                       //没有空格子则返回null
    }
}
