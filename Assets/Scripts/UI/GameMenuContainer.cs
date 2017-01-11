using UnityEngine;
using System.Collections;

public class GameMenuContainer : MonoBehaviour
{

    public GameObject BackgroundPanel;
    public GameObject GameEndContainer;

    public void ShowGameEndContainer()
    {
        BackgroundPanel.SetActive(true);
        GameEndContainer.SetActive(true);
    }
}
