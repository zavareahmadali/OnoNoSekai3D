using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GearHandler : MonoBehaviour, IInteractable
{


    #region Variable Declaration
    public bool isHeavy;
    public GameObject Bridge;

    [Header("Small Gear")]
    public float normalSpeed = 1;

    [Header("Big Gear")]
    public float highSpeed = 3;

    [Header("Other Settings")]
    public float duration;
    public bool oneTimeActivation = true;
    internal bool isActive = false;
    private float currentSpeed = 0;
    private Vector3 rotationVector = -Vector3.forward;
    private float timer;
    private IActivatable bridgeinterface;
    private AudioHandler audioHandler;
    private Animator animator;
    #endregion


    void Awake()
    {
        isActive = false;
        timer = duration;
        audioHandler = GetComponent<AudioHandler>();
        animator = GetComponent<Animator>();
        animator.SetBool("isHeavyGear", isHeavy);
        if (Bridge != null)
        {
            bridgeinterface = Bridge.GetComponent<IActivatable>();
        }
    }

    private void FixedUpdate()
    {
        if (!isActive)
            return;

        transform.Rotate(currentSpeed * rotationVector);
        if (timer==-1)
            return;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            isActive = false;
            if (!oneTimeActivation)
            {
                bridgeinterface.Deactivate<float>(0f); 
            }
        }

    }

    public void Interact(string ono)
    {
        if (ono == Onos.KURUKURU)
        {
            if (!isHeavy)
            {
                currentSpeed = normalSpeed;
                timer = duration;
                isActive = true;
                bridgeinterface.Activate<float>(1);
                audioHandler.PlayAudio(OnoType.KURUKURU);
            }
            else
            {
                //We should show that the gear doesn't rotate because the speed is too low
                animator.SetTrigger("Fail");
                
            }
        }
        else if (ono == Onos.GURUGURU)
        {

            if (isHeavy)
            {
                currentSpeed = highSpeed;
                timer = duration;
                isActive = true;
                bridgeinterface.Activate<float>(Mathf.Clamp(highSpeed/normalSpeed, 1, 1.2f));
                audioHandler.PlayAudio(OnoType.GURUGURU);
            }
            else
            {
                //We Should Show that the gear is destroyed because it's too light
                Debug.Log("Small Gear Failed!");
                animator.SetTrigger("Fail");
                GameStateHandler.LevelFailed();
            }
        }
    }

}
