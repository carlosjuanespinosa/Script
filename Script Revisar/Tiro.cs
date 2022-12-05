using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float shootPower = 10f;
    [SerializeField] float shootRate = 10f;
    [SerializeField] float range = 100f;

    [SerializeField] Camera fpsCamera;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject impactEffect;

    private float nextShootTime = 0f;
    private bool holdShoot = false;



    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(fpsCamera.transform.position, fpsCamera.transform.forward * range, Color.red);
        if (holdShoot && Time.time >= nextShootTime)
        {
            nextShootTime = Time.time + 1 / shootRate;
            PerformShoot();
        }

    }

    private void PerformShoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, Mathf.Infinity))
        {
            muzzleFlash.Play();
            float distance = Vector3.Distance(fpsCamera.transform.position, hit.point);
            if (distance <= range)
            {
                GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 2f);
            }
        }
    }
}