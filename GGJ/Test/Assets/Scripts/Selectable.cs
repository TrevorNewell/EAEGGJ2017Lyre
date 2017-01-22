using UnityEngine;

public class Selectable : MonoBehaviour
{
    public GameObject highlight;
    public int defaultLayer = 0;
    private bool selected = false;

    public int requiresColor = -1;

    public int givesColor = -1;
    public bool hasDisable = false;
    public string tagsToDisable = "";

    public bool disableFireflyGroup = false;

    public GameObject[] tagObjectsToDisable;
    public GameObject[] objectToDisable;

    void Start()
    {
        if (tagsToDisable != "")
            tagObjectsToDisable = GameObject.FindGameObjectsWithTag(tagsToDisable);
    }

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
            if (hasDisable)
            {
                foreach (GameObject g in objectToDisable)
                    g.SetActive(false);
            }
            if (tagsToDisable != "")
            {
                foreach (GameObject g in tagObjectsToDisable)
                    g.SetActive(false);
            }
            if (disableFireflyGroup)
            {
                GameObject g = GameObject.FindGameObjectWithTag("FireflyGroup");
                Transform[] theChildren = g.GetComponentsInChildren<Transform>();
                foreach (Transform t in theChildren)
                {
                    t.gameObject.SetActive(false);
                }
            }

            gameObject.SetActive(false);//Destroy(gameObject);
        }
    }

    public bool GetSelected()
    {
        return selected;
    }
}