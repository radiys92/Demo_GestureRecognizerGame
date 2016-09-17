using UnityEngine;
using System.Collections;
using UIView;

public class WindowsViewStartupActivator : MonoBehaviour
{
    void Awake()
    {
        foreach (var view in transform.GetComponentsInChildren<WindowView>(true))
        {
            view.gameObject.SetActive(true);
        }
    }
}
