using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
            EnableWalls(RedWalls, false);
        }
        if (!activeClocks[1])
        {
            EnableWalls(GreenWalls, false);
        }
        if (!activeClocks[2])
        {
            EnableWalls(BlueWalls, false);
        }


        switch (color)
        {
            case "Red":
                DisableWalls(RedWalls);
                activeClocks[0] = false;
                break;
            case "Green":
                DisableWalls(GreenWalls);
                activeClocks[1] = false;
                break;
            case "Blue":
                DisableWalls(BlueWalls);
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
                DisableWalls(RedWalls);
                break;
            case "Green":
                activeClocks[1] = true;
                DisableWalls(GreenWalls);
                break;
            case "Blue":
                activeClocks[2] = true;
                DisableWalls(BlueWalls);
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
                EnableWalls(RedWalls, true);
                break;
            case "Green":
                if (!activeClocks[1]) return;
                activeClocks[1] = false;
                EnableWalls(GreenWalls, true);
                break;
            case "Blue":
                if (!activeClocks[2]) return;
                activeClocks[2] = false;
                EnableWalls(BlueWalls, true);
                break;
        }
    }

    private void EnableWalls(GameObject walls, bool isClockSwitch)
    {
        walls.SetActive(true);
        if (isClockSwitch)
        {
            StartCoroutine(WaitPlayerExist(walls));
        }
    }

    IEnumerator WaitPlayerExist(GameObject walls)
    {
        TilemapCollider2D tilemap = walls.GetComponent<TilemapCollider2D>();
        List<Collider2D> results = new List<Collider2D>();
        Tilemap map = walls.GetComponent<Tilemap>();
        Color tileColor = map.color;
        tileColor.a = 0.5f;
        map.color = tileColor;
        bool isPlayerInside = false;
        while (true)
        {
            tilemap.enabled = true;
            tilemap.composite.Overlap(results);
            tilemap.enabled = false;
            foreach (Collider2D col in results)
            {
                if (col.CompareTag("Player"))
                {
                    isPlayerInside = true;
                    break;
                }
            }
            if (!isPlayerInside)
            {
                tilemap.enabled = true;
                tileColor.a = 1f;
                map.color = tileColor;
                break;
            }
            isPlayerInside = false;
            yield return new WaitForSeconds(0.05f);
        }

    }

    private void DisableWalls(GameObject walls)
    {
        walls.SetActive(false);
    }


    private void OnDisable()
    {
        PlayerController.OnColorSwtiched -= OnColorSwitched;
        PlayerController.OnClockCollected -= OnClockCollected;
        ClockSwitch.OnClockExpired -= OnClockExpired;
        StopAllCoroutines();
    }
}
