using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zhuizhu : blinkyBaseState
{
    private BlinkyClass Blinky;

    private LoadPath startchase;
    

    public zhuizhu(BlinkyClass _blinky)
    {
        Debug.Log("开始追逐！");
        Blinky = _blinky;
    }

    public void ChangeState()
    {
        Vector2 offset = startchase.Grid.StartTransform.position - startchase.Grid.EndTransform.position;
        if (offset.magnitude > 10)
            Blinky.SetBlinkyState(new Xunluo(Blinky));
    }

    public void myupdate()
    {
        startchase.changepath(startchase.Grid.StartTransform.position, startchase.Grid.EndTransform.position);
    }
}
