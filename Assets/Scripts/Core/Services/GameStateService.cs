using System;

public class GameStateService : IGameStateService
{
    private GameState _state = GameState.None;
    public GameState State => _state;

    public event Action<GameState> OnStateChanged = delegate { };

    public void SetState(GameState state)
    {
        if (_state == state) return;
        _state = state;
        OnStateChanged?.Invoke(_state);
    }
}
