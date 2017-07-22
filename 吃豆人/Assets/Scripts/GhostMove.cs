using UnityEngine;
using System.Collections;

public class GhostMove : MonoBehaviour {

	public Transform[] waypoints;

	int cur = 0;

	public float speed = 0.3f;
	
	// Update is called once per frame
	void FixedUpdate () {

		//数组设置关键点，实现敌人的移动
		if (transform.position != waypoints[cur].position)
		{
			Vector2 p = Vector2.MoveTowards(transform.position,waypoints[cur].position,speed);

			GetComponent<Rigidbody2D>().MovePosition(p);
		}

		else cur = (cur + 1) % waypoints.Length;


		//动画判断
		Vector2 dir = waypoints[cur].position - transform.position;

		GetComponent<Animator>().SetFloat("DirX", dir.x);

		GetComponent<Animator>().SetFloat("DirY", dir.y);
	}


	//触发器检测，销毁Pacman
	void OnTriggerEnter2D(Collider2D co) {

		if (co.name == "pacman")
		Destroy(co.gameObject);
	}
}
