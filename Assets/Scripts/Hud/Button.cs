using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject prefubTextBox;
    private GameObject                  textBox;
    private TextBox                     actionTextBox;

    private SpriteRenderer              spriteRenderer;

    public string                       Str
    {
        set 
        {
            actionTextBox.Str = value;
        }
    }

    public Color color
    {
        set 
        { 
            spriteRenderer.color =  value;
            actionTextBox.color =   value;
        }
        get 
        { 
            return spriteRenderer.color; 
        }
    }

    private void Awake()
    {
        spriteRenderer =    GetComponent<SpriteRenderer>();
        textBox =           Instantiate( prefubTextBox, transform.position - new Vector3( 0, 0, 1 ), transform.rotation );
        actionTextBox =     textBox.GetComponent<TextBox>();
    }
}
