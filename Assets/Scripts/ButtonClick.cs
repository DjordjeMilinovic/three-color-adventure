using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public static Action OnButtonClicked;
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            OnButtonClicked?.Invoke();
        });
    }
}
