using System;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateHandler : MonoBehaviour
{
    public float delayBeforeLoadingTheScene;
    public static Action OnPuzzleSolved;
    private static GameStateHandler instance;
    [SerializeField]
    public UnityEditor.SceneAsset nextSceneAsset;
    private void Awake()
    {
        instance = this;
        if (nextSceneAsset == null)
            Debug.Log("Check the nextScene parameter.", this.gameObject);

    }
    public static void PuzzleSolved()
    {
        OnPuzzleSolved?.Invoke();
        instance.Invoke(nameof(LoadSceneWithDelay), instance.delayBeforeLoadingTheScene );
    }

    private void LoadSceneWithDelay()
    {
        if (nextSceneAsset != null)
        {
            SceneManager.LoadScene(nextSceneAsset.name, LoadSceneMode.Single);
        }
        else
        {
            Debug.Log("Check the nextScene parameter.", this.gameObject);
        }
    }

    public static void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
