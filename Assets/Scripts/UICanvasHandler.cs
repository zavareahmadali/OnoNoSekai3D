using NUnit.Framework;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class UICanvasHandler : MonoBehaviour
{
    public Animator[] NewlyIntroducedOno;
    public TMP_Text LevelText;
    private Animator animator;
    
    private readonly string SHOWPARAM = "Show";
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    void Start()
    {
        GameStateHandler.OnPuzzleSolved += FadeToBlack;
        LevelText.text = SceneManager.GetActiveScene().name;
        foreach (var anim in NewlyIntroducedOno)
        {
            anim.SetTrigger("Show");
        }
    }
    private void OnDisable()
    {
        GameStateHandler.OnPuzzleSolved -= FadeToBlack;
    }
    public void FadeToBlack()
    {
        animator.SetBool(SHOWPARAM, true);
    }
    public void MakeTransparent()
    {
        animator.SetBool(SHOWPARAM, false);
    }

    public void RestartLevel()
    {
        GameStateHandler.RestartLevel();
    }
}
