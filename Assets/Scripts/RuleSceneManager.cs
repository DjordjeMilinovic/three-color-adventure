using UnityEngine;
using UnityEngine.UI;

public class RuleSceneManager : MonoBehaviour
{
    [SerializeField] Button backButton;
    private void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            LevelManager levelManager = FindAnyObjectByType<LevelManager>();
            levelManager.LoadNextScene();
        });
    }
}
