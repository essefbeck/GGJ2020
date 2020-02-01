using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    public int workRemaining;

    public int score;

    public bool finished = false;

    public GameObject fire;

    [SerializeField] private GameObject m_SelectionHalo;
    
    public void Target()
    {
        m_SelectionHalo.SetActive(true);    
    }

    public void Untarget()
    {
        m_SelectionHalo.SetActive(false);
    }

    public void BeginWork()
    {
        m_SelectionHalo.SetActive(false);
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void DoWork()
    {
        if (finished)
            return;
        
        if (workRemaining > 0)
        {
            workRemaining--;
        }
        
        if (workRemaining <= 0)
        {
            fire.SetActive(false);
            ScoreManager.Instance.AddScore(score);
            finished = true;
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
