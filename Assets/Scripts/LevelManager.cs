using System;
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
        Debug.Log("Next scene lets goo!");
        //SceneManager.LoadScene(nextScene);
    }
}
