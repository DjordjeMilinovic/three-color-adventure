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
        noRedWalls.SetActive(false);
        noGreenWalls.SetActive(false);
        noBlueWalls.SetActive(false);
    }

    private void onColorSwitched(Vector3 position, string color)
    {
        allColorWalls.SetActive(false);
        noRedWalls.SetActive(false);
        noGreenWalls.SetActive(false);
        noBlueWalls.SetActive(false);

        switch (color)
        {
            case "ColorRed":
                noRedWalls.gameObject.SetActive(true);
                break;
            case "ColorGreen":
                noGreenWalls.gameObject.SetActive(true);
                break;
            case "ColorBlue":
                noBlueWalls.gameObject.SetActive(true);
                break;
        }
    }

    private void OnDisable()
    {
        PlayerController.OnColorSwtiched -= onColorSwitched;
    }
}
