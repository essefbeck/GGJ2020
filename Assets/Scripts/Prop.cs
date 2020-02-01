using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    public int workRemaining;

    public GameObject fire;

    float workInterval = 1;
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
        }
    }
}
