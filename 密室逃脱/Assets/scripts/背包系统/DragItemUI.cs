using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragItemUI : PackageItemUI {

    public void show()                    //显示拖拽物品
    {
        gameObject.SetActive(true);
    }

    public void hidden()                  //隐藏拖拽物品
    {
        gameObject.SetActive(false);
    }

    public void SetLocalPos(Vector2 position)   //设置拖拽物品的局部坐标

    {
        transform.localPosition = position;
    }

}
