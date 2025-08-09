using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] GameObject openDoor;
    [SerializeField] GameObject closedDoor;

    private void Start()
    {
        openDoor.SetActive(false);
        closedDoor.SetActive(true);

        KeyManager.OnKeyCollected += OnKeyCollected;
    }

    private void OnKeyCollected()
    {
        openDoor.SetActive(true);
        closedDoor.SetActive(false);
    }

    private void OnDisable()
    {

        KeyManager.OnKeyCollected -= OnKeyCollected;
    }
}
