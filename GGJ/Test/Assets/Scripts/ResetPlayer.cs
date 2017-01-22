using UnityEngine;
using System.Collections;

public class ResetPlayer : MonoBehaviour
{
    public Vector3 resetToPosition;
    public Vector3 resetToRotation;
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            FindObjectOfType<LightEmUp>().Reset();
            GameObject.FindGameObjectWithTag("Player").transform.position = resetToPosition;
            GameObject.FindGameObjectWithTag("Player").transform.rotation = Quaternion.Euler(resetToRotation.x, resetToRotation.y, resetToRotation.z);
        }
    }

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
