using UnityEngine;
using System.Collections.Generic;

public class LevelFactory : MonoBehaviour
{
    [Header("Brick Settings")]
    [SerializeField] private GameObject[] brickPrefabs;
    [SerializeField] private int rows = 5;
    [SerializeField] private int columns = 8;
    [SerializeField] private float spacingX = 0.1f;
    [SerializeField] private float spacingY = 0.1f;
    [SerializeField] private string brickTag = "Brick";

    private List<GameObject> _bricks = new List<GameObject>();

    /// <summary>
    /// Создаёт сетку кирпичей
    /// </summary>
    public void GenerateLevel(Vector2 startPosition)
    {
        ClearLevel();

        if (brickPrefabs.Length == 0)
        {
            Debug.LogError("No brick prefabs assigned!");
            return;
        }

        // Размер одного кирпича с учётом масштаба
        SpriteRenderer sr = brickPrefabs[0].GetComponent<SpriteRenderer>();
        Vector2 brickSize = new Vector2(sr.sprite.bounds.size.x * brickPrefabs[0].transform.localScale.x,
                                        sr.sprite.bounds.size.y * brickPrefabs[0].transform.localScale.y);

        // Смещение для центрирования сетки
        float totalWidth = columns * brickSize.x + (columns - 1) * spacingX;
        Vector2 offset = new Vector2(-totalWidth / 2f + brickSize.x / 2f, 0f);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector2 pos = startPosition;
                pos += offset; // Центрируем по X
                pos.x += col * (brickSize.x + spacingX);
                pos.y -= row * (brickSize.y + spacingY);

                GameObject prefab = brickPrefabs[Random.Range(0, brickPrefabs.Length)];
                GameObject brick = Instantiate(prefab, pos, Quaternion.identity, transform);

                // Добавляем тег Brick
                brick.tag = brickTag;

                _bricks.Add(brick);
            }
        }
    }

    /// <summary>
    /// Очищает все бриксы на сцене
    /// </summary>
    public void ClearLevel()
    {
        foreach (var b in _bricks)
        {
            if (b != null) Destroy(b);
        }
        _bricks.Clear();
    }

    public void RemoveBrick(GameObject brick)
    {
        if (_bricks.Contains(brick))
            _bricks.Remove(brick);
    }

    public int RemainingBricks => _bricks.Count;
}


