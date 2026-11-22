using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RetractableBridgeHandler : MonoBehaviour, IActivatable
{
    private Animator animator;
    private readonly string SHOWPARAM = "Show";
    private readonly string SPEEDPARAM = "Speed";
    private bool isActive = false;
    void Awake()
    {
        animator = GetComponent<Animator>();
        isActive = false;
}


    public void Activate<T>(T value)
    {
        if (isActive)
        {
            Deactivate<float>(float.Parse(value.ToString()));
        }
        else
        {
            //We need to both get the lenght of the animation and the speed to set the speed parameter correctly.
            if (value is float floatValue)
            {
                animator.SetFloat(SPEEDPARAM, floatValue);
            }
            else
            {
                Debug.LogWarning("Activate method received a value that is not a float.");
            }
            animator.SetBool(SHOWPARAM, true);
            isActive = true;

        }
        
        
    }

    public void Deactivate<T>(T value)
    {
        animator.SetBool(SHOWPARAM, false);
        isActive = false;
        
    }
}
