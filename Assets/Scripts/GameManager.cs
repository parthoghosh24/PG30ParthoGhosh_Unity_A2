using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private int _playerTotalScore = 0;

    [SerializeField]
    [Tooltip("Time in seconds")]
    private float timeLeft = 120f;

    private bool _isTimerRunning = true;

    [SerializeField]
    private TextMeshProUGUI totalScore;

    [SerializeField]
    private TextMeshProUGUI totalTime;

    public static GameManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        HandleScore(_playerTotalScore);
        DisplayTime(timeLeft);
        Instance = this;
    }

    public void HandleScore(int points)
    {
        _playerTotalScore += points;
        totalScore.text = $"{_playerTotalScore}";
    }

    public void HandleDeath()
    {
        SceneManager.LoadScene("DeadMenu");
    }

    public void HandleWin()
    {
        SceneManager.LoadScene("WinMenu");
    }

    private void Update()
    {
        // Simple if timer is running, check for time left
        if (_isTimerRunning)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                DisplayTime(timeLeft);
            }
            else
            {

                // If time reaches 0, game over
                timeLeft = 0f;
                _isTimerRunning = false;
                DisplayTime(timeLeft);
                HandleDeath();

            }
        }
        
    }

    private void DisplayTime(float timeToDisplay)
    {
        float min = Mathf.FloorToInt(timeToDisplay / 60); // mins
        float sec = Mathf.FloorToInt(timeToDisplay % 60); // sec

        totalTime.text = string.Format("{0:00}:{1:00}", min, sec);
    }


}
