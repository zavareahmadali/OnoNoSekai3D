using UnityEngine;

public class FloatableTile : MonoBehaviour, IInteractable, IFloatable
{
    public bool isFuwaFuwaed = false;
    public float moveSpeed;
    public Renderer visual;
    public Material normalMat;
    public Material fuwafuwaMat;
    private bool shouldFloat;
    private Animator animator;
    private readonly string RISE_ANIM_PARAM = "Rise";
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        shouldFloat = false;
    }
    public void Interact(string ono)
    {
        isFuwaFuwaed = (ono == Onos.FUWAFUWA);
        visual.material = (ono == Onos.FUWAFUWA) ? fuwafuwaMat : normalMat;
        if (shouldFloat && isFuwaFuwaed)
        {
            animator.SetBool(RISE_ANIM_PARAM, true);
        }
        else
        {
            animator.SetBool(RISE_ANIM_PARAM, false);
        }
    }

    public void ChangeFloatState(bool shouldFloat)
    {
        this.shouldFloat = shouldFloat;
        if (shouldFloat && isFuwaFuwaed)
        {
            animator.SetBool(RISE_ANIM_PARAM, true); 
        }
        else
        {
            animator.SetBool(RISE_ANIM_PARAM, false);
        }
    }

}
