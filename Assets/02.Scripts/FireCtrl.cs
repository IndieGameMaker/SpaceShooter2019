using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour
{
    public Transform firePos;
    public GameObject bulletPrefab;
    
    private AudioClip fireSfx;
    private AudioClip reloadSfx;

    public int magazineCount = 20;
    private int shootCount = 0;
    private bool isReloading = false;

    public float fireRate = 0.1f;
    private float nextFire = 0.0f;
    private AudioSource _audio;

    public MeshRenderer muzzleFlash;
    public Light fireLight;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
        fireSfx = Resources.Load<AudioClip>("Sfx/fire");
        reloadSfx = Resources.Load<AudioClip>("Sfx/p_reload");

        //컴포넌트의 활성화 여부
        muzzleFlash.enabled = false;
        fireLight.intensity = 0.0f;
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
        StartCoroutine(ShowMuzzleFlash());
    }

    IEnumerator ShowMuzzleFlash()
    {
        //이미지의 Offset 변경
        Vector2 offset = new Vector2(Random.Range(0,2), Random.Range(0,2)) * 0.5f; //0.0, 0.5
        muzzleFlash.material.SetTextureOffset("_MainTex", offset);

        //이미지의 크기 변경
        Vector3 size = Vector3.one * Random.Range(0.8f, 2.0f);
        muzzleFlash.transform.localScale = size;

        //이미지의 회전
        Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 360));
        //Quaternion rot = Quaternion.Euler(Vector3.forward * Random.Range(0, 360));
        muzzleFlash.transform.localRotation = rot;

        //Light
        fireLight.intensity = Random.Range(1.0f, 4.0f);

        muzzleFlash.enabled = true;

        yield return new WaitForSeconds(0.1f);
        
        muzzleFlash.enabled = false;
        fireLight.intensity = 0.0f;
    }

    IEnumerator Reloading()
    {
        _audio.PlayOneShot(reloadSfx);
        yield return new WaitForSeconds(reloadSfx.length + 0.1f);
        isReloading = false;
        shootCount = 0;
    }

}
