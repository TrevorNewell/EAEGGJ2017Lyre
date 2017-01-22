using UnityEngine;
using System.Collections;

public class ColorListener : MonoBehaviour
{
    public bool isActive;

    public bool activateImmediately = false;
    public bool fader = false;

    public int colorValue;    // For simplicity, 1 corresponds to Red, 2 corresponds to Blue


    public Material defaultMaterial;
    public Material activeMaterial;

    public bool disableColliderIfInactive;
    public Collider theCollider;
    private Material[] defaultMaterialArray;
    private Material[] activeMaterialArray;

    private float overlap; // How long after this is disabled will it stay active?
    private float currentRange;

    private LightEmUp theScript;

    private bool countdownActive = false;
    private float temp = 0;

	// Use this for initialization
	void Start ()
    {
        theScript = GameObject.FindGameObjectWithTag("Player").GetComponent<LightEmUp>();

        Material[] materialTemp = new Material[1];
        materialTemp[0] = defaultMaterial;
        gameObject.GetComponent<MeshRenderer>().materials = materialTemp;

        currentRange = 0;

        overlap = theScript.overlapTime;

        defaultMaterialArray = new Material[1];
        defaultMaterialArray[0] = defaultMaterial;

        activeMaterialArray = new Material[1];
        activeMaterialArray[0] = activeMaterial;

        if (disableColliderIfInactive) theCollider.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        bool currentlyActive = isActive;

        currentRange = theScript.GetRangeByColorID(colorValue);

        DetermineActivity();

        if (countdownActive)
        {
            temp += Time.deltaTime;

            if (temp >= overlap)//
            {
                countdownActive = false;
                gameObject.GetComponent<MeshRenderer>().materials = defaultMaterialArray;
                if (disableColliderIfInactive) theCollider.enabled = false;
            }
        }
    }

    public float CalculateDistance()
    {
        return Vector3.Distance(theScript.gameObject.transform.position, transform.position);
    }

    public void DetermineActivity()
    {
        if ((CalculateDistance() <= currentRange || activateImmediately) && colorValue == theScript.currentColor)
        {
            gameObject.GetComponent<MeshRenderer>().materials = activeMaterialArray;

            isActive = true;

            if (disableColliderIfInactive) theCollider.enabled = true;
        }
        else
        {
            if (isActive)
            {
                Debug.Log("W'e;lkjwe");
                temp = 0;
                countdownActive = true;
            }

            isActive = false;
        }
    }
}
