using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandAndRotate : MonoBehaviour
{
    public GameObject[] objects;
    public float rotationRate = 1.0f;
    public float expansionRate = 1.0f;

    public float maxExpansion = 100.0f;

    private bool isActive = false;
    private Vector3[] originalScale;

	// Use this for initialization
	void Start ()
    {
        originalScale = new Vector3[objects.Length];

	    for (int i = 0; i < objects.Length; i++)
        {
            originalScale[i] = objects[i].transform.localScale;
        }	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Activate(); // This will be called later.  Here for testing only.

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
	}

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].transform.localScale = originalScale[i];
        }
    }

}
