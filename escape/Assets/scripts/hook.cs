using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hook : MonoBehaviour {

    public Camera CabinetCamera;

    public Transform[] hooks;

    float[] hookPosx;

    bool ismove=true;
	
	void Awake () {
        StartCoroutine(OnMouseDown());
        hookPosx = new float[3];
        
    }
	
	
	void Update () {
        for (int i = 0; i < hooks.Length; i++)
        {
            hookPosx[i] = hooks[i].position.x;
        }
        if (ismove)
        {
            if ((hookPosx[0] > hookPosx[1]) && (hookPosx[1] > hookPosx[2]))
            {
                ClickManager._Instance.Open.Play();
                GameManager._Instance.three = 3;
                ismove = false;
            }
        }
        transform.localPosition = new Vector3(
            Mathf.Clamp(transform.localPosition.x, -4.9f, 4.3f),
            transform.localPosition.y,
            transform.localPosition.z);
	}

    private IEnumerator OnMouseDown()
    {
       
        //得到物体的屏幕坐标
        Vector3 screenSpace = CabinetCamera. WorldToScreenPoint(transform.position);  
        //获得物体和鼠标的世界坐标的差值（鼠标的屏幕坐标的z轴用物体的屏幕坐标的z轴）
        Vector3 offset = transform.position - CabinetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
        while (Input.GetMouseButton(0))
        {
            //得到现在鼠标的三维屏幕坐标
            Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
            Vector3 newpos = CabinetCamera.ScreenToWorldPoint(curScreenSpace) + offset;
            float xnow = newpos.x;
            //将当前鼠标的屏幕坐标转换成世界坐标，再加上点击前鼠标和物体的世界坐标的差
            Vector3 curPosition = new Vector3(xnow, transform.position.y, transform.position.z);
            
            transform.position = curPosition;
            yield return new WaitForFixedUpdate(); //循环执行
        }
    }

}
