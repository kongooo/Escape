using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour {

    private static OnClick Instance;
  
    public static OnClick Instace_
    {
        get
        {
            return Instance;
        }
    }

    private string ItemName;
    
    private string Name;
    
	void Start () {
       
        Instance = this;
        Name = gameObject.name;      //取得被点击的格子自身的名字
	}
	
	
	void Update () {
		
	}

    public void Onclick()
    {

        if (transform.childCount == 0)
                return;
            else
            {
                GameObject.Find("Canvas").GetComponent<Canvas>().planeDistance = 0;  //点击背包中物品后背包消失
                ClickManager._Instance.GridName = Name;               //传递格子自身的信息给ClickManager
                ClickManager._Instance.OnclickObj = gameObject;

                Items item = ItemModel.GetItem(Name);
                ItemName = item.Name;


                switch (ItemName)
                {

                    case "未点燃的火把":
                        GameManager._Instance.one = 1;
                        break;
                case "已经点燃的火把":
                    GameManager._Instance.two = 2;
                    break;
                case "一小撮火药":
                        GameManager._Instance.four = 4;
                        break;
                    case "油灯里面的灯芯":
                        GameManager._Instance.five = 5;
                        break;
                    case "一把放在地上的镐，看起来很锋利的样子":
                        GameManager._Instance.six = 6;
                        break;
                    case "箱子里的小石块":
                        GameManager._Instance.nine = 9;
                        break;
                case "点燃的墙上的火把":
                    GameManager._Instance.fivePoint = 11;
                    break;


            }
            }
        

        
    }

}
