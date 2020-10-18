using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PopupEnable : MonoBehaviour
{
    public GameObject objectToToggle;
    public GameObject carToHide;
    public bool resetSelectionAfterClick;

    void Update()
    {
        if (objectToToggle.activeSelf && Input.GetButtonDown("Cancel"))
        {
            SetGameObjectActive(false);
        }
    }

    public void SetGameObjectActive(bool active)
    {
        Debug.Log(active);
        objectToToggle.SetActive(active);

        if (carToHide != null)
            carToHide.SetActive(!active);

        if (resetSelectionAfterClick)
            EventSystem.current.SetSelectedGameObject(null);
    }

}
