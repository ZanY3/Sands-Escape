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

    private Transform target;
    private Vector2 randomTargetPoint;
    private float startAttackCD;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        startAttackCD = attackCD;
        SetNewRandomTargetPoint();
    }

    private void Update()
    {
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
            if(attackCD <= 0)
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
        Debug.Log("New Random Target Point: " + randomTargetPoint);
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
        float randX = Random.Range(minXRandomPoint, maxXRandomPoint);
        float randY = Random.Range(minYRandomPoint, maxYRandomPoint);
        return new Vector2(randX, randY);
    }
}
