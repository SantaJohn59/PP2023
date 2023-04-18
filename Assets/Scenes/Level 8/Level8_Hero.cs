using System;
using UnityEngine;

public class Level8_Hero : MonoBehaviour
{
    private float speed = 3f;
    private float jumpForce = 4f;
    private bool isGrounded;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Run()
    {
        var dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
        sprite.flipX = dirX < 0;
    }

    void Update()
    {
        if (Input.GetButton("Horizontal"))
            Run();

        IsGroundedUpate();
        if (!areJumpsOver && Input.GetButtonDown("Jump") && (isGrounded || ++jumpCount < maxJumpValue))
            Jump();
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        OnJump?.Invoke();
    }

    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask Ground;
    private float checkRadius = 0.1f;
    private int jumpCount;
    private int maxJumpValue = 2;

    private void IsGroundedUpate()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, Ground);
        if (isGrounded) jumpCount = 0;
    }

    private static bool areJumpsOver = false;
    public static void JumpsAreOver()
    {
        areJumpsOver = true;
    }

    public static Action OnJump;
}
