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
        LoadNextScene();
    }

    IEnumerator PlayAnimation(string nextScene)
    {
        animator.SetTrigger("StartFadeAnimation");
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(nextScene);
    }

    public void SetNextScene(string nextScene)
    {
        this.nextScene = nextScene;
    }

    public void LoadNextScene()
    {
        StartCoroutine(PlayAnimation(nextScene));
    }

    private void OnDisable()
    {
        PlayerController.OnGoalReached -= OnGoalReached;
    }
}
