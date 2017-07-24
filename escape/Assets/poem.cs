using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poem : MonoBehaviour {

    public AudioSource Wood;

    public Camera BoxCamera;

    public Transform[] poems;

    public Transform[] FixPos;

    private bool[] IsOut;

    
	
	void Awake () {
        IsOut = new bool[5];
        GameObject.Find("打开的箱子").GetComponent<Collider>().enabled = false;
        StartCoroutine(OnMouseDown());      
	}
	
	
	void Update () {        
        for(int i = 0; i < IsOut.Length; i++)      //判断每个物体是否在相应的位置
        {
            IsOut[i] = JudgePos(FixPos[i], poems[i]);           
        }
        //若都在相应的位置则启动下一层
        if (IsOut[0] == false && IsOut[1] == false && IsOut[2] == false && IsOut[3] == false && IsOut[4] == false)
        {
            GameManager._Instance.eight = 8;
            GameObject.Find("打开的箱子").GetComponent<Collider>().enabled = true;
        }
            
        transform.localPosition = (new Vector3              //使木块始终在箱子内
            (Mathf.Clamp(transform.localPosition.x, -9.8f, 9.8f),
            Mathf.Clamp(transform.localPosition.y, -5.4f, -1.9f),
            transform.localPosition.z
            ));
    }
    private IEnumerator OnMouseDown()
    {

        //得到物体的屏幕坐标
        Vector3 screenSpace = BoxCamera.WorldToScreenPoint(transform.position);
        //获得物体和鼠标的世界坐标的差值（鼠标的屏幕坐标的z轴用物体的屏幕坐标的z轴）
        Vector3 offset = transform.position - BoxCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
        while (Input.GetMouseButton(0))
        {
            
            //得到现在鼠标的三维屏幕坐标
            Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
            //将当前鼠标的屏幕坐标转换成世界坐标，再加上点击前鼠标和物体的世界坐标的差
            Vector3 curPosition = BoxCamera.ScreenToWorldPoint(curScreenSpace) + offset;

            transform.position = curPosition;
            yield return new WaitForFixedUpdate(); //循环执行
        }
    }
     
    private bool JudgePos(Transform fixPos, Transform ObjPos) 
    {
        
        Vector3 offset;
        offset= fixPos.position - ObjPos.position;
        if (offset.magnitude < 0.5)
                return false;
        else
        return true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Wood.Play();
    }
}

