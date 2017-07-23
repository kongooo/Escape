using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyClass {

    blinkyBaseState _state;

    public BlinkyClass()
    {
        _state = new Xunluo(this);
    }
    
    public void SetBlinkyState(blinkyBaseState newstate)
    {
        _state = newstate;
    }
	
	public void update () {
        _state.myupdate();
        _state.ChangeState();
	}

    
   
}
