using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid : MonoBehaviour {

    public Transform StartTransform;
    public Transform EndTransform;

    public float NodeRadius=0.5f;        //节点半径

    public LayerMask WallLayer;      //过滤墙体所在的层

    public class NodeItem              //节点类 储存每一个节点的信息
    {
        public bool isWall;
        public int x, y;          //节点坐标
        public Vector3 pos;       //位置
        public int gCost;         //起点花费
        public int hCost;         //终点花费
        public int fCost          //总花费
        {
            get
            {
                return gCost + fCost;
            }
        }        
        public NodeItem parent;    //父节点（中心节点）
        public NodeItem(bool  Iswall,int X,int Y,Vector3 Pos)    //初始化
        {
            this.x = X;
            this.y = Y;
            this.isWall = Iswall;
            this.pos = Pos;
        }
    }

    private NodeItem[,] Grid;    //二维数组

    private int w, h;            

    private GameObject Wall;
    private GameObject Path;

    private List<GameObject> PathObject = new List<GameObject>();

	void Awake ()
    {
        w = Mathf.RoundToInt(transform.localScale.x * 28);   
        h = Mathf.RoundToInt(transform.localScale.y * 31);   
        Grid = new NodeItem[w, h];                           //创建对应的节点二维数组

         Wall = new GameObject("WallRange");
         Path = new GameObject("PathRange");

        for(int x=2;x<w;x++)
            for(int y = 2; y < h; y++)
            {
                Vector3 pos = new Vector3(x, y, 0);
                bool isWall = Physics.CheckSphere(pos, NodeRadius,WallLayer);
                Grid[x, y] = new NodeItem(isWall, x, y, pos);      //构建节点
                if (isWall)
                {
                   //画出不可行走区域？
                }
            }
	}
    //根据坐标位置获得对应的节点
    public NodeItem GetGrid(Vector3 position)
    {
        //取得坐标的近似整数作为节点的序列
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.y);
        //限定x y范围
        x = Mathf.Clamp(x, 2, w - 1);
        y = Mathf.Clamp(y, 2, h - 1);
        return Grid[x, y];
    }
	
	
	void Update () {
		
	}
}
