using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomAdd : MonoBehaviour {
    private static RandomAdd Instance;
    public static RandomAdd _Instance
    {
        get
        {
            return Instance;
        }
    }


    public List<GameObject>  Puzzles = new List<GameObject>();   //初始化拼图
    private GameObject[] PuzzlesJudge;
    public GameObject[] PuzzleGrids;
    public int indx;
    
	void Awake () {
        PuzzlesJudge = new GameObject[8];
        Instance = this;
        
        indx = Random.Range(0, PuzzleGrids.Length-1);   //保持最后一块总有拼图，防止随机出来拼好的拼图
		for(int i = 0; i < PuzzleGrids.Length; i++)
        {
           
            if (i == indx) continue;               //随机选择空出的格子（除了最后一块）
            GameObject FixPuzzle = Instantiate( GetRandomPuzzle(Puzzles));
            FixPuzzle.transform.SetParent(PuzzleGrids[i].transform);
            FixPuzzle.transform.localPosition = Vector3.zero;
            FixPuzzle.transform.localScale = Vector3.zero;
            FixPuzzle.transform.localScale = new Vector3(1, 1, 0);
        }
        PuzzlesJudge[0] = GameObject.Find("Puzzle(Clone)");
        PuzzlesJudge[1] = GameObject.Find("Puzzle1(Clone)");
        PuzzlesJudge[2] = GameObject.Find("Puzzle2(Clone)");
        PuzzlesJudge[3] = GameObject.Find("Puzzle3(Clone)");
        PuzzlesJudge[4] = GameObject.Find("Puzzle4(Clone)");
        PuzzlesJudge[5] = GameObject.Find("Puzzle5(Clone)");
        PuzzlesJudge[6] = GameObject.Find("Puzzle6(Clone)");
        PuzzlesJudge[7] = GameObject.Find("Puzzle7(Clone)");
    }
	
	
	void Update () {
		 //如果拼图完成
        if((PuzzlesJudge[0].transform.parent==PuzzleGrids[0].transform&& PuzzlesJudge[1].transform.parent == PuzzleGrids[1].transform&& PuzzlesJudge[2].transform.parent == PuzzleGrids[2].transform
            && PuzzlesJudge[3].transform.parent == PuzzleGrids[3].transform&& PuzzlesJudge[4].transform.parent == PuzzleGrids[4].transform&& PuzzlesJudge[5].transform.parent == PuzzleGrids[5].transform
            && PuzzlesJudge[6].transform.parent == PuzzleGrids[6].transform&& PuzzlesJudge[7].transform.parent == PuzzleGrids[7].transform)||Input.GetKeyDown(KeyCode.P))
        {
            GameManager._Instance.seven = 7;
            GameObject.Find("PuzzleCanvas").SetActive(false);   //拼图完成设置拼图画布不可用
        }           
	}

    private GameObject GetRandomPuzzle(List<GameObject> puzzles)  //得到随机出来的拼图快并且从list中删除得到的
    {
        int index = Random.Range(0, puzzles.Count);
        GameObject RandomPuzzle = puzzles[index];
        puzzles.RemoveAt(index);
        return RandomPuzzle;
    }
}
