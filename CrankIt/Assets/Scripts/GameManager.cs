using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Objectifs")]
    public int goal = 10;
    public float timeLimit = 120f;

    [Header("UI")]
    public TMP_Text timerText;
    public TMP_Text scoreText;
    public GameObject victoryPanel;
    public GameObject defeatPanel;

    int score = 0;
    float timeLeft;
    bool gameOver = false;

    void Awake()
    {
        Instance = this;
        timeLeft = timeLimit;
    }

    void Update()
    {
        if (gameOver) return;

        timeLeft -= Time.deltaTime;
        timerText.text = Mathf.CeilToInt(timeLeft) + "s";

        if (timeLeft <= 0f)
            EndGame(false);
    }

    public void AddScore()
    {
        if (gameOver) return;
        score++;
        scoreText.text = score + " / " + goal;

        if (score >= goal)
            EndGame(true);
    }

    void EndGame(bool victory)
    {
        gameOver = true;
        CancelInvoke();
        if (victory) victoryPanel.SetActive(true);
        else defeatPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
