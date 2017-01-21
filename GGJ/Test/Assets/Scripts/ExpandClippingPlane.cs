using UnityEngine;
using System.Collections;

public class ExpandClippingPlane : MonoBehaviour
{
    public float expandRangePerSecond = 0.5f;
    public float retractRangePerSecond = 0.5f;
    public float maxClippingDistance = 10f;
    public int keyToExpand = 1;
    public bool holdToExpand = false;
    private KeyCode actualKeyCode = KeyCode.Alpha1;
    private bool expand = false;
    private bool overrider = false;
    private Camera thisCam;
    private float startClip;
    private float currentClip;

    void Start()
    {
        switch(keyToExpand)
        {
            case 2:
                actualKeyCode = KeyCode.Alpha2;
                break;
            case 3:
                actualKeyCode = KeyCode.Alpha3;
                break;
        }

        thisCam = GetComponent<Camera>();
        startClip = thisCam.nearClipPlane;
        currentClip = startClip;
    }

    public void Retract()
    {
        expand = false;
        overrider = true;
        
    }

	// Update is called once per frame
	void Update ()
    {
        if (holdToExpand)
        {
            expand = Input.GetKey(actualKeyCode);
        }
        else
        {
            if (Input.GetKeyDown(actualKeyCode) && !overrider)
                expand = !expand;
            overrider = false;
        }
	    if (expand)
        {
            if (thisCam.farClipPlane < maxClippingDistance)
            {
                thisCam.farClipPlane += expandRangePerSecond * Time.deltaTime;
            }
        }
        else
        {
            if (thisCam.farClipPlane > startClip)
            {
                thisCam.farClipPlane -= retractRangePerSecond * Time.deltaTime;
            }
        }
	}
}
