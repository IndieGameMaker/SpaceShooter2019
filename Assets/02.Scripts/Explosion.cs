using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Explosion : MonoBehaviour
{
    public int hitCount = 0;
    public GameObject explosionEffect;
    public AudioClip expSFX;

    private AudioSource _audio;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            if (++hitCount == 3)
            {
                ExplosionBarrel();
                Destroy(this.gameObject, 1.8f);
            }
        }
    }

    public void ExplosionBarrel()
    {
        ExpSound(expSFX);

        GameObject effect = Instantiate(explosionEffect
                                        , transform.position
                                        , Quaternion.identity);
        Destroy(effect, 1.5f);

        Collider[] colls = Physics.OverlapSphere(transform.position
                                                ,10.0f
                                                ,1<<8);

        for(int i=0; i<colls.Length; i++)
        {
            Rigidbody rb = colls[i].gameObject.GetComponent<Rigidbody>();
            if (rb == null)
            {
               rb = colls[i].gameObject.AddComponent<Rigidbody>();
            }
            rb.AddExplosionForce(1500.0f, transform.position, 10.0f, 1500.0f);
            colls[i].GetComponent<Explosion>().ExplosionBarrelSelf();
        }
    }

    public void ExplosionBarrelSelf()
    {
        ExpSound(expSFX);
        
        GameObject effect = Instantiate(explosionEffect
                                        , transform.position
                                        , Quaternion.identity);
        Destroy(effect, 1.5f);
        Destroy(this.gameObject, 1.8f);
    }

    void ExpSound(AudioClip sfx)
    {
        GameObject sfxObj = new GameObject("SFX");
        AudioSource __audio = sfxObj.AddComponent<AudioSource>();
        __audio.PlayOneShot(sfx);

        Destroy(sfxObj, sfx.length);
    }
}
