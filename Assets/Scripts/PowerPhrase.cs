using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerPhrase : MonoBehaviour
{
    public TextMeshProUGUI textBox;

    public string[] textList;

    private float timer = 0;
    private float textDisplayTimer = 0;
    private bool hasText = false;
    private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(0.0f, 2.0f);
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasText)
            timer += Time.deltaTime;
        else
        {
            textDisplayTimer += Time.deltaTime;

            if (textDisplayTimer > 3)
            {
                hasText = false;
                textDisplayTimer = 0;
                textBox.text = "";
                canvas.enabled = false;
            }
        }

        if (timer > 8.0f)
        {
            textBox.text = textList[Random.Range(0, textList.Length)];
            hasText = true;
            timer = Random.Range(0.0f, 4.0f); ;
            canvas.enabled = true;
        }

    }
}
