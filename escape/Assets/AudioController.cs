using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public AudioSource water;

	void Start () {
        StartCoroutine(RandomWater());
    }
	
	void Update () {
       
    }
    private IEnumerator RandomWater()
    {
        
        int time=Random.Range(10, 60);
        yield return new  WaitForSeconds(time);   //隔10s到60s播放一次
        water.Play();
        yield return RandomWater();               //重复执行
    }
}
