using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject visual;

    private void Start()
    {
        GameStateHandler.OnPuzzleFailed += ShowGameOverUI;
    }

    private void OnDisable()
    {
        GameStateHandler.OnPuzzleFailed -= ShowGameOverUI;
    }
    public void ShowGameOverUI()
    {
        visual.SetActive(true);
    }
    public void OnRetryButtonClicked()
    {
        GameStateHandler.RestartLevel();
    }
}
