using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int lives = 3;

    void Awake() => Instance = this;

    public void LoseLife()
    {
        lives--;
        Debug.Log("Lives left: " + lives);

        if (lives <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0f;
    }
}
