using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class KnightController : MonoBehaviour, IInteractable
{
    #region VariableDeclaration
    public float WalkSpeed;
    public float WalkUnit;
    public LayerMask Walls;
    public Animator animator;
    public bool JustMoveWithOno;
    private Vector3 destination;
    private readonly string WALK = "Walk";
    private readonly string TO_IDLE = "ToIdle";
    private bool canWalk = false;
    public float walkStopThreshold = 0.25f;
    private bool isNearExit = false;
    private bool isFacingRight = true;
    private Rigidbody rb;
    private AudioHandler audioHandler;
    #endregion

    private void Awake()
    {
        canWalk = false;
        isNearExit = false;
        isFacingRight = true;
        rb = GetComponent<Rigidbody>();
        audioHandler = GetComponent<AudioHandler>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    void OnWalk(InputValue value)
    {
        if (JustMoveWithOno)
            return;

        float valuef = value.Get<float>();
        if (canWalk)
            return;
        if (valuef == 0)
            return;
        if (Physics.BoxCast(transform.position, (WalkUnit * (valuef > 0 ? 1 : -1) * Vector3.forward), (WalkUnit * (valuef > 0 ? 1 : -1) * Vector3.forward)))
            return;
        //if (Physics.Linecast(transform.position + Vector3.up, transform.position + Vector3.up + (WalkUnit * (valuef > 0 ? 1 : -1) * Vector3.forward), Walls))
        //    return;


        if (transform.forward.z * valuef == -1)
        {
            transform.Rotate(new Vector3(0, 180, 0));
        }
        destination = transform.position + (WalkUnit * (valuef > 0 ? 1 : -1) * Vector3.forward);
        canWalk = true;
        animator.SetTrigger(WALK);

    }

    void OnInteract()
    {
        if (isNearExit)
        {
            Debug.Log("Interacted with Exit Door!");
            FindAnyObjectByType<ExitDoorHandler>().OnInteract();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("ExitDoor"))
        {
            isNearExit = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("ExitDoor"))
        {
            isNearExit = false;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (canWalk)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, WalkSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, destination) <= walkStopThreshold)
            {
                //transform.position = destination;
                animator.SetTrigger(TO_IDLE);
                canWalk = false;
            }
        }
    }

    public void Interact(string ono)
    {
        if (ono == Onos.TOKOTOKO)
        {
            if (Physics.Linecast(transform.position + Vector3.up, transform.position + Vector3.up + (WalkUnit * (isFacingRight ? 1 : -1) * Vector3.forward), Walls))
                return;

            destination = transform.position + (WalkUnit * (isFacingRight ? 1 : -1) * Vector3.forward);
            canWalk = true;
            animator.SetTrigger(WALK);
            audioHandler.PlayAudio(OnoType.TOKOTOKO);
        }
    }

}
