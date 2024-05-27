using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;
    public string wearponTag;
    public AudioClip enemyHitSound;
    public AudioClip hitSound;

    private AudioSource sourceBullet;
    private Animator animator;
    private Transform wearponPos;
    private Rigidbody2D rb;

    private void Start()
    {
        sourceBullet = GameObject.FindGameObjectWithTag("BulletSource").GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();

        wearponPos = GameObject.FindGameObjectWithTag(wearponTag).GetComponent<Transform>();
        if(wearponPos != null )
        {
            rb.AddForce(wearponPos.up * speed, ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            float randomPitch = Random.Range(0.6f, 1f);
            sourceBullet.pitch = randomPitch;
            sourceBullet.PlayOneShot(enemyHitSound);
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);

        }
        else
        {
            float randomPitch = Random.Range(0.6f, 1f);
            sourceBullet.pitch = randomPitch;
            sourceBullet.PlayOneShot(hitSound);
        }
        Destroy(gameObject);
    }
}
