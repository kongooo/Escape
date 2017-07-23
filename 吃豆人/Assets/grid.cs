using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid : MonoBehaviour {

    public GameObject node;       //ai的路标

    public Transform StartTransform;
    public Transform EndTransform;

    public float NodeRadius=1f;        //节点半径

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
                return gCost + hCost;
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

    //在Hierarchy中管理生成路标的物体
    private GameObject Path;

    private List<GameObject> PathObject = new List<GameObject>();

	void Awake ()
    {
        w = Mathf.RoundToInt(transform.localScale.x * 28);   
        h = Mathf.RoundToInt(transform.localScale.y * 31);   
        Grid = new NodeItem[w, h];                           //创建对应的节点二维数组

        Debug.Log(w + " " + h);
        Path = new GameObject("PathRange");

        for(int x=2;x<w;x++)
            for(int y = 2; y < h; y++)
            {
                Vector3 pos = new Vector3(x, y, 0);
                
                bool isWall = Physics.CheckSphere(pos, NodeRadius,WallLayer);
                Debug.Log(isWall);
                Grid[x, y] = new NodeItem(isWall, x, y, pos);      //构建节点
                
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
	

    //得到周边节点
    public List<NodeItem> GetNearNode(NodeItem node)
    {
        List<NodeItem> NearList = new List<NodeItem>();
        for(int i=-1;i<2;i++)
            for(int j = -1; j < 2; j++)
            {
                if (i == 0 && j == 0)                 //跳过自身
                    continue;
                else
                {
                    int x = node.x + i;
                    int y = node.y + j;
                    if (x > 1 && x < w && y > 1 && y < h) //不超过边界则放到集合中
                    {
                        NearList.Add(Grid[x, y]);
                    }                    
                }
            }
        return NearList;
    }

    //更新路标
    public void upadatePath(List<NodeItem> path)
    {
        path = new List<NodeItem>();
        //得到新生成的路径的长度
        int newpathlength = path.Count;
        //当新生成的路径的长度大于场景中的路径路标的数量时       
        if (newpathlength > PathObject.Count)
        {
            for(int i = PathObject.Count; i < newpathlength; i++)
            {
                //实例化多出来的数量的物体
                GameObject newpathobj = Instantiate(node, path[i].pos, Quaternion.identity);
                //方便在Hierarchy中管理
                newpathobj.transform.SetParent(Path.transform);
            }            
        }
        else
        {
            for(int i = 0; i < PathObject.Count; i++)
            {
                //激活场景中存在的未被激活的路标
                PathObject[i].SetActive(true);
                //更改场景中已经存在的路标的位置
                PathObject[i].transform.position = path[i].pos;
            }
        }
        for(int i = newpathlength; i < PathObject.Count; i++)
        {
            //使多余的路标不可见
            PathObject[i].SetActive(false);
        }
    }
    
	
}
