using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid : MonoBehaviour {

    public Transform StartTransform;
    public Transform EndTransform;

    public float NodeRadius;        //节点半径

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
        w = Mathf.RoundToInt(transform.localScale.x * 26);   //节点列数
        h = Mathf.RoundToInt(transform.localScale.y * 28);   //节点行数
        Grid = new NodeItem[w, h];                           //创建对应的节点二维数组

	}
	
	
	void Update () {
		
	}
}
