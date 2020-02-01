﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    public int workRemaining;

    public int score;

    public GameObject fire;

    float workInterval = 3;
    float workTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        workTimer += Time.deltaTime;
        if (workTimer > workInterval)
        {
            workTimer = 0;
            DoWork();
        }
    }

    public void DoWork()
    {
        if (workRemaining > 0)
        {
            workRemaining--;
        }
        
        if (workRemaining <= 0)
        {
            fire.SetActive(false);
            ScoreManager.Instance.AddScore(score);
        }
    }

    void OnDestroy()
    {
        if (workRemaining > 0)
        {
            ScoreManager.Instance.LoseLife();
        }
    }
}
