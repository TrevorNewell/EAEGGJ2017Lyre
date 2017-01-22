using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
    public float anglesPerSecond = 7f;
	
	// Update is called once per frame
	void Update ()
    {
        transform.rotation = transform.rotation * Quaternion.Euler(0f, anglesPerSecond * Time.deltaTime, 0);
	}
}
