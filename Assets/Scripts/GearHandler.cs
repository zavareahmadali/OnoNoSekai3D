using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GearHandler : MonoBehaviour, IInteractable
{
    public bool isHeavy;
    public GameObject Bridge;
    public float normalSpeed = 1;
    [SerializeField] private GameObject smallGearFailVFX;

    [Header("Big Gear")]
    public float highSpeed = 3;

    public float duration;
    public bool oneTimeActivation = true;
    internal bool isActive = false;
    private float currentSpeed = 0;
    private Vector3 rotationVector = -Vector3.forward;
    private float timer;
    private IActivatable bridgeinterface;
    private Animator animator;
    private readonly string FAILED = "Failed";


    void Awake()
    {
        isActive = false;
        timer = duration;
        animator = GetComponent<Animator>();
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
        if (timer == -1)
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
            }
            else
            {
                //We should show that the gear doesn't rotate because the speed is too low
            }
        }
        else if (ono == Onos.GURUGURU)
        {

            if (isHeavy)
            {
                currentSpeed = highSpeed;
                timer = duration;
                isActive = true;
                bridgeinterface.Activate<float>(Mathf.Clamp(highSpeed / normalSpeed, 1, 1.2f));
            }
            else
            {
                //bridgeinterface.Activate<float>(Mathf.Clamp(highSpeed / normalSpeed, 1, 1.2f));
                //RetractableBridgeHandler retractable = bridgeinterface as RetractableBridgeHandler;
                //retractable.SetShouldBreak();

                animator.SetTrigger(FAILED);
                if (!isActive)
                {
                    Debug.Log("here?");
                    GameStateHandler.LevelFailed();
                }

            }
        }
    }


    public void ShowFailVFX()
    {
        GameObject go = Instantiate(smallGearFailVFX, transform.position, smallGearFailVFX.transform.rotation);
        Destroy(go, 3);
    }

}
