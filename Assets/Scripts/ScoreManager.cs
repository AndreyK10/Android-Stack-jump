using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    
    [SerializeField] private int score;
    [SerializeField] private TextMeshProUGUI scoreText, highScoreText;
    private string highscore = "HS";


    private void Awake()
    {
        instance = this;
        highScoreText.text = PlayerPrefs.GetInt(highscore).ToString();
    }

    private void Start()
    {
        score = 0;
    }

    private void Update()
    {
        scoreText.text = score.ToString();
    }

    public void IncreaseScore()
    {
        score++;
    }

    public void UpdateHighScore()
    {
        if (score > PlayerPrefs.GetInt(highscore))
        {
            PlayerPrefs.SetInt(highscore, score);
        }
    }

}
