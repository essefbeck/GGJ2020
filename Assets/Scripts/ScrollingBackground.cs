using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    private float width;

    private float offset;

    void Start ()
    {
        width = GetComponent<MeshRenderer>().bounds.size.x;
    }
    
    void Update ()
    {
        float speed = ScrollManager.Instance.GetSpeed();
        offset += Time.deltaTime * speed / width;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offset, 0);
    }
}
