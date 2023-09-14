using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultMenuBackground : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject prefubResultText;
    private GameObject                  resultText;
    TextBox                             actionResultText;

    private SpriteRenderer              spriteRenderer;
    public Color Color
    {
        set 
        { 
            spriteRenderer.color =      value;
            actionResultText.color =    value; 
        }
        get 
        { 
            return spriteRenderer.color;  
        }
    }

    public string Str
    {
        set
        {
            actionResultText.Str = value;
        }
    }

    void Awake()
    {
        resultText =        Instantiate( prefubResultText, transform.localPosition - new Vector3( 0, -2, 1 ), transform.rotation );
        actionResultText =  resultText.GetComponent<TextBox>();
        spriteRenderer =    GetComponent<SpriteRenderer>();
    }
}
