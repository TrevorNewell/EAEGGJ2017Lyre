using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowStatusText : MonoBehaviour
{
    public Text theTextObject;
    public Animator theController;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void DisplayMessage(string message)
    {
        theTextObject.text = message;

        theController.SetBool("FadeIn", true);
    }
}
