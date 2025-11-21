using UnityEngine;

[DefaultExecutionOrder(100)]
public class InputService : UnityEngine.MonoBehaviour, IInputService
{
    // Используем стандартные оси ("Horizontal") для клавиатуры/контроллера.
    // При желании можно расширить: мышь, тач, виртуальный джойстик.

    public float GetHorizontal()
    {
        return Input.GetAxis("Horizontal");
    }

    public bool IsPausePressed()
    {
        return Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P);
    }
}

