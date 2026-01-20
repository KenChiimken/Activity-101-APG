using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("UI Settings")]
    public TextMeshProUGUI scoreText;

    private int score;

    void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("Score Text (TMP) not assigned!");
        }

        UpdateScore(0);
    }

    // Calls when target is destroyed
    public void AddScoreFromTargetZ(float targetZ)
    {
        //Round Z and clamp 1-5, then add to total score
        int zScore = Mathf.Clamp(Mathf.RoundToInt(targetZ), 1, 5);
        score += zScore;

        UpdateScore(score);
    }

    void UpdateScore(int newScore)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + newScore;
        }
    }
}
