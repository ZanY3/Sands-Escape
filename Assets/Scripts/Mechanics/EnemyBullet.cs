using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public int damage;
    public AudioClip playerHitSound;
    public AudioClip hitSound;

    private Transform touretPos;
    private AudioSource sourceBullet;
    private Rigidbody2D rb;

    private void Start()
    {
        touretPos = GameObject.FindGameObjectWithTag("Touret").GetComponent<Transform>();
        sourceBullet = GameObject.FindGameObjectWithTag("BulletSource").GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(touretPos.up * speed, ForceMode2D.Impulse);
        Destroy(gameObject, 2);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float randomPitch = Random.Range(0.6f, 1f);
            sourceBullet.pitch = randomPitch;
            sourceBullet.PlayOneShot(playerHitSound);
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
