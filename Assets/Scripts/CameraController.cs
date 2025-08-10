using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera mainCamera;
    float gameRatio = 16f / 9f;

    private void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        ChangeCameraOrthographicSize();
    }


    private void ChangeCameraOrthographicSize()
    {
        float currentAspectRatio = (float)Screen.width / (float)Screen.height;
        mainCamera.orthographicSize = mainCamera.orthographicSize * (gameRatio / currentAspectRatio);
        gameRatio = currentAspectRatio;
    }

    private void Update()
    {
        float currentAspectRatio = (float)Screen.width / (float)Screen.height;

        if (Mathf.Abs(gameRatio - currentAspectRatio) > 0.2)
        {
            ChangeCameraOrthographicSize();
        }
    }
}