using UnityEngine;

public class SpikesHandler : MonoBehaviour, IActivatable
{
    public bool isActive = false;
    
    private Animator animator;
    private readonly string HIDEPARAM = "Hide";
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
    }

    public void Activate<T>(T value)
    {
        animator.SetBool(HIDEPARAM, true);
        isActive = false;
    }
    public void Deactivate<T>(T value)
    {
        animator.SetBool(HIDEPARAM, false);
        isActive = true;
    }
}

public interface IActivatable
{
    void Activate<T>(T value);
    void Deactivate<T>(T value);
}
