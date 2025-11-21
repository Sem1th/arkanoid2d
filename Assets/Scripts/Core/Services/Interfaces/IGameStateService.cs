using System;

public interface IGameStateService
{
    GameState State { get; }
    event Action<GameState> OnStateChanged;

    void SetState(GameState state);
}