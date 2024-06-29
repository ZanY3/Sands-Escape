using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float speed = 10f;
    public float dashStrenght = 40f;
    public float dashTime = 0.2f;
    public float dashCD = 1f;
    public bool isDashing = false;

    public float attackCD;
    public int damage;

    private bool facingRight = true;
    private Transform target;
    private bool canDash = true;
    private float startDashCD;
    private TrailRenderer tRenderer;
    private Rigidbody2D rb;

    private float startAttackCD;
    private Health health;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tRenderer = GetComponent<TrailRenderer>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        startDashCD = dashCD;
        startAttackCD = attackCD;
        health = GetComponent<Health>();
    }
    private void Update()
    {
        dashCD -= Time.deltaTime;
        attackCD -= Time.deltaTime;

        if (gameObject.GetComponent<Health>().health <= 0)
        {
            Destroy(gameObject);
        }
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
        if(dashCD <= 0)
        {
            Dash();
            dashCD = startDashCD;
        }
        if (target != null)
        {
            FlipTowardsPlayer();
        }
    }
    private async void Dash()
    {
        canDash = false;
        isDashing = true;
        Vector2 dashDirection = (target.position - transform.position).normalized;

        rb.velocity = dashDirection * dashStrenght;
        tRenderer.emitting = true;
        await new WaitForSeconds(dashTime);

        tRenderer.emitting = false;
        isDashing = false;
        await new WaitForSeconds(dashCD);
        canDash = true;
    }
    private void FlipTowardsPlayer()
    {
        if (target == null) return;

        Vector3 directionToPlayer = target.position - transform.position;

        if ((directionToPlayer.x > 0 && !facingRight) || (directionToPlayer.x < 0 && facingRight))
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerMovement>();
            if (attackCD <= 0 && !player.isDashing)
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
                attackCD = startAttackCD;
            }
        }
    }
}
