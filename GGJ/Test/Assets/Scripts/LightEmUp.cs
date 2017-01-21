using UnityEngine;
using System.Collections;

public class LightEmUp : MonoBehaviour
{
    public AudioClip[] tracks;
    public int currentColor = -1;    // For simplicity, 1 corresponds to Red, 2 corresponds to Blue
    public float maxRange = 100;
    public float expansionRate = 0.1f;
    public float retractionRate = 0.1f;
    public float overlapTime = 2.0f; // How long after being disabled, is an object still lit up?
    public float currentRange = 0.0f;

    [Range(1,10)] public int colorCount = 3;
    private int[] colors;
    private float[] ranges;

    private AudioSource theAudioSource;

    private ExpandAndRotate[] expandScripts;
    private ExpandClippingPlane[] expandClipping;

	// Use this for initialization
	void Start ()
    {
        expandScripts = FindObjectsOfType<ExpandAndRotate>();
        expandClipping = FindObjectsOfType<ExpandClippingPlane>();

        theAudioSource = GetComponent<AudioSource>();
        
        colors = new int[colorCount];
        ranges = new float[colorCount];

        for (int i = 0; i < colorCount; i++)
        {
            colors[i] = 48 + i;
            ranges[i] = 0.0f;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (StateManager.instance.TheState == StateManager.State.Pause) return;

        if (currentColor != -1)
        {
            if (ranges[currentColor] < maxRange)
            {
                ranges[currentColor] += expansionRate;
            }

            if (ranges[currentColor] > maxRange)
            {
                ranges[currentColor] = maxRange;
            }
        }

        for (int i = 0; i < colorCount; i++)
        {
            if (i == currentColor)
                continue;
            if (ranges[i] > 0)
            {
                ranges[i] -= retractionRate;
            }
            else
            {
                ranges[i] = 0;
            }
        }

	    /*if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (currentColor != 1)
                Activate(1);
            else
                Activate(-1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (currentColor != 2)
                Activate(2);
            else
                Activate(-1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (currentColor != 3)
                Activate(3);
            else
                Activate(-1);
        }*/

        for (int i = 1; i < colorCount + 1; i++)
        {
            //if (Input.GetKeyDown((KeyCode)(48 + i)))
            if (Input.GetMouseButtonDown(i - 1))
            {
                if (currentColor != i)
                {
                    Activate(i);
                }
                else
                {
                    Activate(-1);
                }
                break;
            }
        }

        //Debug.Log("Color: " + currentColor + " Range: " + currentRange);
	}

    public float GetRangeByColorID(int i)
    {
        return ranges[i];
    }

    public void Activate(int color)
    {
        if (currentColor != color)
        {
            currentColor = color;
            if (currentColor != -1) ranges[currentColor] = 0;

            foreach (ExpandAndRotate e in expandScripts)
            {
                if (e.colorID == currentColor)
                {
                    e.Activate();
                }
                else
                {
                    e.Deactivate();
                }
            }

            foreach (ExpandClippingPlane e in expandClipping)
            {
                if (e.keyToExpand != currentColor)
                {
                    e.Retract();
                }
            }

            if (tracks.Length >= 3) // I assume this has 3 tracks for now.  1 for the first string, 2 for the second string, 3 for no string being played.
            {
                theAudioSource.Stop();
                theAudioSource.clip = tracks[currentColor - 1];
                theAudioSource.Play();
            }
        }
    }

}
