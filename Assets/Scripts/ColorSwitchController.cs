using System;
using UnityEngine;

public class ColorSwitchController : MonoBehaviour
{
    private void Start()
    {
        PlayerController.OnColorSwtiched += OnColorSwitched;
    }

    private void OnColorSwitched(Vector3 position, string color)
    {
        if (transform.position.Equals(position))
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
