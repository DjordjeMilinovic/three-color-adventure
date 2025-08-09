using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string nextScene;

    [SerializeField] private Animator animator;

    private void Start()
    {
        PlayerController.OnGoalReached += OnGoalReached;
    }

    private void OnGoalReached()
    {
        if (nextScene == null || nextScene.Equals("")) { return; }
        LoadNextLevel(nextScene);
    }

    private void LoadNextLevel(string nextScene)
    {
        StartCoroutine(PlayAnimation(nextScene));
    }

    IEnumerator PlayAnimation(string nextScene)
    {
        animator.SetTrigger("StartFadeAnimation");
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(nextScene);
    }

    private void OnDisable()
    {
        PlayerController.OnGoalReached -= OnGoalReached;
    }
}
