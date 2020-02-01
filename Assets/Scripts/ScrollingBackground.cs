using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
     public float width;

    void Start ()
    {
        width = GetComponent<MeshRenderer>().bounds.size.x;
    }
    
    void Update ()
    {
        float speed = ScrollManager.Instance.GetSpeed();
        Vector2 offset = new Vector2(Time.time * speed / width, 0);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }
}
