using System.Collections;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject visual;
    [SerializeField] private float delay;

    void Start()
    {
        GameStateHandler.OnPuzzleFailed += StartCoroutineForUI;
        visual.SetActive(false);
    }

    private void OnDisable()
    {
        GameStateHandler.OnPuzzleFailed -= StartCoroutineForUI;
    }

    public void OnRetryButtonClicked()
    {
        GameStateHandler.RestartLevel();
    }

    private void StartCoroutineForUI()  
    {
        StartCoroutine(nameof(ShowGameOverUIWithDelay));
    }

    private IEnumerator ShowGameOverUIWithDelay()
    {
        yield return new WaitForSeconds(delay);
        visual.SetActive(true);
        GetComponent<Animator>().SetTrigger("Show");
        yield return null;
    }
}
