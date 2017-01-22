using UnityEngine;
using System.Collections;

public class DoorHandler : MonoBehaviour
{
    public GameObject key;
    public GameObject knob;
    public GameObject door;

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            if (!key.activeInHierarchy /*&& !knob.activeInHierarchy*/)
            {
                Destroy(door);
            }
        }
    }
}
