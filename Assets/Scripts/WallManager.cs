using System;
using System.Drawing;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    [SerializeField] private GameObject RedWalls;
    [SerializeField] private GameObject GreenWalls;
    [SerializeField] private GameObject BlueWalls;

    private string currentColor;
    //Mora da se doda brojac za satove ako ih ima vise istih boja

    // 0 red, 1 green, 2 blue
    private bool[] activeClocks = { false, false, false };

    private void Start()
    {
        PlayerController.OnColorSwtiched += OnColorSwitched;
        PlayerController.OnClockCollected += OnClockCollected;
        ClockSwitch.OnClockExpired += OnClockExpired;
        currentColor = "";
    }

    private void OnColorSwitched(Vector3 position, string color)
    {
        if (color.Equals(currentColor))
        {
            return;
        }

        currentColor = color;

        if (!activeClocks[0])
        {
            RedWalls.SetActive(true);
        }
        if (!activeClocks[1])
        {
            GreenWalls.SetActive(true);
        }
        if (!activeClocks[2])
        {
            BlueWalls.SetActive(true);
        }


        switch (color)
        { 
            case "ColorRed":
                RedWalls.gameObject.SetActive(false);
                activeClocks[0] = false;
                break;
            case "ColorGreen":
                GreenWalls.gameObject.SetActive(false);
                activeClocks[1] = false;
                break;
            case "ColorBlue":
                BlueWalls.gameObject.SetActive(false);
                activeClocks[2] = false;
                break;
        }
    }


    private void OnClockCollected(Vector3 position, string color)
    {
        switch (color)
        {
            case "ClockRed":
                if (currentColor.Equals("ColorRed"))
                {
                    return;
                }
                activeClocks[0] = true;
                RedWalls.gameObject.SetActive(false);
                break;
            case "ClockGreen":
                if (currentColor.Equals("ColorGreen"))
                {
                    return;
                }
                activeClocks[1] = true;
                GreenWalls.gameObject.SetActive(false);
                break;
            case "ClockBlue":
                if (currentColor.Equals("ColorBlue"))
                {
                    return;
                }
                activeClocks[2] = true;
                BlueWalls.gameObject.SetActive(false);
                break;
        }
    }

    private void OnClockExpired(string clockColor)
    {
        switch (clockColor)
        {
            case "ClockRed":
                if (!activeClocks[0]) return;
                activeClocks[0] = false;
                RedWalls.gameObject.SetActive(true);
                break;
            case "ClockGreen":
                if (!activeClocks[1]) return;
                activeClocks[1] = false;
                GreenWalls.gameObject.SetActive(true);
                break;
            case "ClockBlue":
                if (!activeClocks[2]) return;
                activeClocks[2] = false;
                BlueWalls.gameObject.SetActive(true);
                break;
        }
    }


    private void OnDisable()
    {
        PlayerController.OnColorSwtiched -= OnColorSwitched;
        PlayerController.OnClockCollected -= OnClockCollected;
    }
}
