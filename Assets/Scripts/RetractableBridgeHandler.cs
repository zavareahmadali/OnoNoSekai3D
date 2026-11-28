using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RetractableBridgeHandler : MonoBehaviour, IActivatable
{


    [SerializeField] private float notBreakingValue;
    [SerializeField] private bool isFragile;
    [SerializeField] private GameObject fracturuedBridge;
    [SerializeField] private Transform fracturuedBridgeSpawnPoint;

    [SerializeField] private GameObject visual;

    private Animator animator;
    private readonly string SHOWPARAM = "Show";
    private readonly string SPEEDPARAM = "Speed";
    private bool isActive = false;
    private bool shouldBreak = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        isActive = false;
        shouldBreak = false;
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
                Debug.Log("FloatValue: " + floatValue);
                
            }
            else
            {
                Debug.LogWarning("Activate method received a value that is not a float.");
            }
            animator.SetBool(SHOWPARAM, true);
            isActive = true;

        }
        
        
    }
    public void SetShouldBreak()
    {
        shouldBreak = true;
    }
    public void OnEnteredBreakPoint()
    {
        if (shouldBreak)
        {
            visual.SetActive(false);
            GameObject go = Instantiate(fracturuedBridge, fracturuedBridgeSpawnPoint.position, fracturuedBridge.transform.rotation);
        }
    }
    public void Deactivate<T>(T value)
    {
        animator.SetBool(SHOWPARAM, false);
        isActive = false;
        
    }
}
