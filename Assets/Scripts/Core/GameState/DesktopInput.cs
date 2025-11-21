using UnityEngine;

public class DesktopInput : MonoBehaviour
{
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal"); 
        MobileInputState.Horizontal = h;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            var spawner = FindObjectOfType<BallSpawner>();
            spawner?.LaunchBall();
            MobileInputState.LaunchPressed = true;
        }
        else
        {
            MobileInputState.LaunchPressed = false;
        }
    }
}




