using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollisionResolver
{
    /// <summary>
    /// Возвращает новое направление в зависимости от точки попадания мяча в платформу.
    /// </summary>
    public static Vector2 ResolveBounce(Vector2 ballPosition, Vector2 paddlePosition, float paddleWidth)
    {
        float relative = (ballPosition.x - paddlePosition.x) / (paddleWidth / 2f);
        float angle = relative * 60f; // 60 градусов максимальный наклон

        float rad = angle * Mathf.Deg2Rad;

        Vector2 direction = new Vector2(Mathf.Sin(rad), Mathf.Cos(rad));
        return direction.normalized;
    }
}
