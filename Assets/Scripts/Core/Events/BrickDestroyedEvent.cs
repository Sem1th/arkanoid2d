
public readonly struct BrickDestroyedEvent
{
    public readonly UnityEngine.GameObject Brick;
    public readonly int Points;

    public BrickDestroyedEvent(UnityEngine.GameObject brick, int points = 10)
    {
        Brick = brick;
        Points = points;
    }
}
