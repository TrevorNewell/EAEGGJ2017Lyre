using UnityEngine;
using System.Collections;

public class WaterSpin : MonoBehaviour
{
    public float sinScaleFactor = 0.5f;
    public float angle = 45f;
    public float anglesPerSecond = 1f;
    public bool xAxis = true;
	
	// Update is called once per frame
	void Update ()
    {
	    if (xAxis)
        {
            angle += anglesPerSecond;
            if (angle >= 360)
                angle = 0;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + sinScaleFactor * Mathf.Sin(angle * Mathf.Deg2Rad) * Time.deltaTime, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else
        {
            angle += anglesPerSecond;
            if (angle >= 360)
                angle = 0;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + sinScaleFactor * Mathf.Sin(angle * Mathf.Deg2Rad) * Time.deltaTime);
        }
	}
}
