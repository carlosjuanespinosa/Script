using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private string targetTag = "Player";
    [SerializeField] private float range = 10f;

    [Header("Shooting Settings")]
    [SerializeField] private float shootPower = 10f;
    [SerializeField] private float shootCadency = 0.25f;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private ParticleSystem shotEffect;

    private Transform target;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetTarget());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetTarget()
    {
        while (gameObject.activeSelf)
        {
            float shortestDistance = Mathf.Infinity;
            GameObject nearestTarget = null;
            GameObject[] potentialTargets = GameObject.FindGameObjectsWithTag(targetTag);
            foreach (GameObject pTarget in potentialTargets)
            {
                float pTargetDistance = Vector3.Distance(transform.position, pTarget.transform.position);
                if (pTargetDistance < shortestDistance)
                {
                    shortestDistance = pTargetDistance;
                    nearestTarget = pTarget;
                }
            }
            if (nearestTarget != null && shortestDistance <= range)
            {
                target = nearestTarget.transform;
            }
            else
            {
                target = null;
            }

            yield return new WaitForSeconds(0.25f);
        }
    }
    IEnumerator CoShoot()
    {
        while (gameObject.activeSelf)
        {
            if (target != null)
            {
                Shoot();
            }

            yield return new WaitForSeconds(shootCadency);
        }
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        if (bulletPrefab.name.Equals("ControladorBalas"))
        {
            bullet.GetComponent<ControladorBalas>().SetTarget(target);
        }
        else
        {
            bullet.GetComponent<Rigidbody>().AddForce(shootPoint.forward * shootPower, ForceMode.VelocityChange);
        }

        if (shotEffect != null) shotEffect.Play();

        Destroy(bullet, 4);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
