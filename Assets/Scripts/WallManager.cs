using System;
using System.Drawing;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    public static WallManager Instance { get; private set; }

    [SerializeField] private GameObject RedWalls;
    [SerializeField] private GameObject GreenWalls;
    [SerializeField] private GameObject BlueWalls;

    public string currentColor;
    //Mora da se doda brojac za satove ako ih ima vise istih boja ili da se ima posebna klasa za brojanje koja je za to zaduzena

    // 0 red, 1 green, 2 blue
    private bool[] activeClocks = { false, false, false };

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

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
            case "Red":
                RedWalls.gameObject.SetActive(false);
                activeClocks[0] = false;
                break;
            case "Green":
                GreenWalls.gameObject.SetActive(false);
                activeClocks[1] = false;
                break;
            case "Blue":
                BlueWalls.gameObject.SetActive(false);
                activeClocks[2] = false;
                break;
        }
    }


    private void OnClockCollected(Vector3 position, string color)
    {
        if (currentColor.Equals(color))
        {
            return;
        }
        switch (color)
        {
            case "Red":
                activeClocks[0] = true;
                RedWalls.gameObject.SetActive(false);
                break;
            case "Green":
                activeClocks[1] = true;
                GreenWalls.gameObject.SetActive(false);
                break;
            case "Blue":
                activeClocks[2] = true;
                BlueWalls.gameObject.SetActive(false);
                break;
        }
    }

    private void OnClockExpired(string clockColor)
    {
        switch (clockColor)
        {
            case "Red":
                if (!activeClocks[0]) return;
                activeClocks[0] = false;
                RedWalls.gameObject.SetActive(true);
                break;
            case "Green":
                if (!activeClocks[1]) return;
                activeClocks[1] = false;
                GreenWalls.gameObject.SetActive(true);
                break;
            case "Blue":
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
        ClockSwitch.OnClockExpired -= OnClockExpired;
    }
}
