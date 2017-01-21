using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandAndRotate : MonoBehaviour
{
    public int colorID = 0;
    public GameObject[] objects;
    public float rotationRate = 1.0f;
    public float expansionRate = 1.0f;
    public float retractionRate = 1.0f;

    public float maxExpansion = 100.0f;
    public float rangeModifier = 1.0f;
    private bool isActive = false;
    private bool retracting = false;
    private Vector3[] originalScale;

    private LightEmUp theScript;

	// Use this for initialization
	void Start ()
    {
        theScript = GameObject.FindGameObjectWithTag("Player").GetComponent<LightEmUp>();

        maxExpansion = theScript.maxRange * rangeModifier; // This allows us to scale our object properly accounting for the differences in the scale of our object and the scale of the scene.
        expansionRate = theScript.expansionRate * rangeModifier;
        originalScale = new Vector3[objects.Length];
	    for (int i = 0; i < objects.Length; i++)
        {
            originalScale[i] = objects[i].transform.localScale;
            objects[i].SetActive(false);
        }	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) Activate(); // This will be called later.  Here for testing only.

        if (StateManager.instance.TheState == StateManager.State.Pause) return;
        if (isActive)
        {
            int i = 0;
            foreach (GameObject g in objects)
            {
                i++;

                if (i % 2 == 0) g.transform.Rotate(Vector3.up, rotationRate);
                else g.transform.Rotate(Vector3.up, -rotationRate);

                if (g.transform.localScale.x + expansionRate <= maxExpansion) g.transform.localScale = new Vector3(g.transform.localScale.x + expansionRate, g.transform.localScale.y, g.transform.localScale.z + expansionRate);
            }
        }
        else if (retracting)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (i % 2 == 0) objects[i].transform.Rotate(Vector3.up, rotationRate);
                else objects[i].transform.Rotate(Vector3.up, -rotationRate);

                if (objects[i].transform.localScale.x > originalScale[i].x) objects[i].transform.localScale = new Vector3(objects[i].transform.localScale.x - retractionRate, objects[i].transform.localScale.y, objects[i].transform.localScale.z - retractionRate);
                else
                {
                    if (i == objects.Length - 1) retracting = false;
                    isActive = false;
                    objects[i].SetActive(false);
                }
            }
        }
	}

    public void Activate()
    {
        isActive = true;
        retracting = false;
        foreach (GameObject g in objects)
        {
            g.SetActive(true);
        }
    }

    public void Deactivate()
    {
        isActive = false;
        retracting = true;

        /*for (int i = 0; i < objects.Length; i++)
        {
            objects[i].transform.localScale = originalScale[i];
        }*/
    }

}
