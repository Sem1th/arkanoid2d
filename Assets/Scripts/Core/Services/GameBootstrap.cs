using UnityEngine;

/// <summary>
/// Простой bootstrap / service locator для прототипа.
/// Инициализирует EventBus, GameStateService и InputService.
/// </summary>
public class GameBootstrap : MonoBehaviour
{
    // Статические ссылки на сервисы
    public static GameStateService GameStateSvc;
    public static EventBus EventBusSvc;
    public static InputService InputSvc;

    private void Awake()
    {
        EventBusSvc = new EventBus();
        GameStateSvc = new GameStateService();

        // InputService: попытаемся найти компонент на том же GameObject
        InputSvc = GetComponent<InputService>();
        if (InputSvc == null)
        {
            InputSvc = gameObject.AddComponent<InputService>();
        }

        // Установим начальное состояние (enum GameState определён в Core/Domain/GameState.cs)
        GameStateSvc.SetState(GameState.Loading);
    }

    private void Start()
    {
        // Переходим в Playing 
        GameStateSvc.SetState(GameState.Playing);
    }
}


