using System.Collections;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public float smoothSpeed = 0.125f;
    private KnightController player;
    private Vector3 offset;
    public bool showPlayerAndExitDoor = true;
    private bool isFollowingPlayer = true;
    private Transform exitDoorCameraView;
    private bool isCloseEnoughToPlayer = false;
    private const float CLOSE_ENOUGH_DISTANCE = 0.1f;
    void Start()
    {
        isFollowingPlayer = false;
        player = FindFirstObjectByType<KnightController>();
        exitDoorCameraView = GameObject.FindFirstObjectByType<ExitDoorHandler>().cameraView.transform;
        offset = transform.position - player.transform.position;

        if (showPlayerAndExitDoor)
        {
            StartCoroutine(nameof(ShowPlayerAndExitDoor));
        }
        else
        {
            isFollowingPlayer = true;
        }
    }

    void FixedUpdate()
    {
        if (player == null)
            return;

        if (!isFollowingPlayer)
            return;
        
        isCloseEnoughToPlayer = Vector3.Distance(transform.position, player.transform.position + offset) <= CLOSE_ENOUGH_DISTANCE;
        
        if (isCloseEnoughToPlayer)
            return;
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, Time.fixedDeltaTime * smoothSpeed);
    }

    private IEnumerator ShowPlayerAndExitDoor()
    {
        yield return new WaitForSeconds(1f);
        while ((Vector3.Distance(transform.position, exitDoorCameraView.position) > CLOSE_ENOUGH_DISTANCE))
        {
            transform.position = Vector3.Lerp(transform.position, exitDoorCameraView.position, Time.fixedDeltaTime * smoothSpeed);
            yield return new WaitForEndOfFrame(); 
        }
        yield return new WaitForSeconds(2f);
        
            isFollowingPlayer = true;
        yield return new WaitForEndOfFrame();
    }
}
