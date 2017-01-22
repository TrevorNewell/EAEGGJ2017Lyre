using UnityEngine;
using System.Collections;

public class LyreActivation : MonoBehaviour
{
    public LightEmUp theScript;

    public Material invisibleMaterial;

    public GameObject redString;
    public GameObject blueString;
    public GameObject greenString;

    public Material originalRedMaterial;
    public Material originalBlueMaterial;
    public Material originalGreenMaterial;

    public Material newRedMaterial;
    public Material newBlueMaterial;
    public Material newGreenMaterial;

    public int activeString = -1;

	// Use this for initialization
	void Start ()
    {
        theScript = FindObjectOfType<LightEmUp>();

        UpdateStringVisibility();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void UpdateStringVisibility()
    {
        if (theScript.hasBlue)
        {
            Material[] temp = new Material[1];
            temp[0] = originalBlueMaterial;
            blueString.GetComponent<MeshRenderer>().materials = temp;
        }
        else
        {
            Material[] temp = new Material[1];
            temp[0] = invisibleMaterial;
            blueString.GetComponent<MeshRenderer>().materials = temp;
        }

        if (theScript.hasRed)
        {
            Material[] temp = new Material[1];
            temp[0] = originalRedMaterial;
            redString.GetComponent<MeshRenderer>().materials = temp;
        }
        else
        {
            Material[] temp = new Material[1];
            temp[0] = invisibleMaterial;
            redString.GetComponent<MeshRenderer>().materials = temp;
        }

        if (theScript.hasGreen)
        {
            Material[] temp = new Material[1];
            temp[0] = originalGreenMaterial;
            greenString.GetComponent<MeshRenderer>().materials = temp;
        }
        else
        {
            Material[] temp = new Material[1];
            temp[0] = invisibleMaterial;
            greenString.GetComponent<MeshRenderer>().materials = temp;
        }
    }

    public void ActivateString(int colorID)
    {
        if (activeString == colorID)
        {
            DeActivateString(colorID);

            activeString = -1;
        }
        else if (activeString != colorID)
        {
            if (colorID == 1)
            {
                Material[] temp = new Material[1];
                temp[0] = newRedMaterial;
                redString.GetComponent<MeshRenderer>().materials = temp;
            }
            else if (colorID == 2)
            {
                Material[] temp = new Material[1];
                temp[0] = newBlueMaterial;
                blueString.GetComponent<MeshRenderer>().materials = temp;

            }
            else if (colorID == 3)
            {
                Material[] temp = new Material[1];
                temp[0] = newGreenMaterial;
                greenString.GetComponent<MeshRenderer>().materials = temp;
            }
        }
    }

    public void DeActivateString(int colorID)
    {
        if (colorID == 1)
        {
            Material[] temp = new Material[1];
            temp[0] = originalRedMaterial;
            redString.GetComponent<MeshRenderer>().materials = temp;
        }
        else if (colorID == 2)
        {
            Material[] temp = new Material[1];
            temp[0] = originalBlueMaterial;
            blueString.GetComponent<MeshRenderer>().materials = temp;

        }
        else if (colorID == 3)
        {
            Material[] temp = new Material[1];
            temp[0] = originalGreenMaterial;
            greenString.GetComponent<MeshRenderer>().materials = temp;
        }
    }
}
