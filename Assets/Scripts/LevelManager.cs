using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string nextScene;

    private void Start()
    {
        PlayerController.OnGoalReached += OnGoalReached;
    }

    private void OnGoalReached()
    {
        if (nextScene == null || nextScene.Equals("")) { return; }
        SceneManager.LoadScene(nextScene);
    }

    private void OnDisable()
    {
        PlayerController.OnGoalReached -= OnGoalReached;
    }
}
