using UnityEngine;
using System.Collections;

public class PacmanMove : MonoBehaviour {

	public float speed = 0.15f;
    float h, v;
	


	void Start () {
	

	}

	void FixedUpdate () {
        h = Input.GetAxis("Horizontal")*speed;
        v = Input.GetAxis("Vertical")*speed;

        Vector3 pos = new Vector3(h, v,transform.position.z);

        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        RaycastHit2D hit = Physics2D.Linecast(transform.position, transform.position + pos);
        gameObject.GetComponent<CircleCollider2D>().enabled = true;

        if (hit.transform == null)
        {
            transform.position += pos;
        }

		//判断执行动画
		

		GetComponent<Animator> ().SetFloat ("DirX",pos.x);

		GetComponent<Animator> ().SetFloat ("DirY",pos.y);
			
	}
		

	
		
}
