using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class happen : MonoBehaviour {
    public float duration1;
    public float duration2;
    public float duration3;
    private float timeAdd = 0;

    private void Start()
    {
        setAlpha(0);
    }
    void Update()
    {
        timeAdd += Time.deltaTime;
        if (duration1 > timeAdd)        //在duration时间内逐渐增加透明度
        {
            setAlpha(timeAdd / duration1);
        }
        else if (duration2 > timeAdd)
        {
            setAlpha(1);
        }
        else if(duration3>timeAdd)
        {
            setAlpha(duration3 - timeAdd / duration1);
        }
    }
    void setAlpha(float A)
    {
        var sprite = GetComponent<SpriteRenderer>();   //获取图片组件
        var co = sprite.color;                         //获取组件的color
        co.a = A;                                     //更改color的透明度
        sprite.color = co;                             //把更改后的color赋值回图片组件
    }
}
