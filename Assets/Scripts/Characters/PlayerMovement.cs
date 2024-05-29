using System.Runtime.CompilerServices;
using System.Threading;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    [Space]
    public float dashStrenght = 40f;
    public float dashTime = 0.2f;
    public float dashCD = 1f;

    public bool isDashing = false;
    public string NoReactLayerInDash;

    private bool canDash = true;
    private float startDashCD;
    private TrailRenderer tRenderer;
    [Space]
    public AudioClip dashSound;

    private AudioSource source;
    private Animator animator;
    private bool isFacingRight = true;
    private bool isRunning = false;
    private Rigidbody2D rb;
    private int defaultLayer = 8;

    private float moveHorizontal, moveVertical;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        startDashCD = dashCD;
        tRenderer = GetComponent<TrailRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        dashCD -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCD <= 0)
        {
            source.PlayOneShot(dashSound);
            Dash();
            dashCD = startDashCD;
        }
        if (isDashing)
            return;

        animator.SetBool("IsRunning", isRunning);
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        movement.Normalize();
        if (moveHorizontal > 0.1f || moveVertical > 0.1f)
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
    private async void Dash()
    {
        canDash = false;
        isDashing = true;
        Vector2 dashDirection = new Vector2(moveHorizontal, moveVertical).normalized;

        int newLayer = LayerMask.NameToLayer(NoReactLayerInDash);
        if (newLayer == -1)
        {
            Debug.LogError("Layer not found: " + NoReactLayerInDash);
            return;
        }
        gameObject.layer = newLayer;

        SetLayerRecursively(gameObject, newLayer);

        rb.velocity = dashDirection * dashStrenght;
        tRenderer.emitting = true;
        await new WaitForSeconds(dashTime);

        RemoveLayer();

        tRenderer.emitting = false;
        isDashing = false;
        await new WaitForSeconds(dashCD);
        canDash = true;
    }
    private void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;
    }
    public void RemoveLayer()
    {
        SetLayerRecursively(gameObject, defaultLayer);
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
