using UnityEngine;
using UnityEngine.UI;

public class LevelSceneManager : MonoBehaviour
{
    [SerializeField] private Button[] tutorialButtons;
    [SerializeField] private Button[] levelButtons;
    [SerializeField] private Button backButton;
    [SerializeField] private LevelManager levelManager;

    private void Start()
    {
        for (int i = 0; i < tutorialButtons.Length; i++)
        {
            int index = i;
            tutorialButtons[i].onClick.AddListener(() =>
            {
                LoadTutorial(index);
            });
        }
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int index = i;
            levelButtons[i].onClick.AddListener(() =>
            {
                LoadLevel(index);
            });
        }
        backButton.onClick.AddListener(() =>
        {
            levelManager.SetNextScene("MainMenu");
            levelManager.LoadNextScene();
        });
    }

    private void LoadTutorial(int i)
    {
        string sceneName = "Tutorial" + (i + 1);
        levelManager.SetNextScene(sceneName);
        levelManager.LoadNextScene();
    }

    private void LoadLevel(int i)
    {
        string sceneName = "Level" + (i + 1);
        levelManager.SetNextScene(sceneName);
        levelManager.LoadNextScene();
    }
}