using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float speed = ScrollManager.Instance.GetSpeed();
        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Eraser")
        {
            Destroy(gameObject);
        }
    }
}
