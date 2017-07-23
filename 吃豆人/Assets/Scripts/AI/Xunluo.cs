using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xunluo :  blinkyBaseState
{
    private BlinkyClass blinky;

    private GameObject pacman;
    private GameObject Blinky;

    private Transform mytrans;

    private Transform[] waypoints;

    int cur = 0;

    public float speed = 0.3f;

    
    public Xunluo(BlinkyClass _blinky)
    {
        
        pacman = GameObject.Find("pacman");
        Blinky = GameObject.Find("blinky");
        blinky = _blinky;
        Debug.Log("开始巡逻！");
        
        mytrans = Blinky.GetComponent<Transform>();
    }

    public void ChangeState()
    {       
        bool ispackman=false;
        Vector2[] direction = new Vector2[4];
        direction[0] = new Vector2(1, 0);
        direction[1] = new Vector2(-1, 0);
        direction[2] = new Vector2(0, 1);
        direction[3] = new Vector2(0, -1);
        Blinky. GetComponent<CircleCollider2D>().enabled = false;
        for(int i = 0; i < direction.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(mytrans.position, direction[i], 10);
            if (hit.collider.gameObject.tag == "PacMan")
                ispackman = true;
        }
        Blinky.GetComponent<CircleCollider2D>().enabled = true;
        if (ispackman)    //发现pacman时状态改变
        {
            blinky.SetBlinkyState(new zhuizhu(blinky));
        }
    }


    public void myupdate()
    {
        
        Debug.Log("正在巡逻");
        if (mytrans.position != waypoints[cur].position)
        {
            Vector2 p = Vector2.MoveTowards(mytrans.position, waypoints[cur].position, speed);

            Blinky.GetComponent<Rigidbody2D>().MovePosition(p);

        }
        else


            cur = (cur + 1) % waypoints.Length;


        //动画判断
        Vector2 dir = waypoints[cur].position - mytrans.position;

        Blinky. GetComponent<Animator>().SetFloat("DirX", dir.x);

        Blinky. GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    
}
