using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform paddle;

    private BallController _currentBall;

    public BallController SpawnBall()
    {
        if (_currentBall != null)
            Destroy(_currentBall.gameObject);

        Vector3 spawnPos = paddle.position + Vector3.up * 0.4f;
        GameObject ballObj = Instantiate(ballPrefab, spawnPos, Quaternion.identity);
        _currentBall = ballObj.GetComponent<BallController>();
        _currentBall.AttachTo(paddle);

        return _currentBall;
    }

    public void LaunchBall()
    {
        if (_currentBall == null) return;

        _currentBall?.Launch();
        AudioManager.Instance.PlayBallLaunch();
    }
}









