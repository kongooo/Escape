using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    private float change;
    Vector3 pos;        
    float nowPos;

    void Start ()
    {
        pos = transform.position;        //记录相机初始位置
	}
	
	void Update ()            //更新相机坐标
    {
       nowPos = transform.position.x;   //记录当前位置
        change = Input.GetAxisRaw("Horizontal") * 0.15f;   //记录输入值
               
        if (nowPos == 7.3f || nowPos == -7)   //到达边界时进行判断
        {
            if (nowPos == 7.3f && change > 0)
            {
                change = 0;
            }
            else if (change < 0)
            {
                change = Input.GetAxisRaw("Horizontal") * 0.1f;
            }
            if (nowPos == -7 && change < 0)
            {
                change = 0;
            }
            else if (change > 0)
            {
                change = Input.GetAxisRaw("Horizontal") * 0.1f;
            }
        }
       
        pos.x += change;                          //改变相机x轴坐标
        transform.position = pos;
        transform.position = new Vector3(                      //限制相机位置
            Mathf.Clamp(transform.position.x, -7f, 7.3f),
            0.0f,
            -10
            );
    }
}
