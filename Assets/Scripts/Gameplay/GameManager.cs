using UnityEngine;


public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BallSpawner _ballSpawner;
    [SerializeField] private LevelFactory levelFactory;
    [SerializeField] private Transform bricksStartPosition;

    [Header("UI")]
    [SerializeField] private GameUIManager uiManager;

    [Header("Lives")]
    [SerializeField] private int lives = 3;

    private int _score = 0;

    private void Awake()
    {
        if (_ballSpawner == null || levelFactory == null || bricksStartPosition == null)
            Debug.LogError("GameManager references missing!");

        GameBootstrap.EventBusSvc.Subscribe<BallLostEvent>(OnBallLost);
        GameBootstrap.EventBusSvc.Subscribe<BrickDestroyedEvent>(OnBrickDestroyed);
    }

    private void Start()
    {
        StartGame();
    }

    private void OnDestroy()
    {
        GameBootstrap.EventBusSvc.Unsubscribe<BallLostEvent>(OnBallLost);
        GameBootstrap.EventBusSvc.Unsubscribe<BrickDestroyedEvent>(OnBrickDestroyed);
    }

    private void StartGame()
    {
        SetPaused(false);
        
        GameBootstrap.GameStateSvc.SetState(GameState.Playing);

        // Генерируем уровень
        levelFactory.GenerateLevel(bricksStartPosition.position);

        // Спавн мяча
        _ballSpawner.SpawnBall();

        _score = 0;
        lives = 3;

        uiManager.InitUI(lives);
    }

    private void OnBallLost(BallLostEvent e)
    {
        lives--;
        uiManager.UpdateLives(lives);

        if (lives > 0)
        {
            _ballSpawner.SpawnBall();
        }
        else
        {
            GameOver(false);
        }
    }

    private void OnBrickDestroyed(BrickDestroyedEvent e)
    {
        _score += e.Points;
        uiManager.UpdateScore(_score);

        if (levelFactory.RemainingBricks == 0)
        {
            GameOver(true);
        }
    }

    private void SetPaused(bool paused)
    {
        Time.timeScale = paused ? 0f : 1f;
        GameBootstrap.GameStateSvc.SetState(
            paused ? GameState.Paused : GameState.Playing
        );
    }

    private void GameOver(bool victory)
    {
        GameBootstrap.GameStateSvc.SetState(victory ? GameState.Victory : GameState.Defeat);
        SetPaused(true); // Ставим паузу
        if (victory) uiManager.ShowVictory();
        else uiManager.ShowDefeat();
        Debug.Log(victory ? "You Win!" : "Game Over");
    }
}


