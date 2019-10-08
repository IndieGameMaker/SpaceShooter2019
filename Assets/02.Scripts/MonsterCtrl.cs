using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
