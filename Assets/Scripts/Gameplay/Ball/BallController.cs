using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallController : MonoBehaviour
{
    [SerializeField] private float speed = 7f;

    private Rigidbody2D rb;
    private bool attached = true;
    private Transform paddle;
    private Vector2 attachOffset = Vector2.up * 0.4f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    private void Update()
    {
        if (attached && paddle != null)
            transform.position = (Vector2)paddle.position + attachOffset;
    }

    public void AttachTo(Transform paddleTransform)
    {
        paddle = paddleTransform;
        attached = true;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
    }

    public void Launch()
    {
        if (!attached) return;

        attached = false;
        rb.isKinematic = false;

        Vector2 dir = new Vector2(Random.Range(-0.5f, 0.5f), 1f).normalized;
        rb.velocity = dir * speed;
    }

    private void FixedUpdate()
    {
        if (!attached)
            rb.velocity = rb.velocity.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (attached) return;

        Vector2 v = rb.velocity;
        if (Mathf.Abs(v.y) < 0.25f)
            v.y = 0.25f * Mathf.Sign(v.y == 0 ? 1 : v.y);

        rb.velocity = v.normalized * speed;

        // Звук
        if (collision.collider.CompareTag("Paddle") ||
            collision.collider.CompareTag("Wall") ||
            collision.collider.CompareTag("Brick"))
            AudioManager.Instance.PlayBallHit();

        // Кирпич
        BrickController brick = collision.collider.GetComponent<BrickController>();
        if (brick != null)
            brick.ProcessHitFromBall();
    }
}


















