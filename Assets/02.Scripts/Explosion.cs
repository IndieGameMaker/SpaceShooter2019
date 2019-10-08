using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int hitCount = 0;

    void Start()
    {
        
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            if (++hitCount == 3)
            {
                ExplosionBarrel();
                Destroy(this.gameObject);
            }
        }
    }

    void ExplosionBarrel()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position
                                                ,10.0f
                                                ,1<<8);

        for(int i=0; i<colls.Length; i++)
        {
            Rigidbody rb = null;
            if (colls[i].gameObject.GetComponent<Rigidbody>() == null)
            {
               rb = colls[i].gameObject.AddComponent<Rigidbody>();
            }
            rb.AddExplosionForce(1500.0f, transform.position, 10.0f, 1500.0f);
        }
    }
}
