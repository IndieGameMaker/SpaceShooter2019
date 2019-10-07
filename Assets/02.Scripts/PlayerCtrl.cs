using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnim
{
    public AnimationClip idle;
    public AnimationClip runForward;
    public AnimationClip runBackward;
    public AnimationClip runLeft;
    public AnimationClip runRight;
}


public class PlayerCtrl : MonoBehaviour
{
    public PlayerAnim  _playerAnim;
    [SerializeField]
    private Transform tr;
    private Animation anim;
    public float moveSpeed = 7.0f;


    void Start()
    {
        tr = GetComponent<Transform>();
        anim = GetComponent<Animation>();
        anim.clip = _playerAnim.idle;
        anim.Play();
    }

    /*
        정규화 벡터 (Normalized Vector, Unit Vector)
        Vector3.forward = new Vector3(0, 0, 1)
        Vector3.up      = new Vector3(0, 1, 0)
        Vector3.right   = new Vector3(1, 0, 0)
        
        Vector3.zero    = new Vector3(0, 0, 0)
        Vector3.one     = new Vector3(1, 1, 1)        
    */

    void Update()
    {
        //tr.position += Vector3.forward * 0.1f;
        // GetAxis    : -1.0f ~ 0.0f ~ 1.0f
        // GetAxisRaw : -1, 0, +1
        float v = Input.GetAxisRaw("Vertical");   // 
        float h = Input.GetAxisRaw("Horizontal"); // -1.0f ~ 0.0f ~ 1.0f
        
        //벡터의 덧셈 연산
        Vector3 dir = (Vector3.forward * v) + (Vector3.right * h);
        
        tr.Translate(dir.normalized * Time.deltaTime * moveSpeed); 

        float r = Input.GetAxis("Mouse X");
        tr.Rotate(Vector3.up * Time.deltaTime * 80.0f * r);

        ChangeAnimation(h, v);    
    }

    void ChangeAnimation(float h, float v)
    {
        if (v >= 0.1f)
        {
            anim.CrossFade(_playerAnim.runForward.name, 0.3f);
        }
        else if (v <= -0.1f)
        {
            anim.CrossFade(_playerAnim.runBackward.name, 0.3f);
        }
        else if (h >= 0.1f)
        {
            anim.CrossFade(_playerAnim.runRight.name, 0.3f);
        }
        else if (h <= -0.1f)
        {
            anim.CrossFade(_playerAnim.runLeft.name, 0.3f);
        }
        else
        {
            anim.CrossFade(_playerAnim.idle.name, 0.3f);
        }
    }
}
