using System.Collections;
using UnityEngine;

public class PlayerMovementNoAnimation : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool isDashing = false;
    private float dashCooldownTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            rb.velocity = moveDirection * dashSpeed;
        }
        else
        {
            rb.velocity = moveDirection * moveSpeed;
        }
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && !isDashing && Time.time >= dashCooldownTime)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        float endTime = Time.time + dashDuration;

        while (Time.time < endTime)
        {
            yield return null; // Wait for the next frame
        }

        isDashing = false;
        dashCooldownTime = Time.time + dashCooldown;
    }
        void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if colliding with an obstacle
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Stop the player's movement upon collision
            rb.velocity = Vector2.zero;
        }
    }
}
