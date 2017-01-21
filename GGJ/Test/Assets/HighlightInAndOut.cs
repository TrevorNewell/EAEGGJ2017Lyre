using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighlightInAndOut : MonoBehaviour
{
    public Text theText;
    public float rate = 0.5f;
    public bool increase = true;
	// Use this for initialization
	void Start ()
    {
        GetComponent<Text>().color = new Color(GetComponent<Text>().color.r, GetComponent<Text>().color.g, GetComponent<Text>().color.b, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GetComponent<Text>().color.a + rate > 255) increase = false;
        else if (GetComponent<Text>().color.a + rate < 0) increase = true;

        if (increase) GetComponent<Text>().color = new Color(GetComponent<Text>().color.r, GetComponent<Text>().color.g, GetComponent<Text>().color.b, GetComponent<Text>().color.a + rate);
        else if (increase) GetComponent<Text>().color = new Color(GetComponent<Text>().color.r, GetComponent<Text>().color.g, GetComponent<Text>().color.b, GetComponent<Text>().color.a - rate);


    }
}
