using UnityEngine;
using System.Collections;

public class LightEmUp : MonoBehaviour
{
    public AudioClip[] tracks;
    public int currentColor = 0;    // For simplicity, 1 corresponds to Red, 2 corresponds to Blue
    public float maxRange = 100;
    public float expansionRate = 0.1f;
    public float overlapTime = 2.0f; // How long after being disabled, is an object still lit up?
    public float currentRange = 0.0f;

    private AudioSource theAudioSource;

    private ExpandAndRotate[] expandScripts;

	// Use this for initialization
	void Start ()
    {
        expandScripts = FindObjectsOfType<ExpandAndRotate>();

        theAudioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (currentRange < maxRange)
        {
            currentRange += expansionRate;
        }

        if (currentRange > maxRange)
        {
            currentRange = maxRange;
        }

	    if (Input.GetKeyDown(KeyCode.Alpha1) && currentColor != 1)
        {
            Activate(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && currentColor != 2)
        {
            Activate(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && currentColor != 3)
        {
            Activate(3);
        }

        //Debug.Log("Color: " + currentColor + " Range: " + currentRange);
	}

    public void Activate(int color)
    {
        if (currentColor != color)
        {
            currentRange = 0;
            currentColor = color;

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

            if (tracks.Length >= 3) // I assume this has 3 tracks for now.  1 for the first string, 2 for the second string, 3 for no string being played.
            {
                theAudioSource.Stop();
                theAudioSource.clip = tracks[currentColor - 1];
                theAudioSource.Play();
            }
        }
    }

}
