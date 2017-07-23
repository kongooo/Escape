using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPath : MonoBehaviour {

    private grid Grid;
	
	void Start () {
        Grid = GetComponent<grid>();
	}
	
	
	void Update () {
        changepath(Grid.StartTransform.position, Grid.EndTransform.position);
	}

    void changepath(Vector3 s,Vector3 e)
    {
        grid.NodeItem StartNode = Grid.GetGrid(s);     //将开始和结束的坐标转换成节点坐标
        grid.NodeItem EndNode = Grid.GetGrid(e);

        //定义开启列表和关闭列表
        //开启列表：存储可以成为下一个父节点的节点
        //关闭列表：存储成为过父节点的节点
        List<grid.NodeItem> OpenList = new List<grid.NodeItem>();
        List<grid.NodeItem> CloseList = new List<grid.NodeItem>();

        OpenList.Add(StartNode);

        while (OpenList.Count > 0)
        {
            //将开启列表的第一个节点记录下来
            grid.NodeItem ParentNode = OpenList[0];

            //遍历开启列表寻找parent
            for(int i = 0; i < OpenList.Count; i++)
            {
                //若开启列表中的节点的总花费更小或不变并且到终点的花费更小
                if (OpenList[i].fCost <= ParentNode.fCost && OpenList[i].gCost < ParentNode.gCost)
                {
                    ParentNode = OpenList[i];
                }
            }

            //将找到的父节点从开启列表中删除，加入关闭列表
            OpenList.Remove(ParentNode);
            CloseList.Add(ParentNode);

            //若父节点为终点节点则停止更新路程花费并且生成路径
            if (ParentNode == EndNode)
            {
                //生成路径
                ShowPath(StartNode, EndNode);
                return;
            }

            List<grid.NodeItem> nodeneighborhood = Grid.GetNearNode(ParentNode);
            //更新花费
            for(int i = 0; i < nodeneighborhood.Count; i++)
            {
                //跳过在墙中的节点和已经关闭的节点
                if (nodeneighborhood[i].isWall || CloseList.Contains(ParentNode))
                {
                    continue;
                }
                //得到更新后的花费
                int newcost = ParentNode.gCost + updatecost(nodeneighborhood[i], ParentNode);
                //若更新后花费更少或者该节点原来不在开启节点中则更新
                if (newcost < nodeneighborhood[i].gCost || !OpenList.Contains(nodeneighborhood[i]))
                {
                    //更新到起点花费
                    nodeneighborhood[i].gCost = newcost;
                    //更新到终点花费
                    nodeneighborhood[i].hCost = updatecost(nodeneighborhood[i], EndNode);
                    //更新父节点
                    nodeneighborhood[i].parent = ParentNode;
                    //若该节点不在开启节点中就把它放进去
                    if (!OpenList.Contains(nodeneighborhood[i]))
                    {
                        OpenList.Add(nodeneighborhood[i]);
                    }
                }
            }
        }
    }

    //生成路径为从结束节点处开始依次寻找其父节点直到开始节点处停止
    void ShowPath(grid.NodeItem startnode,grid.NodeItem endnode)
    {
        //定义存储路径的列表
        List<grid.NodeItem> path = new List<grid.NodeItem>();
        //当结束节点不为空时
        if (endnode != null)
        {
            //存储父节点的变量
            grid.NodeItem temp = endnode;
            //当没有达到开始节点时
            while (temp != startnode)
            {
                //向存储路径的列表中添加父节点
                path.Add(temp);
                //更新父节点
                temp = temp.parent;
            }
            //反转路径（之前是一条从结束节点到开始节点的路）
            path.Reverse();
        }
        //更新路径
        Grid.upadatePath(path);
    }

    int updatecost(grid.NodeItem s,grid.NodeItem e)
    {
        int x = Mathf.Abs(s.x - e.x);
        int y = Mathf.Abs(s.y - e.y);
        if (x > y)
        {
            return y * 10 + (x - y) * 14;
        }
        return x * 10 + (y - x) * 14;
    }
}
