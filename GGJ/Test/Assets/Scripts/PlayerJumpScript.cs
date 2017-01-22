using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;

public class PlayerJumpScript : MonoBehaviour
{
    public float jumpMod = 20f;

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            c.gameObject.GetComponent<RigidbodyFirstPersonController>().movementSettings.JumpForce += jumpMod;
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.tag == "Player")
        {
            c.gameObject.GetComponent<RigidbodyFirstPersonController>().movementSettings.JumpForce -= jumpMod;
        }
    }
}
