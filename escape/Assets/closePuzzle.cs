using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closePuzzle : MonoBehaviour {

    public Canvas PuzzleCanves;

    public void close()
    {
        PuzzleCanves.GetComponent<Canvas>().planeDistance = 0;
    }
}
