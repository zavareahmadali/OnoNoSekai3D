using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Level1TutorialCanvas : MonoBehaviour
{
    public Image Highlighter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
    }

    IEnumerator ShowPataPataTutorial()
    {
        Highlighter.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);

    }


}
