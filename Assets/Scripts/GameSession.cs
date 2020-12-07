using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    // config params
    [Range(0, 20)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI lifeText;
    [SerializeField] bool isAutoPlayEnabled;

    // state variables
    [SerializeField] int currentScore = 0;
    [SerializeField] int startLife = 3;
    int currentLife;


    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        scoreText.text = currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        scoreText.text = currentScore.ToString();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }

    public void ResetLife()
    {
        currentLife = startLife;
        lifeText.text = currentLife.ToString();
    }

    public void TakeLife()
    {
        currentLife--;
        lifeText.text = currentLife.ToString();
    }

    public bool GameOverCheck()
    {
        return currentLife <= 0;
    }

}
