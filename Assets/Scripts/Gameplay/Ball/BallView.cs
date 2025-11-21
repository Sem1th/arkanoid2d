using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallView : MonoBehaviour
{
    public Rigidbody2D Rb { get; private set; }

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    public void SetVelocity(Vector2 velocity)
    {
        Rb.velocity = velocity;
    }

    public Vector2 Position => transform.position;

    public void Stop()
    {
        Rb.velocity = Vector2.zero;
    }
}
