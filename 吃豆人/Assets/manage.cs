using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manage : MonoBehaviour {

    private BlinkyClass _blinky;
    

	void Start () {
        _blinky = new BlinkyClass();
	}
	
	
	void Update () {
        _blinky.update();
	}
}
