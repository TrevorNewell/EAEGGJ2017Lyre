using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour
{
    public GameObject highlight;
    public int defaultLayer = 0;
    private bool selected = false;

    public int requiresColor = -1;

    public int givesColor = -1;
    public bool hasDisable = false;
    public GameObject objectToDisable;

    public void Select()
    {
        if (requiresColor == -1 || requiresColor == FindObjectOfType<LightEmUp>().currentColor)
        {
            highlight.SetActive(true);
            gameObject.layer = 11;
            selected = true;
        }
    }

    public void Deselect()
    {
        highlight.SetActive(false);
        gameObject.layer = defaultLayer;
        selected = false;
    }

    public void Click()
    {
        if (requiresColor == -1 || requiresColor == FindObjectOfType<LightEmUp>().currentColor)
        {
            if (givesColor > 0 && givesColor < 4) FindObjectOfType<LightEmUp>().GetColor(givesColor);
            if (hasDisable) objectToDisable.SetActive(false);
            gameObject.SetActive(false);//Destroy(gameObject);
        }
    }

    public bool GetSelected()
    {
        return selected;
    }
}
