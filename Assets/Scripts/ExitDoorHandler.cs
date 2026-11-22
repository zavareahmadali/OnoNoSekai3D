using System.Collections;
using UnityEngine;

public class ExitDoorHandler : MonoBehaviour
{
    public GameObject nextLevelIndicator;
    public Transform cameraView;
    public MeshRenderer DoorMesh;
    public Material NormalDoorMat;
    public Material InteractableDoorMat;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<KnightController>())
        {
            nextLevelIndicator.SetActive(true);
            DoorMesh.material = InteractableDoorMat;
        }
    }

    internal void OnInteract()
    {
        GameStateHandler.PuzzleSolved();
        nextLevelIndicator.SetActive(false);
    }
}
