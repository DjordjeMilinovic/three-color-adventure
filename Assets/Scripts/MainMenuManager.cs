using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button levelsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private LevelManager levelManager;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        playButton.onClick.AddListener(() =>
        {
            audioSource.PlayOneShot(clickSound);
            levelManager.SetNextScene("Tutorial1");
            levelManager.LoadNextScene();
        });
        levelsButton.onClick.AddListener(() =>
        {
            audioSource.PlayOneShot(clickSound);
            levelManager.SetNextScene("Levels");
            levelManager.LoadNextScene();
        });
        exitButton.onClick.AddListener(() =>
        {
            audioSource.PlayOneShot(clickSound);
            Application.Quit();
        });
    }
}
