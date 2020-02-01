using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int lives;

    public int score;

    public AudioClip failureSound;

    public AudioClip successSound;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            // lose game
        }
    }
}
