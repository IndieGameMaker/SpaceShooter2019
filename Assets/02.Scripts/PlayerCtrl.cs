using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField]
    private Transform tr;
    public float moveSpeed = 5.0f;

    void Start()
    {
        tr = GetComponent<Transform>();
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
        float v = Input.GetAxis("Vertical");   // -1.0f ~ 0.0f ~ 1.0f
        float h = Input.GetAxis("Horizontal"); // -1.0f ~ 0.0f ~ 1.0f
        tr.Translate(Vector3.forward * moveSpeed * Time.deltaTime * v);
    }
}
