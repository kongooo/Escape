using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ToolTipUI : MonoBehaviour {

    public Text outText;
    public Text InText;

    public void updateToolTip(string text)    //更新提示框
    {
        outText.text = text;
        InText.text = text;
    }
   
    public void show( )                    //提示框的显示
    {
        gameObject.SetActive(true);
    }

    public void hidden()                 //提示框的隐藏
    {
        gameObject.SetActive(false);
    }

    public  void SetLocalPos(Vector2 position)   //设置提示框的位置
    {
        transform.localPosition = position;
    }
}
