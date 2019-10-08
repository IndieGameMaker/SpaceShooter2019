using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    public Transform firePos;
    public GameObject bulletPrefab;
    public AudioClip fireSfx;
    public float fireRate = 0.1f;
    private float nextFire = 0.0f;
    private AudioSource _audio;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
        fireSfx = Resources.Load<AudioClip>("Sfx/fire");
    }
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (nextFire <= Time.time)
            {
                Fire();
                nextFire = fireRate + Time.time;
            }
        }        
    }

    void Fire()
    {
        //Instantiate(생성할 프리팹, 위치, 각도)
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        _audio.PlayOneShot(fireSfx, 0.8f);
    }
}
