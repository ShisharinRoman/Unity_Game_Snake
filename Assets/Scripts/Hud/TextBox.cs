using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBox : MonoBehaviour
{

    TMP_Text text;

    public Color color
    {
        set { text.color = value; }
    }
    public string Str
    {
        set { text.text = value; }
    }
    public float FontSize
    {
        set { text.fontSize = value; }
    }

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }
}
