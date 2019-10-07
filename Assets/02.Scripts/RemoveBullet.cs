using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    public GameObject sparkEffect;

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            //충돌한 지점의 배열
            ContactPoint[] contacts = coll.contacts;
            //첫번째 충돌지점의 법선 벡터 산출
            Vector3 _normal = -contacts[0].normal;

            GameObject spark = Instantiate(sparkEffect
                                        , coll.transform.position
                                        , Quaternion.LookRotation(_normal));
            Destroy(spark, 0.3f);
            Destroy(coll.gameObject);
        }
    }

}
