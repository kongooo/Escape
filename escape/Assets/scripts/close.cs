using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class close : MonoBehaviour {

	public void packageClose()
    {
        GameObject.Find("Canvas").GetComponent<Canvas>().planeDistance=0;
    }
}
