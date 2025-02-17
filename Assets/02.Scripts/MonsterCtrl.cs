﻿using System.Collections;
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
    private Animator monsterAnim;       //Animator 컴포넌트

    private WaitForSeconds ws = new WaitForSeconds(0.3f);
    //Animator에 정의된 파라메터의 해쉬값을 미리 추출
    private int hashIsTrace  = Animator.StringToHash("IsTrace");
    private int hashIsAttack = Animator.StringToHash("IsAttack");
    private int hashHit      = Animator.StringToHash("Hit");
    private int hashDie      = Animator.StringToHash("Die");

    //Monster 생명 게이지
    private int hp = 100;

    void Awake()
    {
        nv = GetComponent<NavMeshAgent>();
        monsterAnim = GetComponent<Animator>();

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
    }

    void OnEnable()
    {
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
        float speed = Random.Range(0.6f,1.5f);

        while(!isDie)
        {
            switch (state)
            {
                case State.IDLE:
                    monsterAnim.SetFloat("AnimSpeed", speed);
                    monsterAnim.SetBool(hashIsTrace, false);
                    nv.isStopped = true;
                    break;
                case State.TRACE:
                    monsterAnim.SetFloat("AnimSpeed", speed);
                    monsterAnim.SetBool(hashIsAttack, false);
                    monsterAnim.SetBool(hashIsTrace, true);
                    nv.SetDestination(playerTr.position);
                    nv.isStopped = false;
                    break;
                case State.ATTACK:
                    nv.isStopped = true;
                    monsterAnim.SetFloat("AnimSpeed", speed);
                    monsterAnim.SetBool(hashIsAttack, true);
                    break;
            }

            yield return ws;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            Destroy(coll.gameObject);
            monsterAnim.SetTrigger(hashHit);
            hp -= 10;
            if (hp <= 0)
            {
                MonsterDie();
            }
        }
    }

    void MonsterDie()
    {
        nv.isStopped = true;
        GetComponent<CapsuleCollider>().enabled = false;
        
        isDie = true;
        monsterAnim.SetTrigger(hashDie);
        StopAllCoroutines();
        Invoke("ReturnPool", 3.0f);
    }

    void ReturnPool()
    {
        hp = 100;
        isDie = false;
        GetComponent<CapsuleCollider>().enabled = true;

        this.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider coll)
    {
        Debug.Log(coll.gameObject.name);
    }

}
