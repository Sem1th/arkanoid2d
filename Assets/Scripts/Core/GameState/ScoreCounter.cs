using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter
{
    public int Score { get; private set; }

    public void Add(int value)
    {
        Score += value;
    }

    public void Reset()
    {
        Score = 0;
    }
}
