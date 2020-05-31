using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager game;
    float startTime;
    [SerializeField] GameObject gameOverScreen = null;
    [SerializeField] TextMeshProUGUI gameOverText = null;
    int enemiesKilled = 0;

    void Awake()
    {
        if (game == null) game = this;
        else Destroy(gameObject);

        startTime = Time.time;
    }
    private void Start()
    {
        gameOverScreen.SetActive(false);
        PlayerInputs.player.GetComponent<Health>().OnDeath += OnLoss;
    }

    public void IncrementDead()
    {
        ++enemiesKilled;
    }

    void OnLoss()
    {
        gameOverScreen.SetActive(true);
        string text = $"Game over.\nYou survived {(int)(Time.time - startTime)} s\nKilling {enemiesKilled} aliens";
        gameOverText.text = text;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

}
