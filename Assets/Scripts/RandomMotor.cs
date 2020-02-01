using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMotor : MonoBehaviour
{
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 3.0f)
        {
            HingeJoint2D hinge = GetComponent<HingeJoint2D>();
            JointMotor2D motor = hinge.motor;
            motor.motorSpeed = Random.Range(0, 500);
            hinge.motor = motor;
            timer = Random.Range(1, 2);
        }

    }
}
