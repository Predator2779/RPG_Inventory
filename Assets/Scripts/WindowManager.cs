using UnityEngine;

public class WindowManager : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject popupsPanel;

    private GameObject currentWindow;

    public void OpenWindow(GameObject window)
    {
        if (currentWindow != null)
        {
            currentWindow.SetActive(false);
        }

        currentWindow = window;
        currentWindow.SetActive(true);
    }

    public void OpenPopup(GameObject popup)
    {
        popup.SetActive(true);
    }

    public void ClosePopup(GameObject popup)
    {
        popup.SetActive(false);
    }
}