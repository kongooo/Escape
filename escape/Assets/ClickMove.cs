using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMove : MonoBehaviour {

    public int number;         //取得当前格子的序号

	public void JudgePos()
    {
        for (int i = 0; i < RandomAdd._Instance.PuzzleGrids.Length; i++)
        {
            int value = Mathf.Abs(number - i);  
            if (value == 1 || value == 3)       //判断是否为邻近的格子
            {
                GameObject puzzlegrid = RandomAdd._Instance.PuzzleGrids[i];
                if (puzzlegrid.transform.childCount == 0)         //找到邻近的格子中子物体为0的格子
                {
                    gameObject.transform.GetChild(0).SetParent(puzzlegrid.transform);

                    GameObject puzzlegridChild = puzzlegrid.transform.GetChild(0).gameObject;
                    puzzlegridChild.transform.localPosition = Vector3.zero;
                    puzzlegridChild.transform.localScale = Vector3.zero;
                    puzzlegridChild.transform.transform.localScale = new Vector3(1, 1, 0);
                    if (gameObject.transform.childCount != 0)
                        Destroy(gameObject.transform.GetChild(0).gameObject);
                }
            }
            else continue;
        }
    }
}
