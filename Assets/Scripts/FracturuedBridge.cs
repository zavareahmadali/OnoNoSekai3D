using UnityEngine;

public class FracturuedBridge : MonoBehaviour
{
    [SerializeField] private float explosionForce;
    void Start()
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var r in rigidbodies)
        {
            r.AddForce(UnityEngine.Random.onUnitSphere * explosionForce, ForceMode.Impulse);
        }

        Destroy(gameObject, 2);
    }

    
}
