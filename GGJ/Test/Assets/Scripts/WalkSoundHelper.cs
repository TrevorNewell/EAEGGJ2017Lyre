using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class WalkSoundHelper : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioMixerSnapshot[] tracks;
    private Rigidbody body;

	// Use this for initialization
	void Start ()
    {
        body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (body.velocity.magnitude != 0 && StateManager.instance.TheState != StateManager.State.Pause)
        {
            mixer.TransitionToSnapshots(tracks, new float[2] { 0f, 1f }, 0.1f);
        }
        else
        {
            mixer.TransitionToSnapshots(tracks, new float[2] { 1f, 0f }, 0.7f);
        }
	}
}
