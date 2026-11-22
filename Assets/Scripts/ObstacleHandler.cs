using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.DualShock.LowLevel;
using UnityEngine.Splines;
using UnityEngine.VFX;

public class ObstacleHandler : MonoBehaviour, IInteractable
{

    public bool isBig;
    public bool isFloating;
    public float speed;
    public Renderer visual;
    public Transform target;
    public Material normalMat;
    public Material fuwafuwaMat;
    private bool hasMoved = false;
    private Animator animator;
    private readonly string animParam = "Interact";
    private SplineAnimate splineAnimate;
    void Start()
    {
        animator = GetComponent<Animator>();
        splineAnimate = GetComponent<SplineAnimate>();
        hasMoved = false;
        isFloating = false;

    }

    public void Interact(string ono)
    {
        Debug.Log($"Interacted with {ono}.");
        if (isBig)
        {
            if (ono == Onos.GOROGORO)
            {
                StartMoving();
            }
            else if (ono == Onos.KOROKORO)
            {
                isBig = false;
                SetFloatable(false);
                animator.SetBool(animParam, true);
                Invoke(nameof(StartMoving), 1f);
            }
        }
        else
        {
            if (ono == Onos.KOROKORO)
            {
                StartMoving();
            }
            else if (ono == Onos.GOROGORO)
            {
                isBig = true;
                SetFloatable(false);
                animator.SetBool(animParam, true);
                Invoke(nameof(StartMoving), 1f);
            }
        }

        if (ono == Onos.FUWAFUWA)
        {
            SetFloatable(true);
        }
    }

    public void StartMoving()
    {
        if (!hasMoved)
        {
            hasMoved = true;
            splineAnimate.enabled = true;
            splineAnimate.Play(); 
        }
    }
    public void ReachedDestination()
    {
        Debug.Log("Reached Destination.");
        this.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isBig)
        {
            if (other.transform.TryGetComponent<IBreakable>(out IBreakable de))
            {
                de.Demolish(transform.position);
            }
        }
    }

    private void SetFloatable(bool isFloating)
    {
        this.isFloating = isFloating;
        visual.material = this.isFloating ? fuwafuwaMat : normalMat;
    }
}

public interface IInteractable
{
    void Interact(string ono);
}

public interface IFloatable
{
    void ChangeFloatState(bool shouldFloat);
}

