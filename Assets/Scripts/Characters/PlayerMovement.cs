using System.Runtime.CompilerServices;
using System.Threading;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Animator animator;
    private bool isFacingRight = true;
    private bool isRunning = false;
    private Rigidbody2D rb;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        animator.SetBool("IsRunning", isRunning);
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        movement.Normalize();
        if(moveHorizontal > 0.1f || moveVertical > 0.1f)
        {
            isRunning = true;
        }
        else
            isRunning = false;
        if (movement.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (movement.x < 0 && isFacingRight)
        {
            Flip();
        }
        rb.velocity = movement * speed;
        isRunning = movement != Vector2.zero;
        //animator.SetBool("isRunning", isRunning);

    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
