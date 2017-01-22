using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour
{
    public GameObject highlight;
    public int defaultLayer = 0;
    private bool selected = false;

    public int givesColor = -1;

    public void Select()
    {
        highlight.SetActive(true);
        gameObject.layer = 11;
        selected = true;
    }

    public void Deselect()
    {
        highlight.SetActive(false);
        gameObject.layer = defaultLayer;
        selected = false;
    }

    public void Click()
    {
        if (givesColor > 0 && givesColor < 4) FindObjectOfType<LightEmUp>().GetColor(givesColor);
        gameObject.SetActive(false);//Destroy(gameObject);
    }

    public bool GetSelected()
    {
        return selected;
    }
}
