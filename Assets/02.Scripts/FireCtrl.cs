using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    public Transform firePos;
    public GameObject bulletPrefab;
    
    public AudioClip fireSfx;
    public AudioClip reloadSfx;

    public int magazineCount = 20;
    private int shootCount = 0;
    private bool isReloading = false;

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
            if (!isReloading && nextFire <= Time.time)
            {
                Fire();
                nextFire = fireRate + Time.time;
            }
        }        
    }

    void Fire()
    {
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        _audio.PlayOneShot(fireSfx, 0.8f);
        isReloading = (++shootCount == magazineCount);
        if (isReloading == true)
        {
            StartCoroutine(Reloading());
        }
    }

    IEnumerator Reloading()
    {
        _audio.PlayOneShot(reloadSfx);
        yield return new WaitForSeconds(reloadSfx.length + 0.1f);
        isReloading = false;
        shootCount = 0;
    }

}
