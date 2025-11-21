using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float limitX = 7f;

    private void Update()
    {
        if (GameBootstrap.GameStateSvc.State != GameState.Playing)
            return;

        // Всегда читаем MobileInputState
        float input = MobileInputState.Horizontal;

        Vector3 pos = transform.position;
        pos.x += input * speed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -limitX, limitX);
        transform.position = pos;
    }

    public void Move(float direction)
    {
        Vector3 pos = transform.position;
        pos.x += direction * speed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -limitX, limitX);
        transform.position = pos;
    }
}






