using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items  : MonoBehaviour
{
    //父类   背包中物品拥有的属性(除ItemType外均为只读)
    public int ID { get; private set; }     //物品ID  private保护这个属性,防止被外部更改
    public string Name { get; private set; }
    
    public  string ItemType { get; protected  set; }  //只有访问时通过子类发生时，protected类型才能被访问
    public  Sprite PackItemImage { get; private set; }

    //构造函数  初始化数据
    public Items (int id,string name,Sprite sprite)
    {
        this.ID = id;
        this.Name = name;
        
        this.PackItemImage = sprite;
    }
}
