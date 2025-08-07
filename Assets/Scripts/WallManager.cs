using System;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    [SerializeField] private GameObject allColorWalls;
    [SerializeField] private GameObject noRedWalls;
    [SerializeField] private GameObject noGreenWalls;
    [SerializeField] private GameObject noBlueWalls;

    private void Start()
    {
        PlayerController.OnColorSwtiched += onColorSwitched;
        allColorWalls.SetActive(true);
        noRedWalls.gameObject.SetActive(false);
        noGreenWalls.gameObject.SetActive(false);
        noBlueWalls.gameObject.SetActive(false);
    }

    private void onColorSwitched(Vector3 position, string color)
    {
        allColorWalls.SetActive(false);;
        switch (color)
        {
            case "Red":
                noRedWalls.gameObject.SetActive(true); ;
                noGreenWalls.gameObject.SetActive(false);
                noBlueWalls.gameObject.SetActive(false);
                break;
            case "Green":
                noRedWalls.gameObject.SetActive(false); ;
                noGreenWalls.gameObject.SetActive(true);
                noBlueWalls.gameObject.SetActive(false);
                break;
            case "Blue":
                noRedWalls.gameObject.SetActive(false); ;
                noGreenWalls.gameObject.SetActive(false);
                noBlueWalls.gameObject.SetActive(true);
                break;
        }
    }

    private void OnDisable()
    {
        PlayerController.OnColorSwtiched -= onColorSwitched;
    }
}
