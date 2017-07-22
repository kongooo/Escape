using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xunluo : MonoBehaviour, blinkyBaseState
{
    public Transform[] waypoints;

    int cur = 0;

    public float speed = 0.3f;
    public void ChangeState()
    {
        //if ()      //发现吃豆人则改变到追逐状态
        //{

        //}
    }

    public void Update()
    {
        
    }
    void FixedUpdate()
    {

        //数组设置关键点，实现敌人的移动
        if (transform.position != waypoints[cur].position)
        {
            Vector2 p = Vector2.MoveTowards(transform.position, waypoints[cur].position, speed);

            GetComponent<Rigidbody2D>().MovePosition(p);
        }

        else cur = (cur + 1) % waypoints.Length;


        //动画判断
        Vector2 dir = waypoints[cur].position - transform.position;

        GetComponent<Animator>().SetFloat("DirX", dir.x);

        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }
    void OnTriggerEnter2D(Collider2D co)
    {

        if (co.name == "pacman")
            Destroy(co.gameObject);
    }
}
