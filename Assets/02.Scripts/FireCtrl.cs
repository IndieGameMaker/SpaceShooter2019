using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    public Transform firePos;
    public GameObject bulletPrefab;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Instantiate(생성할 프리팹, 위치, 각도)
            Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        }        
    }
}
