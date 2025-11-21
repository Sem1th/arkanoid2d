using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private TMP_Text scoreText;

    [Header("Lives")]
    [SerializeField] private Transform livesContainer; // Пустой объект, где будут рендериться иконки
    [SerializeField] private GameObject heartPrefab;   // Префаб одного сердечка

    [Header("End Panels")]
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject defeatPanel;

    private int _currentLives = 0;

    public void InitUI(int startingLives)
    {
        _currentLives = startingLives;
        UpdateLives(_currentLives);
        UpdateScore(0);

        if (victoryPanel) victoryPanel.SetActive(false);
        if (defeatPanel) defeatPanel.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null)
            scoreText.text = $"Счет: {score}";
    }

    public void UpdateLives(int lives)
    {
        _currentLives = lives;

        // Сначала очищаем контейнер
        foreach (Transform child in livesContainer)
        {
            Destroy(child.gameObject);
        }

        // Создаём нужное количество иконок
        for (int i = 0; i < _currentLives; i++)
        {
            Instantiate(heartPrefab, livesContainer);
        }
    }

    public void ShowVictory()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(true);
    }

    public void ShowDefeat()
    {
        if (defeatPanel != null)
            defeatPanel.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}



