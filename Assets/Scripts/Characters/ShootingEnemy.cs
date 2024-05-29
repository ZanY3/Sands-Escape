using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShootingEnemy : MonoBehaviour
{
    public float shootCD;
    public Transform shootPoint;
    public GameObject touret;
    public GameObject bulletPrefab;
    public float touretOffset;
    [Space]
    public AudioClip shootSound;

    private AudioSource source;
    private float startShootCD;
    private Transform shootTarget;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        shootTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        startShootCD = shootCD;
    }
    private void Update()
    {
        if (shootTarget != null)
        {
            Vector3 direction = shootTarget.position - touret.transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            touret.transform.rotation = Quaternion.Euler(0, 0, angle + touretOffset);
        }
        shootCD -= Time.deltaTime;
        if(shootCD <= 0 )
        {
            Shoot();
            shootCD = startShootCD;
        }
    }
    private void Shoot()
    {
        source.PlayOneShot(shootSound);
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Vector3 direction = (shootTarget.position - shootPoint.position).normalized;
    }
}
