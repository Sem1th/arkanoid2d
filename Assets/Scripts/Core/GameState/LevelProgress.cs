using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgress
{
    private int _totalBricks;
    private int _destroyedBricks;

    public int TotalBricks => _totalBricks;
    public int DestroyedBricks => _destroyedBricks;
    public bool IsCompleted => _destroyedBricks >= _totalBricks;

    public LevelProgress(int totalBricks)
    {
        _totalBricks = totalBricks;
        _destroyedBricks = 0;
    }

    public void OnBrickDestroyed()
    {
        _destroyedBricks++;
    }
}
