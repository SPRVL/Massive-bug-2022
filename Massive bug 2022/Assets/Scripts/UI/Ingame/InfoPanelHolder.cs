using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InfoPanelHolder : MonoBehaviour
{
    [SerializeField] Transform panelHolder;
    [SerializeField] GameObject infoPanelPrefab;
    [Tooltip("Add button must be holder's child")]
    [SerializeField] protected GameObject addButton;
    [SerializeField] protected int maxPanelCount;

    protected List<GameObject> panels = new List<GameObject>();


    public void AddNewPanel()
    {
        if (panels.Count < maxPanelCount && CanAddNewPanel())
        {
            panels.Add(Instantiate(infoPanelPrefab, panelHolder, false));
            addButton.transform.SetAsLastSibling();
            CheckCurrentPanelCount();
        }

    }
    protected abstract bool CanAddNewPanel();
    public void RemovePanel(GameObject panelToRemove)
    {
        if(panels.Remove(panelToRemove))
            CheckCurrentPanelCount();
    }
    protected virtual void CheckCurrentPanelCount()
    {
        if (panels.Count >= maxPanelCount) addButton.SetActive(false);
        else addButton.SetActive(true);
    }
}
