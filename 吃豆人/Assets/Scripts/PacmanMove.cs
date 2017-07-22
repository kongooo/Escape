using UnityEngine;
using System.Collections;

public class PacmanMove : MonoBehaviour {

	public float speed = 0.4f;

	Vector2 dest = Vector2.zero;


	void Start () {
	
		dest = transform.position;

	}

	void FixedUpdate () {


		//通过Rigidbody2D，实现Pacman的移动
		Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);

		GetComponent<Rigidbody2D>().MovePosition(p);

		//收集输入信息，通过Valid方法，计算移动距离
		if (Input.GetKey (KeyCode.UpArrow)) 
		{

			dest=Valid (Vector2.up,dest);
		}

		if (Input.GetKey (KeyCode.RightArrow)) 
		{

			dest=Valid (Vector2.right,dest);
		}
			

		if (Input.GetKey (KeyCode.DownArrow)) 
		{

			dest=Valid (-Vector2.up,dest);
		}

		if (Input.GetKey (KeyCode.LeftArrow)) 
		{

			dest=Valid (-Vector2.right,dest);
		}


		//判断执行动画
		Vector2 dir = dest - (Vector2)transform.position;

		GetComponent<Animator> ().SetFloat ("DirX",dir.x);

		GetComponent<Animator> ().SetFloat ("DirY",dir.y);
			
	}
		

	//该方法用来判断角色是否可以在该方向上移动，同时计算移动距离
	Vector2 Valid(Vector2 dir,Vector2 dest)
	{

		//通过射线检测判断是否可以通过，移动时可能会有一定误差，所以我用Vector2.Scale(dir,new Vector2(0.1f,0.1f))进行了一定误差处理）

		Vector2 pos =transform.position;

		RaycastHit2D hit = Physics2D.Linecast (pos+dir+Vector2.Scale(dir,new Vector2(0.1f,0.1f)),pos);


		//通过相应判断进行移动计算
		if(hit.collider==GetComponent<Collider2D>())
		{

			if((Vector2)transform.position == dest)
			{
				
				dest = (Vector2)transform.position + dir;

			}

		}

		return dest;
	}
		
}
