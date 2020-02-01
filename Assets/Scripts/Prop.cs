using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    public int workRemaining;

    public int score;

    public GameObject fire;

    float workInterval = 3;
    float workTimer = 0;

    [SerializeField] private GameObject m_SelectionHalo;
    
    public void Target()
    {
        m_SelectionHalo.SetActive(true);    
    }

    public void Untarget()
    {
        m_SelectionHalo.SetActive(false);
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
