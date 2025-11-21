using UnityEngine;

/// <summary>
/// Управление паузой игры. 
/// Поддерживает UI-кнопку и Escape на ПК.
/// </summary>
public class GamePauseManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject pausePanel; // Панель с кнопками паузы

    private bool _paused = false;

    private void Start()
    {
        // Сразу скрываем панель паузы
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    private void Update()
    {
        // ПК: Escape пауза/возобновление
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    /// <summary>Ставим игру на паузу</summary>
    public void PauseGame()
    {
        if (_paused) return;

        _paused = true;
        Time.timeScale = 0f; // останавливаем физику
        if (pausePanel != null)
            pausePanel.SetActive(true);

        // Обновляем глобальное состояние игры
        GameBootstrap.GameStateSvc.SetState(GameState.Paused);
    }

    /// <summary>Возобновляем игру</summary>
    public void ResumeGame()
    {
        if (!_paused) return;

        _paused = false;
        Time.timeScale = 1f;
        if (pausePanel != null)
            pausePanel.SetActive(false);

        // Обновляем глобальное состояние игры
        GameBootstrap.GameStateSvc.SetState(GameState.Playing);
    }

    /// <summary>Переключение состояния паузы</summary>
    public void TogglePause()
    {
        if (_paused) ResumeGame();
        else PauseGame();
    }

    /// <summary>UI-кнопка: Pause</summary>
    public void OnPauseButtonPressed() => TogglePause();

    /// <summary>UI-кнопка: Resume</summary>
    public void OnResumeButtonPressed() => ResumeGame();
}


