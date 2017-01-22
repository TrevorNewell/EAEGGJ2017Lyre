using UnityEngine;
using System.Collections;

public class WaterSpin : MonoBehaviour
{
    public float radius = 30f;
    public float angle = 45f;
    public float anglesPerSecond = 1;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

	// Update is called once per frame
	void Update ()
    {
        angle += anglesPerSecond * Time.deltaTime;
        if (angle >= 360) angle = 0;
        if (angle <= 0) angle = 360; 
        transform.position = new Vector3(originalPosition.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad), originalPosition.y, originalPosition.z + radius * Mathf.Sin(angle * Mathf.Deg2Rad));
	}
}
