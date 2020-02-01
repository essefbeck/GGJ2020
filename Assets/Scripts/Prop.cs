using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    public int workRemaining;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoWork()
    {
        if (workRemaining > 0)
        {
            workRemaining--;
        }
        
        if (workRemaining <= 0)
        {
            // set particles to something happy
        }
    }
}
