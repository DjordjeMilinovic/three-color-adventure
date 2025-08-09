using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button levelsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private LevelManager levelManager;


    private void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            levelManager.SetNextScene("Tutorial1");
            levelManager.LoadNextScene();
        });
        levelsButton.onClick.AddListener(() =>
        {
            levelManager.SetNextScene("Levels");
            levelManager.LoadNextScene();
        });
        exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
