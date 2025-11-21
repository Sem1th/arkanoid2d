using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PaddleView : MonoBehaviour
{
    public Rigidbody2D Rb { get; private set; }
    public float Width => GetComponent<Collider2D>().bounds.size.x;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    public void MoveTo(Vector2 position)
    {
        // Для kinematic rigidbody: прямое позиционирование
        Rb.MovePosition(position);
    }

    public Vector2 Position => transform.position;
}

