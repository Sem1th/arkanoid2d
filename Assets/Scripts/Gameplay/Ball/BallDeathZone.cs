using UnityEngine;

public class BallDeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ball"))
            return;

        // Если игра остановлена — игнорируем
        if (GameBootstrap.GameStateSvc.State != GameState.Playing)
            return;

        Destroy(other.gameObject);

        // Публикуем событие "мяч потерян"
        GameBootstrap.EventBusSvc.Publish(new BallLostEvent());
    }
}


