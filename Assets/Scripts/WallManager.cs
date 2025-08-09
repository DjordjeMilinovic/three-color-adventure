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
            EnableWalls(RedWalls);
        }
        if (!activeClocks[1])
        {
            EnableWalls(GreenWalls);
        }
        if (!activeClocks[2])
        {
            EnableWalls(BlueWalls);
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
                EnableWalls(RedWalls);
                break;
            case "Green":
                if (!activeClocks[1]) return;
                activeClocks[1] = false;
                EnableWalls(GreenWalls);
                break;
            case "Blue":
                if (!activeClocks[2]) return;
                activeClocks[2] = false;
                EnableWalls(BlueWalls);
                break;
        }
    }

    private void EnableWalls(GameObject walls)
    {
        walls.SetActive(true);
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
    }
}
