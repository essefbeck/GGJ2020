using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int lives;

    public TextMeshProUGUI livesText;

    public int score;

    public TextMeshProUGUI scoreText;

    public AudioClip failureSound;

    public AudioClip successSound;

    private AudioSource audioSource;

    public PauseManager pauseManager;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = string.Format("Score: {0}", score);
        livesText.text = string.Format("Lives: {0}", lives);
    }

    public void AddScore(int newScore)
    {
        score += newScore;

        if (audioSource != null)
        {
            audioSource.clip = successSound;
            audioSource.Play();
        }
    }

    public void LoseLife()
    {
        lives--;

        if (audioSource != null)
        {
            audioSource.clip = failureSound;
            audioSource.Play();
        }

        if (lives <= 0)
        {
            pauseManager.showGameOver();
        }
    }
}
