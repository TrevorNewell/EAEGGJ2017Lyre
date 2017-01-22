using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.UI;

public class LightEmUp : MonoBehaviour
{
    public Text theText;

    public AudioMixerSnapshot[] tracks;
    public AudioMixer mixer;
    public float audioTransitionTime = 1f;
    public int currentColor = -1;    // For simplicity, 1 corresponds to Red, 2 corresponds to Blue
    public float maxRange = 100;
    public float expansionRate = 0.1f;
    public float retractionRate = 0.1f;
    public float overlapTime = 2.0f; // How long after being disabled, is an object still lit up?
    public float currentRange = 0.0f;

    public Light ourLight;
    public ParticleSystem theBats;

    public bool hasBlue = false;
    public bool hasRed = false;
    public bool hasGreen = false;

    public GameObject lyre;

    [Range(1,10)] public int colorCount = 3;
    private int[] colors;
    private float[] ranges;

    private AudioSource theAudioSource;

    private ExpandAndRotate[] expandScripts;
    private ExpandClippingPlane[] expandClipping;
    private LyreActivation theLyreScript;

	// Use this for initialization
	void Start ()
    {
        ourLight.enabled = false;

        theLyreScript = FindObjectOfType<LyreActivation>();
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

    public void Reset()
    {
        Activate(-1);
    }

    public void Activate(int color)
    {
        if ((color == 1 && hasRed) || (color == 2 && hasBlue) || (color == 3 && hasGreen) || color == -1)
        {
            if (color > 0 && color < 4)
            {
                //theLyreScript.DeActivateString(currentColor);
                theLyreScript.ActivateString(color);

                ourLight.enabled = true;
                if (!theBats.isPlaying)
                {
                    theBats.Play();
                }
            }
            else if (color == -1)
            {
                theLyreScript.DeActivateString(currentColor);

                ourLight.enabled = false;
                if (theBats.isPlaying)
                {
                    theBats.SetParticles(new ParticleSystem.Particle[0], 0);
                    theBats.Pause();
                }
            }

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
                        if (e.colorID == 1 && hasRed) e.Deactivate();
                        if (e.colorID == 2 && hasBlue) e.Deactivate();
                        if (e.colorID == 3 && hasGreen) e.Deactivate();
                    }
                }

                foreach (ExpandClippingPlane e in expandClipping)
                {
                    if (e.keyToExpand != currentColor)
                    {
                        e.Retract();
                    }
                }

                /*if (tracks.Length >= 3) // I assume this has 3 tracks for now.  1 for the first string, 2 for the second string, 3 for no string being played.
                {
                    theAudioSource.Stop();
                    theAudioSource.clip = tracks[currentColor - 1];
                    theAudioSource.Play();
                }*/

                switch (currentColor)
                {
                    case -1:
                        mixer.TransitionToSnapshots(tracks, new float[4] { 1f, 0f, 0f, 0f }, audioTransitionTime);
                        break;
                    case 1:
                        mixer.TransitionToSnapshots(tracks, new float[4] { 0f, 1f, 0f, 0f }, audioTransitionTime);
                        break;
                    case 2:
                        mixer.TransitionToSnapshots(tracks, new float[4] { 0f, 0f, 1f, 0f }, audioTransitionTime);
                        break;
                    case 3:
                        mixer.TransitionToSnapshots(tracks, new float[4] { 0f, 0f, 0f, 1f }, audioTransitionTime);
                        break;
                }
            }
        }
        else
        {
            //currentColor = -1;
        }
    }

    public void GetColor(int i)
    {
        lyre.GetComponent<MeshRenderer>().enabled = true;
        foreach (ExpandClippingPlane e in expandClipping)
        {
            if (e.keyToExpand == i)
            {
                e.GetColor();
                break;
            }
        }
        if (i == 1)
        {
            hasRed = true;
            FindObjectOfType<ShowStatusText>().DisplayMessage("You can now left-click.");
        }
        else if (i == 2)
        {
            hasBlue = true;

            FindObjectOfType<ShowStatusText>().DisplayMessage("You can now right-click.");
        }
        else if (i == 3)
        {
            hasGreen = true;

            FindObjectOfType<ShowStatusText>().DisplayMessage("You can now middle-click.");

        }
        else
        {
            Debug.Log("THE FUCK IS THIS?!");
        }

        FindObjectOfType<LyreActivation>().UpdateStringVisibility();
    }

}
