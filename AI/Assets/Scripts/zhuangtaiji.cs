using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zhuangtaiji {

    
    //枚举过渡状态
	public enum Transition
    {
        NullTransition=0,  //该过度代表系统中不存在的状态
        sawPlayer,          //AI的两个过渡状态
        lostPlayer
    }
    //状态枚举
    public enum StateID
    {
        NullStateID=0,
        ChasingPlayer,
        FollowingPath
    }

    public abstract class FSMstate
    {
        //由过度状态和当前状态组成的字典
        protected Dictionary<Transition,StateID> map=new Dictionary<Transition,StateID>();
        protected StateID stateID;
        public StateID ID { get { return stateID; } }  //只读属性

        //向状态地图中添加状态
        public void AddTransition(Transition trans,StateID id)
        {
            //检查过渡状态是否为空
            if (trans == Transition.NullTransition)
            {
                Debug.LogError("FSMState ERROR: NullTransition is not allowed for a real transition");
                return;
            }
            //检查想要改变的状态是否为空
            if(id==StateID.NullStateID)
            {
                Debug.LogError("FSMState ERROR: NullStateID is not allowed for a real ID");
                return;
            }
            //检查过渡状态是否在字典中
            if (map.ContainsKey(trans))
            {
                Debug.LogError("FSMState ERROR: State " + stateID.ToString() + " already has transition "
                   + trans.ToString() + "Impossible to assign to another state");
                return;
            }
            map.Add(trans, id);
        }

        //从状态地图中删除状态
        public void DeleteTransition(Transition trans)
        {
            //若为空的过渡状态
            if (trans == Transition.NullTransition)
            {
                Debug.LogError("FSMState ERROR: NullTransition is not allowed");
                return;
            }
            //若地图中包括该状态则删除
            if (map.ContainsKey(trans))
            {
                map.Remove(trans);
                return;
            }
            //否则报错
            Debug.LogError("FSMState ERROR: Transition " + trans.ToString() + " passed to "
                + stateID.ToString() +" was not on the state's transition list");
        }

        //在当前状态接收到一个过渡状态时返回状态机需要成为的状态
        public StateID GetState(Transition trans)
        {
            //状态地图中包括该过度状态则返回对应的状态，不包括则返回空
            if (map.ContainsKey(trans))
            {
                return map[trans];
            }
            return StateID.NullStateID;
        }

        //设立进入一个特定状态的条件，在状态机分配它到当前状态之前被自动调用
        public virtual void DoBeforeEntering() { }

        //在有限状态机切换到新的状态之前被自动调用（重置变量）
        public virtual void DoBeforeLeaving(Transform Ai,Transform Po) { }

        //决定当前状态是否需要过渡到其他状态
        public abstract void Reason(GameObject player, GameObject AI);

        //控制AI的行为
        public abstract void Act(GameObject player, GameObject AI);
    }

    //AI的有限状态机类
    public class FSMSystem
    {
        Transform ai;
        Transform po;

        //状态集合
        private List<FSMstate> states;

        //通过预装一个过渡的唯一方式来改变当前的状态，而不是直接改变当前的状态
        //单例
        private StateID currentStateID;
        public StateID CurrentStateID { get { return currentStateID; } }
        private FSMstate currentState;
        public FSMstate CurrentState { get { return currentState; } }

        //构造函数初始化states
        public FSMSystem(Transform AI,Transform Po)
        {
            states = new List<FSMstate>();
            ai = AI;
            po = Po;
        }

        //为有限状态机置入新的状态
        public void AddState(FSMstate s)
        {
            //在添加前检测是否为空
            if (s == null)
            {
                Debug.LogError("FSM ERROR: Null reference is not allowed");
            }
            //添加第一个状态
            if (states.Count == 0)
            {
                states.Add(s);
                currentState = s;
                currentStateID = s.ID;
                return;
            }

            //若状态未被添加过则添加进集合
            foreach(FSMstate state in states)
            {
                if (state.ID == s.ID)
                {
                    Debug.LogError("FSM ERROR: Impossible to add state " + 
                        s.ID.ToString() +" because state has already been added");
                    return;
                }
                states.Add(s);
            }
        }

        //删除状态
        public void DeleteState(StateID id)
        {
            if (id == StateID.NullStateID)
            {
                Debug.LogError("FSM ERROR: NullStateID is not allowed for a real state");
                return;
            }
            //遍历列表，若存在该状态则删除
            foreach(FSMstate state in states)
            {
                if (state.ID == id)
                {
                    states.Remove(state);
                    return;
                }                    
            }
            //否则打印错误信息
            Debug.LogError("FSM ERROR: Impossible to delete state " + id.ToString() +
                        ". It was not on the list of states");
        }

        //通过接受过渡状态来改变当前状态
        public void PerformTransition(Transition Trans)
        {
            //该状态未空则打印错误信息并且返回
            if (Trans == Transition.NullTransition)
            {
                Debug.LogError("FSM ERROR: NullTransition is not allowed for a real transition");
                return;
            }
            //得到当前状态通过过渡状态改变后的状态
            StateID id = currentState.GetState(Trans);
            //若该状态未空则返回
            if (id == StateID.NullStateID)
            {
                Debug.LogError("FSM ERROR: State " + currentStateID.ToString() + " does not have a target state " +
                           " for transition " + Trans.ToString());
                            return;
            }
            //更新当前状态
            currentStateID = id;
            foreach(FSMstate state in states)
            {
                if (state.ID == currentStateID)
                {
                    currentState.DoBeforeLeaving(ai,po);
                    currentState = state;
                    currentState.DoBeforeEntering();
                    break;
                }
            }
                
        }
    }

}
