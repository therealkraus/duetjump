using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    Material material;
    Color c;
    Text text;
    public Vector3 startPos;

    // Use this for initialization
    void Start()
    {
        //material = GetComponent<MeshRenderer>().material;
        //color = material.color;
        startPos = transform.localPosition;
        text = GetComponent<Text>();
    }

    private void Update()
    {
        if (text.color.a > 0)
        {
            //color = text.color;
            //material = GetComponent<MeshRenderer>().material;
            //color = material.color;
            //material.color = new Color(color.r, color.g, color.b, color.a - (1f * Time.fixedDeltaTime));
            //text.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (0.3f * Time.fixedDeltaTime));
            transform.Translate(Vector3.up * Time.fixedDeltaTime * 25f);
        }
    }

    public void ResetText()
    {
        //text.transform.localScale = new Vector3(1f, 1f, 1f);
        //text.fontSize = 150;
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        //material.color = new Color(color.r, color.g, color.b, 1);
    }

}
