using UnityEngine;
using System.Collections;

public class Clicker : MonoBehaviour
{
    public float rangeToPick = 1.5f;
    private GameObject lastSelected;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject != lastSelected)
            {
                if (lastSelected != null && lastSelected.GetComponent<Selectable>() != null && lastSelected.GetComponent<Selectable>().GetSelected())
                {
                    lastSelected.GetComponent<Selectable>().Deselect();
                }
            }
            lastSelected = hit.collider.gameObject;
            if (lastSelected.GetComponent<Selectable>() != null)
            {
                if (!lastSelected.GetComponent<Selectable>().GetSelected())
                {
                    lastSelected.GetComponent<Selectable>().Select();
                }
                
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (hit.distance < rangeToPick)
                    {
                        lastSelected.GetComponent<Selectable>().Click();
                    }
                }
            }
        }
    }
}
