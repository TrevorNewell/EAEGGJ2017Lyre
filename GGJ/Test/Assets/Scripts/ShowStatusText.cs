using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowStatusText : MonoBehaviour
{
    public bool isTrigger; 
    public Text theTextObject;
    public Animator theController;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && isTrigger) DisplayMessage("Level Complete");
    }

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
