using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BrickController : MonoBehaviour
{
    [SerializeField] private int hitPoints = 1;
    [SerializeField] private int points = 10;

    private bool _pendingDestroy = false;

    public void ProcessHitFromBall()
    {
        if (_pendingDestroy) return;

        hitPoints--;
        if (hitPoints <= 0)
        {
            _pendingDestroy = true;
            StartCoroutine(DestroyAtEndOfFixedUpdate());
        }
    }

    private System.Collections.IEnumerator DestroyAtEndOfFixedUpdate()
    {
        yield return new WaitForFixedUpdate();

        AudioManager.Instance.PlayBrickDestroy();

        FindObjectOfType<LevelFactory>().RemoveBrick(gameObject);
        
        GameBootstrap.EventBusSvc?.Publish(new BrickDestroyedEvent(gameObject, points));

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            ProcessHitFromBall();
        }
    }
}







