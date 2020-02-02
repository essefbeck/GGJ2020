using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    public int workRemaining;

    public int score;

    public bool finished = false;
    
    public Vector3 offset;

    public GameObject fire;
    public GameObject success;
    public AudioSource workSound;

    [SerializeField] private GameObject[] m_SelectionHalo;

    void Start()
    {
        success.SetActive(false);
    }

    public void Target(int playerId)
    {
        m_SelectionHalo[playerId].SetActive(true);    
    }

    public void Untarget(int playerId)
    {
        m_SelectionHalo[playerId].SetActive(false);
    }

    public void BeginWork(int playerId)
    {
        m_SelectionHalo[playerId].SetActive(false);
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void DoWork()
    {
        if (finished)
            return;

        if (!workSound.isPlaying)
            workSound.Play();
        
        if (workRemaining > 0)
        {
            workRemaining--;
        }
        
        if (workRemaining <= 0)
        {
            fire.SetActive(false);
            success.SetActive(true);
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
