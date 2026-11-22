using Mono.Cecil;
using System;
using System.Collections;
using UnityEngine;

public class PressurePlateHandler : MonoBehaviour
{
    public GameObject target;
    public bool isOneTimeActivation;
    public bool isActivatedByHeavy;
    public float delayBetweenDeactivation;
    private Animator animator;
    private readonly string animParam = "Activate";
    private bool isActivated = false;
    
    private void Awake()
    {
        isActivated = false;
        animator = GetComponent<Animator>();
    }
    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.TryGetComponent<ObstacleHandler>(out ObstacleHandler obstacleHandler))
        {
            if (isActivatedByHeavy)
            {
                if (obstacleHandler.isBig)
                {
                    ToggleActivation(true);
                }
            }
            else
            {
                ToggleActivation(true);
            }   
        }
        else if (other.transform.CompareTag("Player"))
        {
            ToggleActivation(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isOneTimeActivation)
            return;

        if (other.TryGetComponent<ObstacleHandler>(out ObstacleHandler obstacleHandler))
        {
            if (isActivatedByHeavy)
            {
                if (obstacleHandler.isBig)
                {
                    ToggleActivation(false);
                }
            }
            else
            {
                ToggleActivation(false);
            }

        }
        else if (other.transform.CompareTag("Player"))
        {
            ToggleActivation(false);
        }
    }

    void ToggleActivation(bool shouldActivate)
    {
        if (target != null)
        {
            string message = shouldActivate ? "Activate" : "Deactivate";
            target.SendMessage(message,SendMessageOptions.DontRequireReceiver);

        }
        else
        {
            Debug.Log("no target assigned.");
        }
        animator.SetBool(animParam, shouldActivate);
        isActivated = shouldActivate;
    }

    private void OnDrawGizmos()
    {
        if (target != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, target.transform.position);
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(target.transform.position, Vector3.one * 0.5f);

        }

    }

}
