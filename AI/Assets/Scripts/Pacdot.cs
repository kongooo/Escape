using UnityEngine;
using System.Collections;

public class Pacdot : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//触发器检测,销毁豆子
	void OnTriggerEnter2D(Collider2D co)
	{
		if (co.name == "pacman")
			Destroy (gameObject);
	}
}
