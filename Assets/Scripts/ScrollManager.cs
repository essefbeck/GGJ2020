using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollManager : MonoBehaviour
{
    public static ScrollManager Instance { get; private set; }

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public float GetSpeed()
    {
        return speed;
    }
}
