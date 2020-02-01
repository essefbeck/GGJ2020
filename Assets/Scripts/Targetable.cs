using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    [SerializeField] private GameObject m_SelectionHalo;
    
    public void Target()
    {
        m_SelectionHalo.SetActive(true);    
    }

    public void Untarget()
    {
        m_SelectionHalo.SetActive(false);
    }
}
