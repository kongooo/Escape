using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPos : MonoBehaviour {

    private static SetPos Instance;
    public static SetPos _Instance
    {
        get
        {
            return Instance;
        }
    }

    private Transform camera;
    private float x,y;
    private void Awake()
    {
        Instance = this;
    }

    void Update () {               //设置背包图标的位置
        //获得可使用的相机的Transform
        camera = ClickManager._Instance.FindEnableCamera(Camera.allCameras).transform;
        transform.SetParent(camera.transform);
        transform.localPosition = new Vector3(13.8f, -7.3f, 10);
    }
   
}
