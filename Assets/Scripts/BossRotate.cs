using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRotate : MonoBehaviour
{
    public float rotationCoefficient;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = ScrollManager.Instance.GetSpeed() * rotationCoefficient;
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}
