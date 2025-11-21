using UnityEngine;
using UnityEngine.EventSystems;

public class MobileInput : MonoBehaviour
{
    [Header("References")]
    public BallSpawner ballSpawner;

    private bool moveLeft;
    private bool moveRight;
    private bool launchPressedThisFrame;

    private void Update()
    {
        if (Time.timeScale == 0f) 
        {
            MobileInputState.Horizontal = 0f;
            MobileInputState.LaunchPressed = false;
            return;
        }

        float move = 0f;
        if (moveLeft) move = -1f;
        if (moveRight) move = 1f;

        MobileInputState.Horizontal = move;

        // Запуск
        if (launchPressedThisFrame)
        {
            launchPressedThisFrame = false;
            ballSpawner?.LaunchBall();
            MobileInputState.LaunchPressed = true;
        }
        else
        {
            MobileInputState.LaunchPressed = false;
        }
    }

    // UI события
    public void OnLeftPressed() => moveLeft = true;
    public void OnLeftReleased() => moveLeft = false;

    public void OnRightPressed() => moveRight = true;
    public void OnRightReleased() => moveRight = false;

    public void OnLaunchPressed() => launchPressedThisFrame = true;

    private void OnDisable()
    {
        moveLeft = moveRight = false;
        launchPressedThisFrame = false;
        MobileInputState.Horizontal = 0f;
        MobileInputState.LaunchPressed = false;
    }

    private void OnDestroy() => OnDisable();
}



