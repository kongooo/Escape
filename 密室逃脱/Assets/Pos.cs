using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pos : MonoBehaviour {

    public Camera main;

    private static Pos Instance;
    public static Pos _Instance
    {
        get
        {
            return Instance;
        }
    }

    private Transform camera;
    private float x, y;
    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {               //设置背包图标的位置
        //获得可使用的相机的Transform
        camera = ClickManager._Instance.FindEnableCamera(Camera.allCameras).transform;
        transform.SetParent(camera.transform);
        transform.localPosition = new Vector3(-7f, -3.2f, 10);
        if (transform.parent == main.transform||ClickManager._Instance.isEnd==true)
        {          
            var spr= gameObject.GetComponent<SpriteRenderer>();
            var co = spr.color;
            co.a = 0;
            gameObject.GetComponent<SpriteRenderer>().color = co;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            var spr = gameObject.GetComponent<SpriteRenderer>();
            var co = spr.color;
            co.a = 1;
            gameObject.GetComponent<SpriteRenderer>().color = co;
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
