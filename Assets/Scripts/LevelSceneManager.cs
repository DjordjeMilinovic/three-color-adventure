using UnityEngine;
using UnityEngine.UI;

public class LevelSceneManager : MonoBehaviour
{
    [SerializeField] private Button[] tutorialButtons;
    [SerializeField] private Button[] levelButtons;
    [SerializeField] private Button backButton;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private LevelManager levelManager;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

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
            audioSource.PlayOneShot(clickSound);
            levelManager.SetNextScene("MainMenu");
            levelManager.LoadNextScene();
        });
    }

    private void LoadTutorial(int i)
    {
        audioSource.PlayOneShot(clickSound);
        string sceneName = "Tutorial" + (i + 1);
        levelManager.SetNextScene(sceneName);
        levelManager.LoadNextScene();
    }

    private void LoadLevel(int i)
    {
        audioSource.PlayOneShot(clickSound);
        string sceneName = "Level" + (i + 1);
        levelManager.SetNextScene(sceneName);
        levelManager.LoadNextScene();
    }
}