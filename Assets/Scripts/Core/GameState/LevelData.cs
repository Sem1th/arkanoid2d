using System;

[Serializable]
public class LevelData
{
    // Можно расширять: layout, brick types, score multiplier и т.д.
    public int Rows = 5;
    public int Columns = 8;
    public float BrickWidth = 1f;
    public float BrickHeight = 0.5f;

    // Доп: шаблон расположения кирпичей (null = заполнить равномерно)
    public int[] Layout; // длина Rows*Columns, 0 = пустая, > 0 = тип кирпича

    public LevelData() { }

    public LevelData(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
    }

    public int TotalCells => Rows * Columns;
}

