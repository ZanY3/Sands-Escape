using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float maxRangeToFollow;
    [Space]
    public float attackCD;
    public int damage;
    [Space]
    public float minXRandomPoint;
    public float minYRandomPoint;
    public float maxXRandomPoint;
    public float maxYRandomPoint;
    [Space]
    public WallController room;

    private Transform target;
    private Vector2 randomTargetPoint;
    private float startAttackCD;
    private Health health;

    private void Start()
    {
        health = GetComponent<Health>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        startAttackCD = attackCD;
        SetNewRandomTargetPoint();
    }

    private void Update()
    {
        if(gameObject.GetComponent<Health>().health <= 0)
        {
            room.enemiesLeft--;
            Destroy(gameObject);
        }
        attackCD -= Time.deltaTime;
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);

        if (target != null && distanceToPlayer < maxRangeToFollow)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            MoveToRandomPoint();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerMovement>();
            if(attackCD <= 0 && !player.isDashing)
            {
                
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
                attackCD = startAttackCD;
            }
        }

        if (collision.gameObject.CompareTag("Block") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            SetNewRandomTargetPoint();
        }
    }

    private void SetNewRandomTargetPoint()
    {
        randomTargetPoint = GetRandomPoint();
    }

    private void MoveToRandomPoint()
    {
        if (Vector2.Distance(transform.position, randomTargetPoint) < 0.2f)
        {
            SetNewRandomTargetPoint();
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, randomTargetPoint, speed * Time.deltaTime);
        }
    }

    private Vector2 GetRandomPoint()
    {
        if (room != null)
        {
            var roomTransform = room.GetComponent<Transform>();
            float randX = Random.Range(-roomTransform.position.x - 10, roomTransform.position.x + 10);
            float randY = Random.Range(-roomTransform.position.y - 5, roomTransform.position.y + 5);
            return new Vector2(randX, randY);
        }
        else
        {
            Debug.LogError("Current room not found!");
            return transform.position;
        }
    }
}
