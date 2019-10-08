using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    public enum State
    {
        IDLE
        ,TRACE
        ,ATTACK
        ,DIE
    }

    public State state = State.IDLE;
    public float attackDist = 2.0f;     //공격 사정거리
    public float traceDist  = 10.0f;    //추적 사정거리
    public bool isDie = false;          //몬스터의 사망여부

    private Transform playerTr;         //주인공의 위치 추출을 위한 Transform
    private Transform monsterTr;        //몬스터의 위치 추출을 위한 Transform
    private NavMeshAgent nv;            //NavMeshAgent 컴포넌트

    private WaitForSeconds ws = new WaitForSeconds(0.3f);

    void Start()
    {
        nv = GetComponent<NavMeshAgent>();
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();

        GameObject monsterObj = GameObject.FindGameObjectWithTag("MONSTER");
        if (monsterObj != null)
        {
            monsterTr = monsterObj.GetComponent<Transform>();
        }
        else
        {
            Debug.LogError("MONSTER Tag not found !!!");
        }

        StartCoroutine(CheckMonsterState());
        StartCoroutine(MonsterAction());
    }

    IEnumerator CheckMonsterState()
    {
        while(!isDie)
        {
           yield return ws;
           
           float distance = Vector3.Distance(playerTr.position, monsterTr.position);
           if (distance <= attackDist)      //공격 사정거리 이내에 있을 경우
           {
               state = State.ATTACK;
           }
           else if (distance <= traceDist)  //추적 사정거리 이내에 있을 경우
           {
               state = State.TRACE;
           }
           else 
           {
               state = State.IDLE;
           }
        }
    }

    IEnumerator MonsterAction()
    {
        while(!isDie)
        {
            switch (state)
            {
                case State.IDLE:
                    nv.isStopped = true;
                    break;
                case State.TRACE:
                    nv.SetDestination(playerTr.position);
                    nv.isStopped = false;
                    break;
                case State.ATTACK:
                    break;
            }

            yield return ws;
        }
    }
}
