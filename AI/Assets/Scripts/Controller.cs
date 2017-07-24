using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public GameObject player;
    public GameObject AI;
    public Transform[] path;
    


    private bool gaming=true;  //判断玩家是否死亡
    private zhuangtaiji.FSMSystem fsm;

    //通过过渡状态改变当前状态
    public void setTransition(zhuangtaiji.Transition t)
    {
        fsm.PerformTransition(t);
    }
	
	void Start () {
        MakeFSM();        
	}
	
	
	void FixedUpdate () {
        if (gaming)
        {
            fsm.CurrentState.Reason(player, gameObject);
            fsm.CurrentState.Act(player, gameObject);
        }        
	}

    private void MakeFSM()
    {
        //巡逻状态
        FollowPathState follow = new FollowPathState(path);
        follow.AddTransition(zhuangtaiji.Transition.sawPlayer, zhuangtaiji.StateID.ChasingPlayer);
        //追逐状态
        ChasePlayerState chase = new ChasePlayerState();
        chase.AddTransition(zhuangtaiji.Transition.lostPlayer, zhuangtaiji.StateID.FollowingPath);
        //初始化状态机
        fsm = new zhuangtaiji.FSMSystem(gameObject.transform,follow.wayPoints[follow.currentWayPoint]);
        fsm.AddState(follow);
        fsm.AddState(chase);
    }

    public class FollowPathState : zhuangtaiji.FSMstate
    {
        public int currentWayPoint;
        public Transform[] wayPoints;
        //构造函数初始化
        public FollowPathState(Transform[] way)
        {
            wayPoints = way;
            currentWayPoint = 0;
            //设置自己的stateID
            stateID = zhuangtaiji.StateID.FollowingPath;
        }
        
        //巡逻方法
        public override void Act(GameObject player, GameObject AI)
        {
            Vector3 AIpos = AI.GetComponent<Transform>().position;
            if (AIpos != wayPoints[currentWayPoint].position)
            {
                
                Vector2 p = Vector2.MoveTowards(AIpos, wayPoints[currentWayPoint].position, 0.1f);

                AI.GetComponent<Rigidbody2D>().MovePosition(p);

            }
            else
                currentWayPoint = (currentWayPoint + 1) % wayPoints.Length;

            //动画判断
            Vector3 dir = wayPoints[currentWayPoint].position - AIpos;

            //AI.GetComponent<Animator>().SetFloat("DirX", dir.x);//不存在？？

            AI.GetComponent<Animator>().SetFloat("DirY", dir.y);
        }
        //离开巡逻状态的触发条件
        public override void Reason(GameObject player, GameObject AI)
        {
            bool ispackman = false;
            Vector2[] direction = new Vector2[4];
            direction[0] = new Vector2(1, 0);
            direction[1] = new Vector2(-1, 0);
            direction[2] = new Vector2(0, 1);
            direction[3] = new Vector2(0, -1);
            AI.GetComponent<CircleCollider2D>().enabled = false;
            Vector3 AIpos = AI.GetComponent<Transform>().position;
            for (int i = 0; i < direction.Length; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(AIpos, direction[i], 20);
                if (hit.transform == null)
                    return;
                if (hit.transform.gameObject.name=="pacman")
                    ispackman = true;
            }
            AI.GetComponent<CircleCollider2D>().enabled = true;
            if (ispackman)    //发现pacman时状态改变
            {
                AI.GetComponent<Controller>().setTransition(zhuangtaiji.Transition.sawPlayer);
            }
        }
    }

    public class ChasePlayerState : zhuangtaiji.FSMstate
    {
        
        public ChasePlayerState()
        {
            stateID = zhuangtaiji.StateID.ChasingPlayer;            
        }

        public override void DoBeforeLeaving(Transform AI,Transform point)
        {
            LoadPath findpath = GameObject.Find("maze").GetComponent<LoadPath>();
            findpath.changepath(AI.position, point.position);
        }

        public override void Act(GameObject player, GameObject AI)
        {
            LoadPath findpath = GameObject.Find("maze").GetComponent<LoadPath>();
            findpath.changepath(AI.transform.position, player.transform.position);           
        }

        public override void Reason(GameObject player, GameObject AI)
        {
            if (Vector3.Distance(AI.transform.position, player.transform.position) >= 15)
            {
                
                AI.GetComponent<Controller>().setTransition(zhuangtaiji.Transition.lostPlayer);
            }                
        }
    }
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            gaming = false;
            Destroy(player.gameObject);
        }            
    }
}
