using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InfoButton : MonoBehaviour
{

    public void ShowInfo()
    {
        GameObject InfoCanvasPrefab = Resources.Load<GameObject>("InfoCanvas");
        GameObject newInfocanvas=Instantiate(InfoCanvasPrefab);
        Button backButton = newInfocanvas.GetComponentInChildren<Button>();
        backButton.onClick.AddListener(delegate { Destroy(newInfocanvas); });
    }
}
