using UnityEngine;

public class GateHandler : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Activate()
    {
        animator.SetBool("Open", true);
    }
    public void Deactivate()
    {
        animator.SetBool("Open", false);
    }
}
