using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;
    public string wearponTag;

    private Animator animator;
    private Transform wearponPos;
    private Rigidbody2D rb;

    private void Start()
    {
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
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);

        }
        Destroy(gameObject);
    }
}
